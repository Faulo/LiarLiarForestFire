using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        Button startButton;

        void Start() {
            startButton = buttonContainer.InstantiateButton("Start", () => state = State.Start);
            buttonContainer.InstantiateButton("Exit", () => state = State.Exit);
        }

        public IEnumerator WaitForCompletion() {
            yield return new WaitUntil(() => startButton);

            do {
                startButton.Select();

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
