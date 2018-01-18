using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class WordDef : LexerTypes
    {
        public string wordName;

        public WordDef(string name)
        {
            wordName = name;
        }
    }
}
