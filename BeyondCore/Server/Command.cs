using BeyondCore.Utils;
using BeyondCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondCore.Server
{
  public enum CommandType {
    ListProcesses, WindowScreenshot, SendEmail, ClearScreenshots
  }

  public class Command
  {
    private string command;
    private int commandType = -1;
    private string[] param;

    public Command(string command)
    {
      this.command = command;

      // Split the command
      // TO DO: Add regex for arguments
      string[] _command = command.Split(' ');
      List<string> _params = _command.ToList<string>();
      _params.RemoveAt(0);
      this.param = _params.ToArray();

      foreach (CommandType c in Enum.GetValues(typeof(CommandType)).Cast<CommandType>()) {
        try {
          if (Convert.ToInt32(_command[0]) == (int)c) {
            this.commandType = (int)c;
          }
        }
        catch (Exception) { }
      }

      if (commandType == -1) {
        throw new UnknownCommand();
      }
    }

    public virtual string execute()
    {
      // Child Implement
      throw new NotImplementedException("Command execute method not implemented");
    }

    public string getCommand()
    {
      return this.command;
    }

    public void setCommand(string command)
    {
      this.command = command;
    }

    public int getCommandType()
    {
      return this.commandType;
    }

    public void setCommandType(int commandType)
    {
      this.commandType = commandType;
    }

    public string[] getParams()
    {
      return this.param;
    }

    public void setParams(string[] param)
    {
      this.param = param;
    }
  }
}
