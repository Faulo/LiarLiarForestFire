using System.Collections;

namespace Runtime {
    interface IUIState {
        IEnumerator WaitForCompletion();
    }
}
