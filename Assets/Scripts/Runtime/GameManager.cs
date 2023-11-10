using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class GameManager : MonoBehaviour {
        internal static GameManager instance { get; private set; }

        [SerializeField]
        UIState mainMenuState;

        [Space]
        [SerializeField]
        internal Button buttonPrefab;

        void Awake() {
            instance = this;
        }

        IEnumerator Start() {
            yield return mainMenuState.InstantiateAndWaitForCompletion();

            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
