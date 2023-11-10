using FMODUnity;
using UnityEngine;

namespace Runtime {
    sealed class AudioState : MonoBehaviour {
        [SerializeField]
        ParamRef intensityParam = new();

        void OnEnable() {
            GameState.onChange += UpdateState;
        }

        void OnDisable() {
            GameState.onChange -= UpdateState;
        }

        public void UpdateState(GameState state) {
            intensityParam.Value = state.currentRound;
        }
    }
}
