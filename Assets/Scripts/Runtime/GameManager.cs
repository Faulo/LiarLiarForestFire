using System.Collections;
using UnityEngine;

namespace Runtime {
    sealed class GameManager : MonoBehaviour {
        [SerializeField]
        UIState mainMenuState;

        IEnumerator Start() {
            yield return Instantiate(mainMenuState).WaitForCompletion();
        }
    }
}
