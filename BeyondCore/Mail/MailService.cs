using System;
using System.Net.Mail;

namespace BeyondCore.Mail
{
  public static class MailService
  {
    // TO DO: Read from file
    // SMTP Configuration
    public static string smtpAddress = "smtp.gmail.com";
    public static int smtpPort = 587;

    // SMTP Account
    public static string smtpMail = "ejlorenzo19@gmail.com";
    public static string smtpPassword = "eD21115476*";

    public static void Send(Mail mail)
    {
      try {
        SmtpClient mailServer = new SmtpClient(MailService.smtpAddress, MailService.smtpPort) {
          EnableSsl = true,

          Credentials = new System.Net.NetworkCredential(MailService.smtpMail, MailService.smtpPassword)
        };
        mailServer.Send(mail.ToMailMessage());
        Console.WriteLine("Mail sent to {0}!", mail.getTo());
      }
      catch (Exception ex) {
        Console.WriteLine("Error sending email ({0})", ex.StackTrace);
      }
    }
  }
}
