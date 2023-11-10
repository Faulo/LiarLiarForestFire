using TMPro;
using UnityEngine;

namespace Runtime {
    sealed class TextMeshProReceiver : MonoBehaviour, IBindingReceiver<string> {

        TMP_Text component;

        void Awake() {
            if (TryGetComponent(out component)) {
                component.enabled = false;
            }
        }

        public void Bind(string model) {
            if (component) {
                component.text = model;
                component.enabled = !string.IsNullOrEmpty(model);
            }
        }
    }
}
