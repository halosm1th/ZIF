﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIF.LexerType
{
    class VarAdder : LexerTypes
    {
        public string varName;

        public VarAdder(string _varName)
        {
            varName = _varName;
        }
    }
}
