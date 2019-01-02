using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Beyond_Aiden
{
  class Server
  {
    // Server address
    public static string JODIE = "127.0.0.1";
    public static int JODIE_PORT = 19500;

    Thread t;
    TcpListener listener;
    TcpClient client;
    string localIP;

    public Server()
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

    public void Setup()
    {
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
        listener = new TcpListener(IPAddress.Parse(Server.JODIE), Server.JODIE_PORT);
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

        string received = (string)reader.ReadLine();
        //Console.WriteLine("Im hearing: {0}", received);

        if (!received.Contains("Hello Jodie, I am")) {
          try {
            Command c = new Command(received);
            writer.WriteLine(c.execute());
          }
          catch (Exception ex) {
            writer.WriteLine(String.Format("Error executing the command: {0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
          }
        }

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
      client = new TcpClient(Server.JODIE, Server.JODIE_PORT);

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
