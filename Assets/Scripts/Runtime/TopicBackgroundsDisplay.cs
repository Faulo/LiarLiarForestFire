using System.Collections.Generic;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime {
    sealed class TopicBackgroundsDisplay : MonoBehaviour {
        [SerializeField]
        GameObject topicBackgroundPrefab;

        void OnEnable() {
            GameState.onStart += UpdateState;
        }

        void OnDisable() {
            GameState.onStart -= UpdateState;
        }

        void UpdateState(GameState state) {
            SetBackgrounds(state.topics);
        }

        void SetBackgrounds(IEnumerable<TopicAsset> topics) {
            transform.Clear();

            foreach (var topic in topics) {
                var instance = Instantiate(topicBackgroundPrefab, transform);
                instance.BindTo(topic);
            }
        }
    }
}
