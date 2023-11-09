using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Runtime {
    sealed class ApplicationInfo : MonoBehaviour {
        TMP_Text component;
        string template;

        void Awake() {
            if (TryGetComponent(out component)) {
                template = component.text;
            }
        }

        void Start() {
            UpdateText();
        }

        void OnEnable() {
        }

        void OnDisable() {
        }

        IEnumerable<(string, object)> tokens {
            get {
                yield return ("Application.name", Application.productName);
                yield return ("Application.version", Application.version);
            }
        }

        void UpdateText() {
            if (component) {
                component.text = ReplaceTokens(template);
            }
        }

        string ReplaceTokens(string text) {
            foreach (var (key, value) in tokens) {
                text = text.Replace($"{{{key}}}", value.ToString());
            }

            return text;
        }
    }
}
