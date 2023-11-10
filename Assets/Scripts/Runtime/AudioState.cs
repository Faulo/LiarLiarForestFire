using FMODUnity;
using UnityEngine;

namespace Runtime {
    sealed class AudioState : MonoBehaviour {
        [SerializeField, ParamRef]
        string intensityParam = "Intensity";
        [SerializeField, ParamRef]
        string stateParam = "State";

        void OnEnable() {
            GameState.onChange += UpdateState;
            GameState.onWin += SetWin;
            GameState.onLose += SetLose;
        }

        void OnDisable() {
            GameState.onChange -= UpdateState;
            GameState.onWin -= SetWin;
            GameState.onLose -= SetLose;
        }

        public void SetWin() {
            RuntimeManager.StudioSystem.setParameterByNameWithLabel(stateParam, "Success");
        }

        public void SetLose() {
            RuntimeManager.StudioSystem.setParameterByNameWithLabel(stateParam, "Failure");
        }

        public void UpdateState(GameState state) {
            RuntimeManager.StudioSystem.setParameterByName(intensityParam, state.currentRound);
        }
    }
}
