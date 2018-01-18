using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class Import : LexerTypes
    {
        public string Name;

        public Import(string value)
        {
            Name = value;
        }
    }
}
