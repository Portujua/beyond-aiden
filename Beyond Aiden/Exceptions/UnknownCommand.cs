using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond_Aiden
{
  [Serializable]
  class UnknownCommand : Exception
  {
    public UnknownCommand() : base(string.Format("Unknown command"))
    {

    }
  }
}
