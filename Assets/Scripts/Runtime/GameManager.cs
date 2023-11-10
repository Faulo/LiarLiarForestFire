using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class GameManager : MonoBehaviour {
        internal static GameManager instance { get; private set; }

        [Header("Prefabs")]
        [SerializeField]
        GameObject mainMenuPrefab;
        [SerializeField]
        internal Button buttonPrefab;

        void Awake() {
            instance = this;
        }

        IEnumerator Start() {
            yield return mainMenuPrefab.InstantiateAndWaitForCompletion();

            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
