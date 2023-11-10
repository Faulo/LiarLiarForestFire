using System.Collections;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime {
    sealed class InputDisplay : MonoBehaviour {
        [SerializeField]
        InputAsset asset;

        IEnumerator Start() {
            while (true) {
                gameObject.BindTo(asset.inputSprite);
                yield return Wait.forSeconds[1];
            }
        }
    }
}
