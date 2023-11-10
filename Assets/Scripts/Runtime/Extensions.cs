using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;
using UnityObject = UnityEngine.Object;

namespace Runtime {
    static class Extensions {
        internal static Button InstantiateButton(this Transform parent, string label, UnityAction onClick) {
            var button = UnityObject.Instantiate(GameManager.instance.buttonPrefab, parent);

            button.BindTo(label);
            new LocalizedString("Buttons", label).StringChanged += button.BindTo;

            button.onClick.AddListener(onClick);

            return button;
        }
        internal static void BindTo<T>(this GameObject gameObject, T model) {
            if (gameObject) {
                foreach (var receiver in gameObject.GetComponentsInChildren<IBindingReceiver<T>>()) {
                    receiver.Bind(model);
                }
            }
        }
        internal static void BindTo<T>(this Component component, T model) {
            if (component) {
                component.gameObject.BindTo(model);
            }
        }
        internal static IEnumerator InstantiateAndWaitForCompletion(this GameObject prefab) {
            var instance = UnityObject.Instantiate(prefab);

            foreach (var state in instance.GetComponents<IScreen>()) {
                yield return state.WaitForCompletion();
            }

            UnityObject.Destroy(instance);
        }
    }
}
