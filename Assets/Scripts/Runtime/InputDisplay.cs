using System.Collections;
using Runtime.Assets;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime {
    sealed class InputDisplay : MonoBehaviour, IBindingReceiver<InputAsset> {
        [SerializeField]
        InputAsset asset;

        IEnumerator Start() {
            while (true) {
                if (asset) {
                    gameObject.BindTo(asset.inputSprite);
                }

                yield return Wait.forSeconds[1];
            }
        }

        public void Bind(InputAsset model) => asset = model;
    }
}
