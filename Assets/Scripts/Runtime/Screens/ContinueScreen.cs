using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Screens {
    sealed class ContinueScreen : MonoBehaviour, IScreen {
        enum State {
            Unknown,
            Start
        }
        [SerializeField]
        Transform buttonContainer;
        [SerializeField]
        string continueScreenText = "Continue";

        [Header("Prefabs")]
        [SerializeField]
        GameObject nextScreenPrefab;

        State state;

        Button continueButton;

        void Start() {
            continueButton = buttonContainer.InstantiateButton(continueScreenText, () => state = State.Start);
        }

        public IEnumerator WaitForCompletion() {
            yield return new WaitUntil(() => continueButton);

            continueButton.Select();

            yield return new WaitWhile(() => state == State.Unknown);

            switch (state) {
                case State.Start:
                    if (nextScreenPrefab) {
                        gameObject.SetActive(false);
                        yield return nextScreenPrefab.InstantiateAndWaitForCompletion();
                    }

                    break;
            }
        }
    }
}
