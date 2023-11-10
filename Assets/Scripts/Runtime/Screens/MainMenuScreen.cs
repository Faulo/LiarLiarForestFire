using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Screens {
    sealed class MainMenuScreen : MonoBehaviour, IScreen {
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
                gameObject.SetActive(true);
                startButton.Select();

                yield return new WaitWhile(() => state == State.Unknown);

                switch (state) {
                    case State.Start:
                        gameObject.SetActive(false);
                        yield return newGamePrefab.InstantiateAndWaitForCompletion();
                        state = State.Unknown;
                        break;
                }
            } while (state != State.Exit);
        }
    }
}
