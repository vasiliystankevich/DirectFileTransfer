using System;
using System.IO;
using NUnit.Framework;
using log4net;
using log4net.Config;
using Tests;

namespace CommonClasses
{
    namespace Tests
    {
        public class FakeLog
        {
        }

        public class LogTest : BaseTest
        {
            [Test]
            public void Logging()
            {
                try
                {
                    Globalton<Logger<FakeLog>>.Instance.Log.Info("test log");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }

    public class Logger<Type>
    {
        public Logger()
        {
            var configFile = new FileInfo("log4net.config");
            if (!configFile.Exists) throw new FileNotFoundException("file not found - log4net.config");
                XmlConfigurator.Configure(configFile);
        }

        private readonly ILog _log = LogManager.GetLogger(typeof(Type));
        public ILog Log { get { return _log; } }
    }
}
