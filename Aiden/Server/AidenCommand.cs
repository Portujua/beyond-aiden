using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeyondCore.Server;
using BeyondCore.Utils;
using BeyondCore.Exceptions;
using BeyondCore.Mail;

namespace Aiden.Server
{
  class AidenCommand : Command
  {
    public AidenCommand(string command) : base(command)
    {
      
    }

    public override string execute()
    {
      string result = "";

      switch (this.getCommandType()) {
        case (int)CommandType.ListProcesses:
          List<Process> processList = WindowsSystem.ListProcesses();

          result += "--------------------------" + Environment.NewLine;
          processList.ForEach((process) => {
            result += String.Format("{0} (hWnd: {1})", process.MainWindowTitle, process.MainWindowHandle) + Environment.NewLine;
          });
          result += "--------------------------" + Environment.NewLine;

          break;
        case (int)CommandType.WindowScreenshot:
          if (this.getParams().Length == 0) {
            throw new MissingCommandParameter();
          }

          List<string> files = WindowsSystem.WindowScreenshot(this.getParams()[0]);

          result += "--------------------------" + Environment.NewLine;
          result += String.Format("Screenshot list of '{0}':", this.getParams()[0]) + Environment.NewLine;

          files.ForEach((file) => {
            result += "- " + file + Environment.NewLine;
          });

          // Check if needed to send them in email
          if (this.getParams().Length > 1) {
            Mail mail = new Mail(MailService.smtpMail, result, String.Format("Screenshots of '{0}'", this.getParams()[0]));
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
