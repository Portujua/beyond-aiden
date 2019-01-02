using System;
using System.Collections.Generic;
using System.Threading;

namespace Aiden
{
  class Program
  {
    private static void Main(string[] args)
    {
      App app = new App();
      Thread t = new Thread(app.Main);
      t.Start();
    }
  }
}
