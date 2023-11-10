using System;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Runtime {
    sealed class ReportersDisplay : MonoBehaviour {
        [SerializeField]
        GameObject[] reporterPrefabs = Array.Empty<GameObject>();

        void OnEnable() {
            GameRound.onStart += UpdateState;
        }

        void OnDisable() {
            GameRound.onStart -= UpdateState;
        }

        void UpdateState(GameRound round) {
            transform.Clear();

            int reporterCount = round.reporterCount;
            var prefab = reporterPrefabs[reporterCount];

            if (prefab) {
                var instance = Instantiate(prefab, transform);
                int i = 0;
                foreach (var (reporter, input) in round.reporterAndInputs) {
                    instance.transform.GetChild(i).BindTo((reporter, input));
                    i++;
                }
            }
        }
    }
}
