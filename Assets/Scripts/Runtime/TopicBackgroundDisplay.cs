using Runtime.Assets;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class TopicBackgroundDisplay : MonoBehaviour, IBindingReceiver<TopicAsset> {
        [SerializeField]
        Image image;
        [SerializeField]
        TopicAsset topic;

        void OnDisable() {
            GameState.onChange -= UpdateState;
        }

        void UpdateState(GameState state) {
            if (topic) {
                image.sprite = topic.GetBackgroundProgressionSprite(state);
            }
        }

        public void Bind(TopicAsset model) {
            topic = model;
            GameState.onChange -= UpdateState;
            GameState.onChange += UpdateState;
        }
    }
}
