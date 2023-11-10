using System;
using System.Linq;
using UnityEngine;

namespace Runtime.Assets {
    [CreateAssetMenu]
    sealed class ReporterAsset : ScriptableObject {
        [SerializeField]
        internal Sprite sprite;
        [SerializeField]
        TopicAsset[] truthTopics = Array.Empty<TopicAsset>();

        internal bool IsTruthfulAbout(TopicAsset topic) => truthTopics.Contains(topic);
    }
}
