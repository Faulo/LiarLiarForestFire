using System.Collections;
using UnityEngine;

namespace Runtime {
    sealed class IntroductionState : MonoBehaviour, IUIState {
        enum State {
            Unknown,
            Start
        }
        [SerializeField]
        Transform buttonContainer;

        [Header("Prefabs")]
        [SerializeField]
        GameObject newGamePrefab;

        State state;

        void Start() {
            buttonContainer.InstantiateButton("Continue", () => state = State.Start, true);
        }

        public IEnumerator WaitForCompletion() {
            yield return new WaitWhile(() => state == State.Unknown);

            switch (state) {
                case State.Start:
                    yield return newGamePrefab.InstantiateAndWaitForCompletion();
                    break;
            }
        }
    }
}
