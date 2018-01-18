using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class Asm : LexerTypes
    {
        public string line;

        public Asm(string value)
        {
            line = value;
        }
    }
}
