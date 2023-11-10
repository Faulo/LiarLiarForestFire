using UnityEngine;

namespace Runtime {
    sealed class TopicDisplay : MonoBehaviour {
        void OnEnable() {
            GameRound.onStart += UpdateState;
        }

        void OnDisable() {
            GameRound.onStart -= UpdateState;
        }

        public void UpdateState(GameRound round) {
            gameObject.BindTo(round.topic.announcementSprite);
        }
    }
}
