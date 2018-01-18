using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class WordCall : LexerTypes
    {
        public string wordName;

        public WordCall(string value)
        {
            wordName = value;
        }
    }
}
