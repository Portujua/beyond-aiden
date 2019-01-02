using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondCore.Exceptions
{
  [Serializable]
  public class UnknownCommand : Exception
  {
    public UnknownCommand() : base(string.Format("Unknown command"))
    {

    }
  }
}
