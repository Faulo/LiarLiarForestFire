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
            transform.Clear();

            foreach (var topic in state.topics) {
                var instance = Instantiate(topicBackgroundPrefab, transform);
                instance.BindTo(topic);
            }
        }
    }
}
