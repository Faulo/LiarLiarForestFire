using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class InputAsset : ScriptableObject {
        [SerializeField]
        internal InputActionReference action = new();
        [SerializeField]
        Sprite reporterSprite;
    }
}
