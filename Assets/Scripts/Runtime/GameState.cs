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
        IEnumerable<ReporterAsset> GetTruthfulReporters(TopicAsset topic) => reportersAndTopics
                .Where(value => value.Value.Contains(topic))
                .Select(value => value.Key);
        bool HasTruthfulReporter(TopicAsset topic) => reportersAndTopics
                .Any(value => value.Value.Contains(topic));

        internal int currentRound { get; private set; }
        internal int lastRound { get; private set; }

        internal int successes { get; private set; }
        internal int mistakes { get; private set; }

        internal bool isRunning { get; private set; }
        internal bool hasWon { get; private set; }

        internal GameState(int roundCount, IReadOnlyList<TopicAsset> topics, IReadOnlyList<ReporterAsset> reporters) {
            instance = this;

            lastRound = roundCount;
            isRunning = roundCount > 0;

            foreach (var topic in topics) {
                topicsAndDestruction[topic] = 0;
            }

            do {
                CreateReporters(topics, reporters);
            } while (!topics.All(HasTruthfulReporter));

            onStartInternal?.Invoke(this);
        }

        void CreateReporters(IReadOnlyList<TopicAsset> topics, IReadOnlyList<ReporterAsset> reporters) {
            reportersAndTopics.Clear();
            foreach (var reporter in reporters) {
                reportersAndTopics[reporter] = CreateUniqueTopicGroup(topics);
            }
        }

        TopicGroup CreateUniqueTopicGroup(IReadOnlyList<TopicAsset> topics) {
            TopicGroup topicGroup = default;
            do {
                topicGroup = new TopicGroup(topics.Shuffle().Take(UnityRandom.Range(0, 4)));
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

            var truthReporter = GetTruthfulReporters(round.topic).RandomElement();
            round.reporterAndInputs[truthReporter] = correctAnswer;

            foreach (var reporter in reporters.Without(truthReporter).Shuffle().Take(reporterCount - 1)) {
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

            round.RaiseFinish();
            onChangeInternal?.Invoke(this);
        }

        internal void FinishGame() {
            hasWon = notDestructedTopics.Any();

            if (hasWon) {
                onWin?.Invoke();
            } else {
                onLose?.Invoke();
            }
        }
    }
}
