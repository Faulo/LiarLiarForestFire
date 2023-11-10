using System;
using System.Collections.Generic;
using Runtime.Assets;
using UnityRandom = UnityEngine.Random;

namespace Runtime {
    sealed record GameRound {
        static GameRound instance;

        static event Action<GameRound> onStartInternal;
        internal static event Action<GameRound> onStart {
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

        static event Action<GameRound> onFinishInternal;
        internal static event Action<GameRound> onFinish {
            add {
                onFinishInternal += value;

                if (instance is not null) {
                    value?.Invoke(instance);
                }
            }
            remove {
                onFinishInternal -= value;
            }
        }

        internal InputAsset correctAnswer;
        internal InputAsset guessedAnswer;
        internal TopicAsset topic;
        internal Dictionary<ReporterAsset, InputAsset> reporterAndInputs = new();

        internal int reporterCount => UnityRandom.Range(1, 4);

        internal bool isCorrect => correctAnswer == guessedAnswer;

        internal GameRound() {
            instance = this;
        }
        internal void RaiseStart() {
            onStartInternal?.Invoke(this);
        }
        internal void RaiseFinish() {
            onFinishInternal?.Invoke(this);
        }
    }
}
