using System;
using FMODUnity;
using UnityEngine;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class TopicAsset : ScriptableObject {
        [Header("Audio")]
        [SerializeField]
        EventReference successEvent = new();
        [SerializeField]
        EventReference failureEvent = new();

        [Header("Visuals")]
        [SerializeField]
        internal Sprite announcementSprite;
        [SerializeField]
        Sprite[] backgroundProgressionSprites = Array.Empty<Sprite>();

        internal Sprite GetBackgroundProgressionSprite(GameState state) {
            return backgroundProgressionSprites[0];
        }

        internal void RaiseEvent(bool isCorrect) {
            if (isCorrect) {
                if (!successEvent.IsNull) {
                    RuntimeManager.PlayOneShot(successEvent);
                }
            } else {
                if (!failureEvent.IsNull) {
                    RuntimeManager.PlayOneShot(failureEvent);
                }
            }
        }
    }
}
