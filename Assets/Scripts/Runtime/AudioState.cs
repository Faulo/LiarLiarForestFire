using FMODUnity;
using UnityEngine;

namespace Runtime {
    sealed class AudioState : MonoBehaviour {
        [SerializeField, ParamRef]
        string intensityParam = "Intensity";

        void OnEnable() {
            GameState.onChange += UpdateState;
        }

        void OnDisable() {
            GameState.onChange -= UpdateState;
        }

        public void UpdateState(GameState state) {
            RuntimeManager.StudioSystem.setParameterByName(intensityParam, state.currentRound);
        }
    }
}
