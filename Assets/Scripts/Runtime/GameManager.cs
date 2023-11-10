using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Runtime {
    sealed class GameManager : MonoBehaviour {
        internal static GameManager instance { get; private set; }

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

        void Awake() {
            instance = this;
        }

        public void OnEnable() {
            GameState.onWin += onWin.Invoke;
            GameState.onLose += onLose.Invoke;
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
