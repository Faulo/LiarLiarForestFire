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
        static string status {
            get => m_status;
            set {
                if (m_status != value) {
                    m_status = value;
                    onStatusChange?.Invoke(value);
                }
            }
        }
        static LocalizedString m_localizedStatus;
        internal static LocalizedString localizedStatus {
            set {
                if (m_localizedStatus != value) {
                    if (m_localizedStatus is not null) {
                        m_localizedStatus.StringChanged -= SetStatus;
                    }

                    m_localizedStatus = value;
                    status = "";

                    if (m_localizedStatus is not null) {
                        m_localizedStatus.StringChanged += SetStatus;
                    }
                }
            }
        }
        static void SetStatus(string status) {
            GameManager.status = status;
        }

        [Header("Prefabs")]
        [SerializeField]
        GameObject mainMenuPrefab;
        [SerializeField]
        internal Button buttonPrefab;

        [Header("Events")]
        [SerializeField]
        UnityEvent onStart = new();

        void Awake() {
            instance = this;
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
