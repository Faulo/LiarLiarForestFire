using Runtime.Assets;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime {
    sealed class ReporterDisplay : MonoBehaviour, IBindingReceiver<(ReporterAsset reporter, InputAsset input)> {
        [SerializeField]
        Image reporterImage;
        [SerializeField]
        Image inputImage;

        public void Bind((ReporterAsset reporter, InputAsset input) model) {
            reporterImage.sprite = model.reporter.sprite;
            inputImage.sprite = model.input.inputSprite;
        }
    }
}
