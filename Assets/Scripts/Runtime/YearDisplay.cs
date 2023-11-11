using TMPro;
using UnityEngine;

namespace Runtime {
    sealed class YearDisplay : MonoBehaviour {
        [SerializeField]
        int firstYear = 2023;
        [SerializeField]
        string afterYear = ":";

        int year;
        void OnEnable() {
            year = firstYear;
            GameRound.onStart += HandleYear;
        }
        void OnDisable() {
            GameRound.onStart -= HandleYear;
        }

        void HandleYear(GameRound round) {
            year++;
            if (TryGetComponent<TMP_Text>(out var text)) {
                text.text = $"{year}{afterYear}";
            }
        }
    }
}
