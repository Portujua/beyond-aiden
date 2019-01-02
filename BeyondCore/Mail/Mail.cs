using System.Collections.Generic;
using System.Net.Mail;

namespace BeyondCore.Mail
{
  public class Mail
  {
    private string from = MailService.smtpMail, to, message, title = "";
    private List<string> files = new List<string>();

    public Mail(string to, string message = "", string title = "")
    {
      this.to = to;
      this.message = message;
      this.title = title;
    }

    public void addAttachment(string filename)
    {
      files.Add(filename);
    }

    public void addAttachment(List<string> filenames)
    {
      this.files = filenames;
    }

    public MailMessage ToMailMessage()
    {
      MailMessage mail = new MailMessage(getFrom(), getTo(), getTitle(), getMessage());
      
      if (this.files.Count > 0) {
        this.files.ForEach(file => {
          mail.Attachments.Add(new Attachment(file));
        });
      }

      return mail;
    }

    public string getFrom()
    {
      return from;
    }

    public string getTo()
    {
      return to;
    }

    public string getMessage()
    {
      return message;
    }

    public string getTitle()
    {
      return title;
    }
  }
}
