using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class SpriteReceiver : MonoBehaviour, IBindingReceiver<Sprite> {

        Image component;

        void Awake() {
            if (TryGetComponent(out component)) {
                component.enabled = false;
            }
        }

        public void Bind(Sprite model) {
            if (component) {
                component.sprite = model;
                component.enabled = model;
            }
        }
    }
}
