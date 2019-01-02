using System;
using System.Diagnostics;

namespace Beyond_Aiden.Exceptions
{
  [Serializable]
  internal class InvalidProcessHWND : Exception
  {
    public InvalidProcessHWND()
    {

    }

    public InvalidProcessHWND(Process p) : base(string.Format("Invalid process received (Window: {0} -- hWnd: {1})", p.MainWindowTitle, p.MainWindowHandle))
    {

    }
  }
}
