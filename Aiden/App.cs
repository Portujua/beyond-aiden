using Aiden.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aiden
{
  class App
  {
    AidenServer server;

    public void Main()
    {
      server = new AidenServer();

      // Setup and start server
      server.Setup();
      server.Start();

      // Say hello!
      server.Greet();

      while (true) {
        // Main logic
        server.Send(Console.ReadLine());

        // Thread sleep
        Thread.Sleep(3000);
      }
    }
  }
}
