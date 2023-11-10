using UnityEngine;

namespace Runtime {
    sealed class AudioIntensity : MonoBehaviour {
        void OnEnable() {
            GameState.onChange += UpdateState;
        }

        void OnDisable() {
            GameState.onChange -= UpdateState;
        }

        public void UpdateState(GameState state) {
            int intensity = state.currentRound / state.lastRound;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Intensity", intensity);
        }
    }
}
