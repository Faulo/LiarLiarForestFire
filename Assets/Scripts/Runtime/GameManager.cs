using System.Collections;
using FMODUnity;
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

        [Header("Music")]
        [SerializeField]
        EventReference BGM;

        void Awake() {
            instance = this;
            RuntimeManager.PlayOneShot(BGM);
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
