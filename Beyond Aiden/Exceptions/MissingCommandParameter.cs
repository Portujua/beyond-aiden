using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond_Aiden
{
  [Serializable]
  class MissingCommandParameter : Exception
  {
    public MissingCommandParameter() : base(string.Format("Missing command parameters"))
    {

    }
  }
}
