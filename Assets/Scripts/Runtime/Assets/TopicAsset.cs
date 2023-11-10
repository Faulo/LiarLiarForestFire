using System;
using UnityEngine;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class TopicAsset : ScriptableObject {
        [SerializeField]
        Sprite announcementSprite;
        [SerializeField]
        Sprite[] backgroundProgressionSprites = Array.Empty<Sprite>();
    }
}
