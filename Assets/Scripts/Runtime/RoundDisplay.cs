using UnityEngine;

namespace Runtime {
    sealed class RoundDisplay : MonoBehaviour {
        void OnEnable() {
            GameState.onChange += UpdateState;
        }

        void OnDisable() {
            GameState.onChange -= UpdateState;
        }

        public void UpdateState(GameState state) {
            string round = $"{state.currentRound}/{state.lastRound}";
            gameObject.BindTo(round);
        }
    }
}
