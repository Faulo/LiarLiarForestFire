using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class InputAsset : ScriptableObject {
        [SerializeField]
        internal InputActionReference action;
        [SerializeField]
        Sprite buttonSprite;
        [SerializeField]
        Sprite keyboardSprite;

        internal Sprite inputSprite => isKeyboard
            ? keyboardSprite
            : buttonSprite;

        bool isKeyboard {
            get {
                if (Gamepad.current is null) {
                    return true;
                }

                if (Keyboard.current is null) {
                    return false;
                }

                return Keyboard.current.lastUpdateTime > Gamepad.current.lastUpdateTime;
            }
        }
    }
}
