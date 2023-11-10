using UnityEngine;

namespace Runtime {
    sealed class StatusDisplay : MonoBehaviour {
        [SerializeField]
        GameObject fallbackObject;
        [SerializeField]
        GameObject targetObject;

        void OnEnable() {
            GameManager.onStatusChange += UpdateState;
        }

        void OnDisable() {
            GameManager.onStatusChange -= UpdateState;
        }

        public void UpdateState(string status) {
            if (targetObject) {
                targetObject.BindTo(status);
            }

            if (fallbackObject) {
                fallbackObject.SetActive(string.IsNullOrEmpty(status));
            }
        }
    }
}
