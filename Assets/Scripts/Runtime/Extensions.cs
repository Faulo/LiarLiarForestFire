using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;

namespace Runtime {
    static class Extensions {
        internal static Button InstantiateButton(this Transform parent, string label, UnityAction onClick, bool select = false) {
            var button = UnityObject.Instantiate(GameManager.instance.buttonPrefab, parent);
            button.BindTo(label);
            button.onClick.AddListener(onClick);

            if (select) {
                EventSystem.current.SetSelectedGameObject(button.gameObject);
            }

            return button;
        }
        internal static void BindTo<T>(this GameObject gameObject, T model) {
            foreach (var receiver in gameObject.GetComponentsInChildren<IBindingReceiver<T>>()) {
                receiver.Bind(model);
            }
        }
        internal static void BindTo<T>(this Component component, T model) {
            component.gameObject.BindTo(model);
        }
        internal static IEnumerator InstantiateAndWaitForCompletion(this GameObject prefab) {
            var instance = UnityObject.Instantiate(prefab);

            foreach (var state in instance.GetComponents<IUIState>()) {
                yield return state.WaitForCompletion();
            }

            UnityObject.Destroy(instance);
        }
    }
}
