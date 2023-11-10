using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Runtime.Screens {
    sealed class MainMenuScreen : MonoBehaviour, IScreen {
        enum State {
            Unknown,
            Start,
            Exit,
            SwitchLanguage,
            SwitchCredits,
        }
        [SerializeField]
        Transform buttonContainer;

        [Header("Prefabs")]
        [SerializeField]
        GameObject newGamePrefab;

        State state;
        Button startButton;

        void Start() {
            buttonContainer.InstantiateButton("Switch language", () => state = State.SwitchLanguage);
            startButton = buttonContainer.InstantiateButton("Start", () => state = State.Start);
            buttonContainer.InstantiateButton("Credits", () => state = State.SwitchCredits);
            buttonContainer.InstantiateButton("Exit", () => state = State.Exit);
        }

        public IEnumerator WaitForCompletion() {
            yield return new WaitUntil(() => startButton);

            do {
                state = State.Unknown;
                gameObject.SetActive(true);
                startButton.Select();

                yield return new WaitWhile(() => state == State.Unknown);

                switch (state) {
                    case State.SwitchLanguage:
                        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetOther(LocalizationSettings.SelectedLocale);
                        break;
                    case State.Start:
                        gameObject.SetActive(false);
                        yield return newGamePrefab.InstantiateAndWaitForCompletion();
                        break;
                }
            } while (state != State.Exit);
        }
    }
}
