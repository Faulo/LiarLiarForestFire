using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class MaterialReceiver : MonoBehaviour, IBindingReceiver<Material> {
        public void Bind(Material model) {
            if (TryGetComponent<Image>(out var component)) {
                component.material = model;
            }
        }
    }
}
