using UnityEngine;

namespace Runtime {
    sealed class TopicDisplay : MonoBehaviour {
        void OnEnable() {
            GameRound.onStart += UpdateState;
            GameManager.onStatusChange += UpdateStatus;
        }

        void OnDisable() {
            GameRound.onStart -= UpdateState;
            GameManager.onStatusChange -= UpdateStatus;
        }

        public void UpdateState(GameRound round) {
            gameObject.BindTo(default(string));
            gameObject.BindTo(round.topic.announcementSprite);
        }

        public void UpdateStatus(string status) {
            gameObject.BindTo(status);
            gameObject.BindTo(default(Sprite));
        }
    }
}
