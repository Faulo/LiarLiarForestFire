using UnityEngine;

namespace Runtime {
    sealed class CameraSpaceCanvas : MonoBehaviour {
        void Start() {
            if (TryGetComponent<Canvas>(out var canvas)) {
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = Camera.main;
            }
        }
    }
}
