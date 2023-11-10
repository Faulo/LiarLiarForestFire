using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime {
    sealed class PlayAudio : MonoBehaviour {

        [SerializeField, FormerlySerializedAs("BGM")]
        EventReference reference;

        void PlayOneShot() {
            RuntimeManager.PlayOneShot(reference);
        }
    }
}
