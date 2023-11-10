using System;
using UnityEngine;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class TopicAsset : ScriptableObject {
        [SerializeField]
        internal Sprite announcementSprite;
        [SerializeField]
        Sprite[] backgroundProgressionSprites = Array.Empty<Sprite>();

        internal Sprite GetBackgroundProgressionSprite(GameState state) {
            return backgroundProgressionSprites[0];
        }
    }
}
