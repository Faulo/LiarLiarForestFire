using System;
using System.Collections.Generic;
using Runtime.Assets;
using Slothsoft.UnityExtensions;

namespace Runtime {
    sealed record GameState {
        internal static event Action<GameState> onChange;

        readonly List<GameRound> rounds = new();
        readonly List<TopicAsset> topics = new();
        readonly List<ReporterAsset> reporters = new();

        internal int currentRound { get; private set; }
        internal int lastRound { get; private set; }

        internal int successes { get; private set; }
        internal int mistakes { get; private set; }

        internal bool isRunning { get; private set; }
        internal bool hasWon { get; private set; }

        internal GameState(int roundCount, IEnumerable<TopicAsset> topics, IEnumerable<ReporterAsset> reporters) {
            lastRound = roundCount;

            this.topics.AddRange(topics);
            this.reporters.AddRange(reporters);
        }

        internal void AddTopic(TopicAsset topic) {
            topics.Add(topic);

            onChange?.Invoke(this);
        }

        internal void AddReporter(ReporterAsset reporter) {
            reporters.Add(reporter);

            onChange?.Invoke(this);
        }

        internal GameRound StartRound(InputAsset correctAnswer) {
            currentRound++;
            var round = new GameRound {
                correctAnswer = correctAnswer,
                topic = topics.RandomElement()
            };

            rounds.Add(round);
            onChange?.Invoke(this);

            return round;
        }

        internal void FinishRound(GameRound round) {
            if (round.isCorrect) {
                successes++;
            } else {
                mistakes++;
            }

            isRunning = currentRound < lastRound;

            if (!isRunning) {
                hasWon = successes > mistakes;
            }

            onChange?.Invoke(this);
        }
    }
}
