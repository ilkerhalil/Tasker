using System;
using System.Collections.Concurrent;

namespace NullTask
{
    public class TestCollection
    {
        private static TestCollection _testCollection;
        public static TestCollection CreateTestCollection()
        {
            lock (_testCollection)
            {
                return _testCollection ?? new TestCollection();
            }

        }

        public ConcurrentBag<string> ConcurrentBag { get; }

        private TestCollection()
        {
            ConcurrentBag = new ConcurrentBag<string>();
        }
    }
}
