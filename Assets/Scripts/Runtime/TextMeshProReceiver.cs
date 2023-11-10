using TMPro;
using UnityEngine;

namespace Runtime {
    sealed class TextMeshProReceiver : MonoBehaviour, IBindingReceiver<string> {
        public void Bind(string model) {
            if (TryGetComponent<TMP_Text>(out var component)) {
                component.text = model;
            }
        }
    }
}
