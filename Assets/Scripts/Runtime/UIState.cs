using System.Collections;
using UnityEngine;

namespace Runtime {
    abstract class UIState : MonoBehaviour {
        protected abstract IEnumerator WaitForCompletion();

        internal IEnumerator InstantiateAndWaitForCompletion() {
            var instance = Instantiate(this);
            yield return instance.WaitForCompletion();
            Destroy(instance.gameObject);
        }
    }
}
