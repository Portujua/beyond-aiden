using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Beyond_Aiden.Utils
{
  static class WindowsSystem
  {
    public static List<Process> ListProcesses()
    {
      Process[] processlist = Process.GetProcesses();
      List<Process> list = new List<Process>();

      foreach (Process process in processlist) {
        if (!string.IsNullOrEmpty(process.MainWindowTitle)) {
          try {
            list.Add(process);
          }
          catch (Exception) {

          }
        }
      }

      return list;
    }

    public static List<string> WindowScreenshot(string title)
    {
      List<Process> list = ListProcesses();
      List<string> filenamesList = new List<string>();

      try {
        foreach (Process p in list) {
          if (p.MainWindowTitle.ToLower().Contains(title.ToLower())) {
            string addedFile = ScreenCapturer.CaptureAndSave(p.MainWindowHandle);
            filenamesList.Add(addedFile);
          }
        }
      }
      catch (Exception ex) {
        Console.WriteLine("There was an error capturing window with title '{0}'", title);
      }

      return filenamesList;
    }

    public static void ClearScreenshotFolder()
    {
      string[] files = Directory.GetFiles(Path.Combine(Environment.GetEnvironmentVariable("TEMP")), "windows-*.png");

      foreach (string file in files) {
        File.Delete(file);
      }
    }
  }
}
