using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZIF.LexerType;

namespace ZIF
{
    class Lexer
    {
        private List<string> sourceCode;
        public List<LexerTypes> LexedCode { get { return lexerStuff; } }
        private List<LexerType.LexerTypes> lexerStuff = new List<LexerTypes>();
        private bool verbose;

        public Lexer(List<string> _sourceCode)
        {
            sourceCode = _sourceCode;
            verbose = Program.verbose;
        }

        public void stripComments()
        {
            if (verbose)
            {
                Console.WriteLine("Stripping comments");
            }

            List<string> commentlessCode = new List<string>();
            for (int i = 0; i < sourceCode.Count; i++)
            {
                string line = sourceCode.ElementAt(i);
                if (line.Contains("("))
                {
                    int startingIndex = line.IndexOf("(");
                    int endingIndex = line.IndexOf(")")+1;
                    line = line.Remove(startingIndex, (endingIndex - startingIndex));
                }
                commentlessCode.Add(line);
            }
            sourceCode = commentlessCode;
        }

        public void ReplaceStrings()
        {
            if (verbose)
            {
                Console.WriteLine("replacing strings.");
            }
            List<string> newCode = new List<string>();
            foreach (string s in sourceCode)
            {
                string line = s;
                if (s.Contains("\""))
                {

                    string[] splitData = s.Split('\"');
                    string strin = splitData[1];//give us the string
                    if (verbose)
                    {
                        Console.WriteLine("Found strin: " + strin);
                    }
                    char[] strinTemp = strin.ToCharArray();
                    Array.Reverse(strinTemp);
                    strin = new string(strinTemp);
                    string charString = "";
                    foreach (char c in strin)
                    {
                        if (c == ' ')
                        {
                            charString += " 32 ";
                        }
                        else
                        {
                            charString += " '" + c + "' ";
                        }

                    }
                    charString += " " + strin.Length;
                    line = splitData[0] + charString + splitData[2];
                }
                newCode.Add(line);
            }
            sourceCode = newCode;
        }

        private bool isInt(string character)
        {
            if (verbose)
            {
                Console.WriteLine("Checking to see if " + character + " is int");
            }
            try
            {

                Convert.ToInt32(character);
                if (verbose)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("It is");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                return true;
            }
            catch (Exception e)
            {
                if (verbose)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("It is not");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                return false;
            }
        }

        private List<string> getWords(string line)
        {
            List<string> words = new List<string>();
            string current = "";
            foreach (char c in line)
            {
                if (c == ' ' || c == '\n' || c == '\t' | c=='\r')
                {
                    if (current == "")
                    {

                    }
                    else
                    {
                        words.Add(current);
                        current = "";
                    }
                }
                else
                {
                    current += c;
                }
            }
            if (current != "")
            {
                words.Add(current);
            }
            if (verbose)
            {
                Console.WriteLine("Found words: ");
                foreach (var word in words)
                {
                    Console.WriteLine(word);
                }
            }
            return words;
        }

        public void Lex()
        {
            if (verbose)
            {
                Console.WriteLine("lexing");
            }
            stripComments();
            ReplaceStrings();
            foreach (string line in sourceCode)
            {
                string currentFunction = "";
                List<string> words = getWords(line);
                for(int i =0; i < words.Count; i++)
                {
                    string w = words.ElementAt(i);


                    if (isInt(w))
                    {
                        DataPush dp = new DataPush(w);
                        lexerStuff.Add(dp);
                    }
                    else if (w.StartsWith("$"))
                    {
                        VarUse vu = new VarUse(w.Split('$')[1]);
                        lexerStuff.Add(vu);
                    }else if (w == "=")
                    {
                        i++;
                        string name = words.ElementAt(i);
                        i++;
                        string value = words.ElementAt(i);
                        VarDec dv= new VarDec(name,value,currentFunction);
                        lexerStuff.Add(dv);
                    }else if (w.StartsWith(":"))
                    {
                        string name = w.Split(':')[1];
                        WordDef wf = new WordDef(name);
                        lexerStuff.Add(wf);
                        currentFunction = name;
                    }else if (w.StartsWith("@"))
                    {
                        string value = w.Split('@')[1];
                        HalfData hd = new HalfData(value);
                        lexerStuff.Add(hd);
                    }else if (w == ";")
                    {
                        EndFunc ef = new EndFunc();
                        lexerStuff.Add(ef);
                        currentFunction = "";
                    }else if (w.StartsWith("#"))
                    {
                        string value = w.Split('#')[1];
                        Asm asm = new Asm(value);
                        lexerStuff.Add(asm);
                    }
                    else if (w.StartsWith("?"))
                    {
                        string value = w.Split('?')[1];
                        Import import = new Import(value);
                        lexerStuff.Add(import);
                    }else if (w.StartsWith("|"))
                    {
                        string value = w.Split('|')[1];
                        VarAdder va = new VarAdder(value);
                        lexerStuff.Add(va);
                    }
                    else
                    {
                        WordCall wd = new WordCall(w);
                        lexerStuff.Add(wd);
                    }
                }

            }
        }
    }
}
