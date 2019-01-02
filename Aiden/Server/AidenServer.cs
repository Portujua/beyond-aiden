using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeyondCore.Server;

namespace Aiden.Server
{
  public class AidenServer : BeyondServer
  {
    public void Setup()
    {
      // Setup the process function
      this.__Setup((StreamReader reader, StreamWriter writer) => {
        string received = (string)reader.ReadLine();

        if (!received.Contains("Hello Jodie, I am")) {
          try {
            AidenCommand c = new AidenCommand(received);
            writer.WriteLine(c.execute());
          }
          catch (Exception ex) {
            writer.WriteLine(String.Format("Error executing the command: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
          }
        }

        return 0;
      });      
    }
  }
}
