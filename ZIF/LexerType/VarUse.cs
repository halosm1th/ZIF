using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class VarUse : LexerTypes
    {
        public string varName;

        public VarUse(string _varName)
        {
            varName = _varName;
        }
    }
}
