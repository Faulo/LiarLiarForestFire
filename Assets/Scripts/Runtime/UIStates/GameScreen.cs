﻿using System.Collections;
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
        [SerializeField]
        InputActionAsset actionsAsset;
        InputActionAsset actionsInstance;

        [Header("Prefabs")]
        [SerializeField]
        GameObject winPrefab;
        [SerializeField]
        GameObject losePrefab;

        Input input;

        void OnEnable() {
            actionsInstance = Instantiate(actionsAsset);
            actionsInstance.FindAction(nameof(Input.A)).performed += _ => input = Input.A;
            actionsInstance.FindAction(nameof(Input.B)).performed += _ => input = Input.B;
            actionsInstance.FindAction(nameof(Input.X)).performed += _ => input = Input.X;
            actionsInstance.FindAction(nameof(Input.Y)).performed += _ => input = Input.Y;
            actionsInstance.Enable();
        }
        void OnDisable() {
            Destroy(actionsInstance);
        }

        void Start() {
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
