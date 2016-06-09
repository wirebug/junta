using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient {
    class HubProxy {
        IHubProxy proxy;

        public HubProxy() {
            string url = @"http://localhost:58523/";
            var connection = new HubConnection(url);
            proxy = connection.CreateHubProxy("JuntaHub");

            connection.Start().Wait();
        }
    }
}
