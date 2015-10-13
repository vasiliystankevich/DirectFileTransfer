using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Booth.Console.Properties;
using CommonClasses;
using NoIP.DDNS;
using NoIP.DDNS.DTO;
using Open.Nat;
using ServerSTUNLibrary;

namespace Booth.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var communicationOutsideWorld = new CommunicationOutsideWorld())
            {
                try
                {
                    #region Settings

                    var descriptionStunServer = Settings.Default.DescriptionStunServer;
                    var publicPort = Settings.Default.PublicPort;
                    var privatePort = Settings.Default.PrivatePort;
                    var uriScheme = Settings.Default.UriScheme;
                    var noipUser = Settings.Default.NoIpUser;
                    var noipPassword = Settings.Default.NoIpPassword;
                    var noipUserAgent = Settings.Default.NoIpUserAgent;

                    #endregion Settings

                    communicationOutsideWorld.OpenExternalPort(privatePort, publicPort, descriptionStunServer);
                    var localUri = communicationOutsideWorld.CommunicationWithNoIP(uriScheme, noipUser, noipPassword,
                        noipUserAgent);
                    var host = new WebAppHost();
                    host.Startup(localUri);
                    System.Console.WriteLine("WebAppHost Startup....");
                    System.Console.WriteLine("Press any key for exit...");
                    System.Console.ReadKey();
                }
                catch (Exception exception)
                {
                    System.Console.WriteLine("Exception: ");
                    System.Console.WriteLine(exception);
                    Globalton<Logger<Program>>.Instance.Log.Error(exception);
                    System.Console.WriteLine("Press any key for exit...");
                    System.Console.ReadKey();
                }
            }
        }
    }
}
