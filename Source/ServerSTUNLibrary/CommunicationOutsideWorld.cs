using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonClasses;
using Microsoft.Practices.ObjectBuilder2;
using NoIP.DDNS;
using NoIP.DDNS.DTO;
using Open.Nat;

namespace ServerSTUNLibrary
{
    public class CommunicationOutsideWorld:IDisposable
    {
        public void OpenExternalPort(int privatePort, int publicPort, string description)
        {
            localHostPort = privatePort;
            var timeSpan = new TimeSpan(0, 0, 0, 30);
            var cancellationTokenSource = new CancellationTokenSource(timeSpan);
            natDiscoverer = new NatDiscoverer();
            natDevice = natDiscoverer
                .DiscoverDeviceAsync(PortMapper.Upnp, cancellationTokenSource)
                .IsCompleted();
            mapping = new Mapping(Protocol.Tcp, privatePort, publicPort, description);
            natDevice
                .CreatePortMapAsync(mapping)
                .IsCompleted();
        }

        public Uri CommunicationWithNoIP(string uriScheme, string user, string password, string userAgent)
        {
            var externalIp = natDevice.GetExternalIPAsync().IsCompleted();
            var noipClient = new Client(new UserAgent(userAgent));
            noipClient.Register(user, password);
            var noipZone = noipClient.GetZones().First();
            var noipHost = noipClient.GetHosts(noipZone).First();
            var noipHostUpdate = new Host(string.Format("{0}.{1}", noipHost.Name, noipZone.Name))
            {
                Address = externalIp
            };
            noipClient.UpdateHost(noipHostUpdate);
            return new Uri(string.Format("{0}://{1}:{2}", uriScheme, noipHostUpdate.Name, localHostPort));
        }

        public void Dispose()
        {
            if (mapping.IsNotNull())
                natDevice.DeletePortMapAsync(mapping).IsCompleted();
            NatDiscoverer.ReleaseAll();
        }
        private NatDevice natDevice { get; set; }
        private NatDiscoverer natDiscoverer { get; set; }

        private Mapping mapping { get; set; }

        private int localHostPort { get; set; }

    }
}
