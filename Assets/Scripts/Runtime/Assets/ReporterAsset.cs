using UnityEngine;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class ReporterAsset : ScriptableObject {
        [SerializeField]
        Sprite sprite;
    }
}
