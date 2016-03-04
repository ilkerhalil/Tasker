using System;
using System.Collections.Concurrent;

namespace NullTask
{
    public class TestCollection
    {
        private static TestCollection _testCollection;
        static readonly object dummy = new object();
        public static TestCollection CreateTestCollection
        {
            get
            {
                lock (dummy)
                {
                    return _testCollection ?? new TestCollection();
                }
            }


        }

        public ConcurrentBag<string> ConcurrentBag { get; }

        private TestCollection()
        {
            ConcurrentBag = new ConcurrentBag<string>();
        }
    }
}
