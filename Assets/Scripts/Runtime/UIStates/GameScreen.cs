using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime {
    sealed record GameState {
        internal int currentRound = 1;
        internal int lastRound = 10;
        internal bool hasWon;

        internal bool isRunning => currentRound <= lastRound;
    }
    sealed class GameScreen : MonoBehaviour, IScreen {
        enum Input {
            Unknown,
            A,
            B,
            X,
            Y
        }

        [Header("Prefabs")]
        [SerializeField]
        GameObject winPrefab;
        [SerializeField]
        GameObject losePrefab;

        Input input;

        void Start() {
        }

        void Update() {
            if (Keyboard.current.digit1Key.wasPressedThisFrame) {
                input = Input.A;
            }

            if (Keyboard.current.digit2Key.wasPressedThisFrame) {
                input = Input.B;
            }

            if (Keyboard.current.digit3Key.wasPressedThisFrame) {
                input = Input.X;
            }

            if (Keyboard.current.digit4Key.wasPressedThisFrame) {
                input = Input.Y;
            }
        }

        public IEnumerator WaitForCompletion() {
            var state = new GameState();

            while (state.isRunning) {
                input = Input.Unknown;

                yield return new WaitWhile(() => input == Input.Unknown);

                Debug.Log(input);

                state.currentRound++;
            }

            if (state.hasWon) {
                yield return winPrefab.InstantiateAndWaitForCompletion();
            } else {
                yield return losePrefab.InstantiateAndWaitForCompletion();
            }
        }
    }
}
