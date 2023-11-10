using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityEngine;
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
        readonly Dictionary<TopicAsset, int> topicsAndDestruction = new();
        internal int GetDestruction(TopicAsset topic) => topicsAndDestruction.GetValueOrDefault(topic);
        IEnumerable<TopicAsset> notDestructedTopics {
            get {
                foreach (var (topic, destruction) in topicsAndDestruction) {
                    if (destruction < topic.maxDestruction) {
                        yield return topic;
                    }
                }
            }
        }
        internal IEnumerable<TopicAsset> topics => topicsAndDestruction.Keys;
        readonly Dictionary<ReporterAsset, TopicGroup> reportersAndTopics = new();
        internal IEnumerable<ReporterAsset> reporters => reportersAndTopics.Keys;

        bool IsTruthfulAbout(ReporterAsset reporter, TopicAsset topic) => reportersAndTopics[reporter]
            .Contains(topic);

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

            foreach (var topic in topics) {
                topicsAndDestruction[topic] = 0;
            }

            foreach (var reporter in reporters) {
                reportersAndTopics[reporter] = CreateUniqueTopicGroup();
            }

            onStartInternal?.Invoke(this);
        }

        TopicGroup CreateUniqueTopicGroup() {
            TopicGroup topicGroup = default;
            do {
                topicGroup = new TopicGroup(topics.Shuffle().Take(UnityRandom.Range(0, 4)).ToList());
            } while (reportersAndTopics.ContainsValue(topicGroup));
            return topicGroup;
        }

        internal GameRound StartRound(IEnumerable<InputAsset> answers) {
            currentRound++;

            var correctAnswer = answers.RandomElement();

            var round = new GameRound {
                correctAnswer = correctAnswer,
                topic = notDestructedTopics.RandomElement()
            };

            int reporterCount = Mathf.Min(4, currentRound);
            foreach (var reporter in reporters.Shuffle().Take(reporterCount)) {
                var input = IsTruthfulAbout(reporter, round.topic)
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
                topicsAndDestruction[round.topic]++;
                mistakes++;
            }

            isRunning = currentRound < lastRound && notDestructedTopics.Any();

            if (!isRunning) {
                hasWon = notDestructedTopics.Any();

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
