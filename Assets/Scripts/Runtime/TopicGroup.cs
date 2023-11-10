using System.Collections.Generic;
using System.Linq;
using Runtime.Assets;

namespace Runtime {
    sealed record TopicGroup {
        internal TopicAsset first;
        internal TopicAsset second;
        internal TopicAsset third;
        internal TopicAsset fourth;

        public TopicGroup(IList<TopicAsset> enumerable) {
            first = enumerable.ElementAtOrDefault(0);
            second = enumerable.ElementAtOrDefault(1);
            third = enumerable.ElementAtOrDefault(2);
            fourth = enumerable.ElementAtOrDefault(3);
        }

        internal bool Contains(TopicAsset asset) {
            return asset == first
                || asset == second
                || asset == third
                || asset == fourth;
        }
    }
}
