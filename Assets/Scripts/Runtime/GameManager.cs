using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Runtime {
    sealed class GameManager : MonoBehaviour {
        internal static GameManager instance { get; private set; }
        internal static event Action<string> onStatusChange;

        static string m_status;
        internal static string status {
            get => m_status;
            set {
                if (m_status != value) {
                    m_status = value;
                    onStatusChange?.Invoke(value);
                }
            }
        }

        [Header("Prefabs")]
        [SerializeField]
        GameObject mainMenuPrefab;
        [SerializeField]
        internal Button buttonPrefab;

        [Header("Events")]
        [SerializeField]
        UnityEvent onStart = new();
        [SerializeField]
        UnityEvent onWin = new();
        [SerializeField]
        UnityEvent onLose = new();

        [Header("Loca")]
        [SerializeField]
        LocalizedString introductionText = new();
        [SerializeField]
        LocalizedString winText = new();
        [SerializeField]
        LocalizedString loseText = new();

        void Awake() {
            instance = this;
        }

        public void OnEnable() {
            GameState.onWin += HandleWin;
            GameState.onLose += HandleLose;
        }

        void HandleWin() {
            status = winText.GetLocalizedString();
            onWin.Invoke();
        }

        void HandleLose() {
            status = loseText.GetLocalizedString();
            onLose.Invoke();
        }

        public void OnDisable() {
            GameState.onWin -= onWin.Invoke;
            GameState.onLose -= onLose.Invoke;
        }

        IEnumerator Start() {
            onStart.Invoke();

            yield return mainMenuPrefab.InstantiateAndWaitForCompletion();

            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
