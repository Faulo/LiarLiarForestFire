using System.Collections;
using UnityEngine;

namespace Runtime {
    sealed class MainMenuState : MonoBehaviour, IUIState {
        enum State {
            Unknown,
            Start,
            Exit
        }
        [SerializeField]
        Transform buttonContainer;

        [Header("Prefabs")]
        [SerializeField]
        GameObject newGamePrefab;

        State state;

        void Start() {
            buttonContainer.InstantiateButton("Start", () => state = State.Start, true);
            buttonContainer.InstantiateButton("Exit", () => state = State.Exit);
        }

        public IEnumerator WaitForCompletion() {
            do {
                yield return new WaitWhile(() => state == State.Unknown);

                switch (state) {
                    case State.Start:
                        gameObject.SetActive(false);
                        yield return newGamePrefab.InstantiateAndWaitForCompletion();
                        gameObject.SetActive(true);
                        state = State.Unknown;
                        break;
                }
            } while (state != State.Exit);
        }
    }
}
