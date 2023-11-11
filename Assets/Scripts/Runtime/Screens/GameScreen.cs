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
        float waitAfterSign = 0.5f;
        [SerializeField, Range(0, 10)]
        float waitAfterSuccess = 1;
        [SerializeField, Range(0, 10)]
        float waitAfterFailure = 1;
        [SerializeField, Range(0, 10)]
        float waitAfterGame = 1;
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
        [SerializeField]
        GameObject signPrefab;

        public IEnumerator WaitForCompletion() {
            var state = new GameState(numberOfRounds, topics, reporters);

            while (state.isRunning) {
                yield return ProcessRound(state);
            }

            state.FinishGame();

            yield return Wait.forSeconds[waitAfterGame];

            gameObject.SetActive(false);

            if (state.hasWon) {
                yield return winPrefab.InstantiateAndWaitForCompletion();
            } else {
                yield return losePrefab.InstantiateAndWaitForCompletion();
            }
        }

        IEnumerator ProcessRound(GameState state) {
            var round = state.StartRound(inputs);

            actionsInstance = Instantiate(actionsAsset);

            foreach (var input in inputs) {
                actionsInstance.FindAction(input.action.name).performed += _ => round.guessedAnswer = input;
            }

            actionsInstance.Enable();

            yield return new WaitUntil(() => round.guessedAnswer);

            Destroy(actionsInstance);

            var instance = Instantiate(signPrefab);
            instance.BindTo(round.guessedAnswer);

            yield return Wait.forSeconds[waitAfterSign];

            state.FinishRound(round);

            yield return Wait.forSeconds[round.isCorrect ? waitAfterSuccess : waitAfterFailure];

            Destroy(instance);
        }
    }
}
