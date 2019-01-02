using Beyond_Aiden.Utils;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Beyond_Aiden
{
  class Program
  {
    private static void Main(string[] args)
    {
      // Mail
      /*Mail mail = new Mail("ejlorenzo19@gmail.com", "Testing attachments!");
      mail.addAttachment(WindowsSystem.WindowScreenshot("whatsapp"));
      MailService.Send(mail);*/
      App app = new App();
      Thread t = new Thread(app.Main);
      t.Start();
    }
  }
}
