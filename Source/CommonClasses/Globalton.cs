using System;
using NUnit.Framework;
using Tests;

namespace CommonClasses
{
    namespace Tests
    {
        public class FakeClassForGlobalton
        {
        }

        public class GlobaltonTest : BaseTest
        {
            [Test]
            public void CreatedObject()
            {
                var obj = Globalton<FakeClassForGlobalton>.Instance;
                Assert.AreEqual(obj, Globalton<FakeClassForGlobalton>.Instance);
            }
        }
    }

    public class Globalton<T> where T : class, new()
    {
        protected Globalton() { }

        protected sealed class GlobaltonCreator
        {
            private static readonly Lazy<T> instance = new Lazy<T>(() => new T());
            public static T Instance { get { return instance.Value; } }
        }

        public static T Instance
        {
            get { return GlobaltonCreator.Instance; }
        }

    }
}
