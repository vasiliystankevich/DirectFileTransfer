using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace ServerSTUNLibrary
{
    public class WebAppHost
    {
        public void Startup(Uri baseUri)
        {
            Task.Factory.StartNew(() =>
            {
                var are = new AutoResetEvent(false);
                using (WebApp.Start<Startup>(baseUri.ToString()))
                    are.WaitOne();
            });
        }
    }
}
