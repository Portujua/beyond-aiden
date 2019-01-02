using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeyondCore.Exceptions
{
  [Serializable]
  public class MissingCommandParameter : Exception
  {
    public MissingCommandParameter() : base(string.Format("Missing command parameters"))
    {

    }
  }
}
