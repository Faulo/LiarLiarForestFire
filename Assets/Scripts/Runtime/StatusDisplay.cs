using UnityEngine;

namespace Runtime {
    sealed class StatusDisplay : MonoBehaviour {
        void OnEnable() {
            GameManager.onStatusChange += UpdateState;
        }

        void OnDisable() {
            GameManager.onStatusChange -= UpdateState;
        }

        public void UpdateState(string status) {
            gameObject.BindTo(status);
        }
    }
}
