using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIF.LexerType;

namespace ZIF
{

    struct Variable
    {
        public string Function;
        public VarDec variable;
        public int CurrentFunctionIndex;


        /// <summary>
        /// Create a new variable
        /// </summary>
        /// <param name="f">the current word</param>
        /// <param name="vn">the variable name</param>
        /// <param name="cfi">the current function index (where you are in the current function)</param>
        /// <param name="v">the value</param>
        public Variable(string f, VarDec v, int cfi)
        {
            
            Function = f;
            variable = v;
            CurrentFunctionIndex = cfi;
        }

    }
    class Parser
    {
        public List<string> ASM { get { return asm; } }
        List<LexerTypes> sourceCode = new List<LexerTypes>();
        List<string> asm = new List<string>();
        private bool verbose;

        public Parser(List<LexerTypes> _sourceCode)
        {
            sourceCode = _sourceCode;
            verbose = Program.verbose;
        }

        private string getFilepath(Import import)
        {

            string filePath = "";
            string file = import.Name;
            if (!file.Contains("/"))
            {
                filePath = Directory.GetCurrentDirectory() + "/" + file;
            }
            else
            {
                if (file.Contains("~"))
                {
                    filePath = file.Replace("~", Directory.GetCurrentDirectory() + "/");
                }
            }
            if (verbose)
            {
                Console.WriteLine("Importing: " + filePath);
            }
            return filePath;
        }


        public void Parse()
        {
            int variableCounter = 0;
            if (verbose)
            {
                Console.WriteLine("Parsing.");
            }
            string curentWord = "";
            List<Variable> variables = new List<Variable>();
            foreach (LexerTypes lexer in sourceCode)
            {
                if (lexer.GetType() == typeof(Asm))
                {
                    Asm inlineAsm = (Asm) lexer;
                    ASM.Add(inlineAsm.line);
                }else if (lexer.GetType() == typeof(WordDef))
                {
                    curentWord = ((WordDef) lexer).wordName;
                    string line = "label:" + ((WordDef) lexer).wordName;
                    ASM.Add(line);
                }else if (lexer.GetType() == typeof(EndFunc))
                {
                    curentWord = "";
                    variableCounter = 0;
                    ASM.Add("ret:0");
                }else if (lexer.GetType() == typeof(DataPush))
                {
                    ASM.Add("pushi:"+((DataPush)lexer).value);
                }else if (lexer.GetType() == typeof(HalfData))
                {
                    ASM.Add("pushi:"+((HalfData)lexer).WordName);
                }else if (lexer.GetType() == typeof(Import))
                {
                    string filename =  getFilepath((Import)lexer);
                    List<string> importCode = Program.compile(filename);
                    foreach (string s in importCode)
                    {
                        ASM.Add(s);
                    }
                }else if (lexer.GetType() == typeof(WordCall))
                {
                    ASM.Add("call:"+ ((WordCall)lexer).wordName);
                }else if (lexer.GetType() == typeof(VarDec))
                {
                    variableCounter++;
                    VarDec vd = (VarDec) lexer;
                    Variable v = new Variable(curentWord,vd,variableCounter);
                    variables.Add(v);
                    string asm = $@"loadi:r0,50000
loadi:r1,{variableCounter}
add:r0,r1,r0
loadi:r1,{v.variable.varValue}
savrr:r1,r0";
                    ASM.Add(asm);


                }else if (lexer.GetType() == typeof(VarUse))
                {
                    string varName = ((VarUse) lexer).varName;
                    foreach (Variable v in variables)
                    {
                        if (v.variable.varName == varName && v.Function == curentWord)
                        {
                            ASM.Add($@"loadi:r0,50000
loadi:r1,{v.CurrentFunctionIndex}
add:r0,r0,r1
redrr:r1,r0
push:r1");
                        }
                    }
                }else if (lexer.GetType() == typeof(VarAdder))
                {
                    string varName = ((VarAdder) lexer).varName;
                    foreach (Variable v in variables)
                    {
                        if (v.variable.varName == varName && v.Function == curentWord)
                        {
                            ASM.Add($@"loadi:r1,{v.CurrentFunctionIndex}
loadi:r0,50000
add:r1,r0,r1
push:r1");
                        }
                    }
                }
            }
        }
    }
}
