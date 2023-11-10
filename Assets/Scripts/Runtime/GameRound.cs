using Runtime.Assets;

namespace Runtime {
    sealed record GameRound {
        internal InputAsset correctAnswer;
        internal InputAsset guessedAnswer;
        internal TopicAsset topic;

        internal bool isCorrect => correctAnswer == guessedAnswer;
    }
}
