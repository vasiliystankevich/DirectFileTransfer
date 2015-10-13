using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        [SetUp]
        public virtual void Init()
        {
            Container = new UnityContainer();
        }

        protected IUnityContainer Container { get; set; }
    }

    [TestFixture]
    public abstract class BaseTest<TInterface, TInterfaceImp>: BaseTest where TInterfaceImp: TInterface 
    {
        [SetUp]
        public override void Init()
        {
            base.Init();
            Container.RegisterType<TInterface, TInterfaceImp>();
            ObjectTested = Container.Resolve<TInterface>();
        }

        protected TInterface ObjectTested { get; set; }
    }

}
