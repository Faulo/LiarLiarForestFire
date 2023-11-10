using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class InputAsset : ScriptableObject {
        [SerializeField]
        internal InputActionReference action;
        [SerializeField]
        internal Sprite buttonSprite;
    }
}
