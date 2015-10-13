using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using ServerSTUNLibrary.Models;

namespace ServerSTUNLibrary.Modules
{
    public class Home : NancyModule
    {
        public Home()
        {
            Get["/"] = IndexGet;
        }

        private dynamic IndexGet(dynamic arg)
        {
            var data = FileUriDataModel.GetUriList(Request.Url);
            return View["Index", data];
        }
    }
}
