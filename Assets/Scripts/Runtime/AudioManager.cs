using FMODUnity;
using UnityEngine;

namespace Runtime {
    sealed class AudioManager : MonoBehaviour {

        [SerializeField]
        EventReference BGM;

        void Start() {
            RuntimeManager.PlayOneShot(BGM);
        }
    }
}
