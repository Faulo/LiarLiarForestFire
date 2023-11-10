using System;

namespace Runtime {
    sealed record GameState {
        internal static event Action<GameState> onChange;

        int m_currentRound;
        internal int currentRound {
            get => m_currentRound;
            set {
                if (m_currentRound != value) {
                    m_currentRound = value;
                    onChange?.Invoke(this);
                }
            }
        }
        internal int lastRound = 10;
        internal bool hasWon;

        internal bool isRunning => currentRound <= lastRound;
    }
}
