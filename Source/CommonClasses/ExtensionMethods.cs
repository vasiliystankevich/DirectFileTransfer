using NUnit.Framework;
using Tests;

namespace CommonClasses
{
    namespace Tests
    {
        internal class ExtensionMethodsTest:BaseTest
        {
            [Test]
            public void IsNullObjectTest()
            {
                object v = null;
                Assert.True(v.IsNull());
            }
        
            [Test]
            public void IsNotNullObjectTest()
            {
                var v = new object();
                Assert.True(v.IsNotNull());
            }
        }
    }

    public static class ExtensionMethods
    {
        public static bool IsNull<T>(this T obj) where T : class
        {
            return obj == null;
        }

        public static bool IsNull<T>(this T? obj) where T : struct
        {
            return !obj.HasValue;
        }
    
        public static bool IsNotNull<T>(this T obj) where T : class
        {
            return obj != null;
        }

        public static bool IsNotNull<T>(this T? obj) where T : struct
        {
            return obj.HasValue;
        }
    }
}
