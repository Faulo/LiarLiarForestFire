using System;
using System.Collections;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Screens {
    sealed class GameScreen : MonoBehaviour, IScreen {
        [SerializeField]
        InputActionAsset actionsAsset;
        InputActionAsset actionsInstance;
        [SerializeField, Range(0, 100)]
        int numberOfRounds = 10;
        [SerializeField, Range(0, 10)]
        float waitAfterPress = 1;
        [SerializeField]
        InputAsset[] inputs = Array.Empty<InputAsset>();
        [SerializeField]
        TopicAsset[] topics = Array.Empty<TopicAsset>();
        [SerializeField]
        ReporterAsset[] reporters = Array.Empty<ReporterAsset>();

        [Header("Prefabs")]
        [SerializeField]
        GameObject winPrefab;
        [SerializeField]
        GameObject losePrefab;

        public IEnumerator WaitForCompletion() {
            var state = new GameState(numberOfRounds, topics, reporters);

            while (state.isRunning) {
                yield return CreateRound(state);
                yield return Wait.forSeconds[waitAfterPress];
            }

            gameObject.SetActive(false);

            if (state.hasWon) {
                yield return winPrefab.InstantiateAndWaitForCompletion();
            } else {
                yield return losePrefab.InstantiateAndWaitForCompletion();
            }
        }

        IEnumerator CreateRound(GameState state) {
            var round = state.StartRound(inputs);

            actionsInstance = Instantiate(actionsAsset);

            foreach (var input in inputs) {
                actionsInstance.FindAction(input.action.name).performed += _ => round.guessedAnswer = input;
            }

            actionsInstance.Enable();

            yield return new WaitUntil(() => round.guessedAnswer);

            Destroy(actionsInstance);

            state.FinishRound(round);
        }
    }
}
