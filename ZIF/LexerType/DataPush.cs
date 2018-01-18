using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class DataPush : LexerTypes
    {
        public string value;

        public DataPush(string _value)
        {
            value = _value;
        }
    }
}
