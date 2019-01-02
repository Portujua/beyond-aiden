using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeyondCore.Server
{
  public class BeyondServer
  {
    // Server address
    public static string JODIE = "127.0.0.1";
    public static int JODIE_PORT = 19500;

    Thread t;
    TcpListener listener;
    TcpClient client;
    string localIP;

    Func<StreamReader, StreamWriter, int> processFunction;

    public BeyondServer()
    {
    }

    public void Greet()
    {
      // Get my IP
      using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)) {
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint endpoint = socket.LocalEndPoint as IPEndPoint;
        localIP = endpoint.Address.ToString();
      }

      // Greet Jodie
      this.Send(String.Format("Hello Jodie, I am {0}", localIP));
    }

    public void __Setup(Func<StreamReader, StreamWriter, int> processFunction)
    {
      this.processFunction = processFunction;
      t = new Thread(__Start);
      t.IsBackground = true;
    }

    public void Start()
    {
      t.Start();
    }

    public void __Start()
    {
      listener = null;

      try {
        listener = new TcpListener(IPAddress.Parse(BeyondServer.JODIE), BeyondServer.JODIE_PORT);
        listener.Start();

        while (true) {
          TcpClient client = listener.AcceptTcpClient();
          Thread t = new Thread(ProcessRequests);
          t.Start(client);
        }
      }
      catch (Exception ex) {
        Console.WriteLine(ex.Message);
      }
    }

    public void ProcessRequests(object argument)
    {
      TcpClient client = (TcpClient)argument;

      try {
        StreamReader reader = new StreamReader(client.GetStream());
        StreamWriter writer = new StreamWriter(client.GetStream());

        this.processFunction(reader, writer);

        writer.Close();
        reader.Close();
      }
      catch (Exception) {

      }
      finally {
        client.Close();
      }
    }

    public void Send(object command)
    {
      client = new TcpClient(BeyondServer.JODIE, BeyondServer.JODIE_PORT);

      StreamReader reader = new StreamReader(client.GetStream());
      StreamWriter writer = new StreamWriter(client.GetStream());

      writer.WriteLine(command);
      writer.Flush();

      string answer = reader.ReadToEnd();

      if (!String.IsNullOrEmpty(answer) && !String.IsNullOrWhiteSpace(answer)) {
        Console.WriteLine("Aiden said: {0}{1}", Environment.NewLine, answer);
      }

      writer.Close();
      reader.Close();
      client.Close();
    }
  }
}
