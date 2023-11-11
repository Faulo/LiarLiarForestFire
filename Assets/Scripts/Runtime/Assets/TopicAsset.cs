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
        internal Material announcementMaterial;
        [SerializeField]
        Sprite[] backgroundProgressionSprites = Array.Empty<Sprite>();
        internal int maxDestruction => backgroundProgressionSprites.Length - 1;

        internal Sprite GetBackgroundProgressionSprite(GameState state) {
            int i = state.GetDestruction(this);
            i = Mathf.Clamp(i, 0, maxDestruction);
            return backgroundProgressionSprites[i];
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
