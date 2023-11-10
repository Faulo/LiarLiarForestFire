using System.Collections.Generic;
using System.Linq;
using Runtime.Assets;

namespace Runtime {
    sealed record TopicGroup {
        internal readonly TopicAsset first;
        internal readonly TopicAsset second;
        internal readonly TopicAsset third;
        internal readonly TopicAsset fourth;

        public TopicGroup(IEnumerable<TopicAsset> assets) {
            var list = assets
                .OrderBy(a => a.name)
                .ToList();
            first = list.ElementAtOrDefault(0);
            second = list.ElementAtOrDefault(1);
            third = list.ElementAtOrDefault(2);
            fourth = list.ElementAtOrDefault(3);
        }

        internal bool Contains(TopicAsset asset) {
            return asset == first
                || asset == second
                || asset == third
                || asset == fourth;
        }

        public override string ToString() => $"{first} | {second} | {third} | {fourth}";
    }
}
