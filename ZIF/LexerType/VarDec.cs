using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class VarDec : LexerTypes
    {
        public string varName;
        public string varValue;
        public string currentFunction;

        public VarDec(string name, string value, string function)
        {
            varName = name;
            varValue = value;
            currentFunction = function;
        }
    }
}
