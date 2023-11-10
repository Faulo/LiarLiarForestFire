using UnityEngine;

namespace AssemblyCSharp {

    sealed class AudioManager : MonoBehaviour {

        [SerializeField] FMODUnity.EventReference BGM;


        private void Start() {
            FMODUnity.RuntimeManager.PlayOneShot(BGM);
        }

    }
}
