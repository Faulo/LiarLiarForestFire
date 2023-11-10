using System.Collections;
using UnityEngine;

namespace Runtime {
    sealed class MainMenuState : UIState {
        enum State {
            Unknown,
            Start,
            Exit
        }
        [SerializeField]
        Transform buttonContainer;
        [SerializeField]
        UIState newGameState;

        State state;

        void Start() {
            buttonContainer.InstantiateButton("Start", () => state = State.Start, true);
            buttonContainer.InstantiateButton("Exit", () => state = State.Exit);
        }

        protected override IEnumerator WaitForCompletion() {
            yield return new WaitWhile(() => state == State.Unknown);

            switch (state) {
                case State.Start:
                    yield return newGameState.InstantiateAndWaitForCompletion();
                    break;
                case State.Exit:
                    break;
            }
        }
    }
}
