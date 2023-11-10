using System.Collections;
using UnityEngine;

namespace Runtime {
    abstract class UIState : MonoBehaviour {
        internal abstract IEnumerator WaitForCompletion();
    }
}
