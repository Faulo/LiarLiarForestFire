using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityRandom = UnityEngine.Random;

namespace Runtime {
    sealed record GameState {
        static GameState instance;

        static event Action<GameState> onChangeInternal;
        internal static event Action<GameState> onChange {
            add {
                onChangeInternal += value;

                if (instance is not null) {
                    value?.Invoke(instance);
                }
            }
            remove {
                onChangeInternal -= value;
            }
        }

        static event Action<GameState> onStartInternal;
        internal static event Action<GameState> onStart {
            add {
                onStartInternal += value;

                if (instance is not null) {
                    value?.Invoke(instance);
                }
            }
            remove {
                onStartInternal -= value;
            }
        }

        internal static event Action onWin;

        internal static event Action onLose;

        internal readonly List<GameRound> rounds = new();
        internal readonly List<TopicAsset> topics = new();
        internal readonly List<ReporterAsset> reporters = new();

        internal int currentRound { get; private set; }
        internal int lastRound { get; private set; }

        internal int successes { get; private set; }
        internal int mistakes { get; private set; }

        internal bool isRunning { get; private set; }
        internal bool hasWon { get; private set; }

        internal GameState(int roundCount, IEnumerable<TopicAsset> topics, IEnumerable<ReporterAsset> reporters) {
            instance = this;

            lastRound = roundCount;
            isRunning = roundCount > 0;

            this.topics.AddRange(topics);
            this.reporters.AddRange(reporters);

            onStartInternal?.Invoke(this);
        }

        internal void AddTopic(TopicAsset topic) {
            topics.Add(topic);

            onChangeInternal?.Invoke(this);
        }

        internal void AddReporter(ReporterAsset reporter) {
            reporters.Add(reporter);

            onChangeInternal?.Invoke(this);
        }

        internal GameRound StartRound(IEnumerable<InputAsset> answers) {
            currentRound++;

            var correctAnswer = answers.RandomElement();

            var round = new GameRound {
                correctAnswer = correctAnswer,
                topic = topics.RandomElement()
            };

            int reporterCount = UnityRandom.Range(1, 4);
            foreach (var reporter in reporters.Shuffle().Take(reporterCount)) {
                var input = reporter.IsTruthfulAbout(round.topic)
                    ? correctAnswer
                    : answers.Without(correctAnswer).RandomElement();
                round.reporterAndInputs[reporter] = input;
            }

            rounds.Add(round);

            round.RaiseStart();
            onChangeInternal?.Invoke(this);

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

                if (hasWon) {
                    onWin?.Invoke();
                } else {
                    onLose?.Invoke();
                }
            }

            round.RaiseFinish();
            onChangeInternal?.Invoke(this);
        }
    }
}
