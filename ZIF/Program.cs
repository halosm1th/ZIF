using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZIF.LexerType;

namespace ZIF
{
    class Program
    {
        private static string filename = "program.zif";
        public static bool verbose = false;
        static void Main(string[] args)
        {
            
            if (args.Length > 0)
            {
                handleArgs(args);
            }
            if (!File.Exists(filename))
            {
                Console.WriteLine("{0} does not exist.",filename);
                Environment.Exit(1);
            }

            List<string>asm = compile(filename);
            File.WriteAllLines(Directory.GetCurrentDirectory()+"/program.asm",asm);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done. No errors");
        }

        public static List<string> compile(string file)
        {
            string code = File.ReadAllText(file);
            if (verbose)
            {

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Loaded: " + file);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(code);

                Console.WriteLine("\n\n");
            }
            List<string> sourceCode = generateSourceCode(code);
            Lexer lexer = new Lexer(sourceCode);
            lexer.Lex();

            List<LexerTypes> lexedCode = lexer.LexedCode;

            Parser p = new Parser(lexedCode);
            p.Parse();

            List<string> asm = p.ASM;

            if (verbose)
            {
                foreach (string s in asm)
                {
                    Console.WriteLine(s);
                }
            }

            return asm;
        }

        private static List<string> generateSourceCode(string sourceCode)
        {
            return sourceCode.Split('\n').ToList();
        }

        public static void errorReport(string error, int lineNumber,  string line)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error on line {lineNumber}: {line}. Error: {error}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void handleArgs(string[] args)
        {
            if (args.Length > 1)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-f")
                    {
                        i++;
                        filename = args[i];
                    }else if (args[i] == "-v")
                    {
                        verbose = true;
                    }
                }
            }
            else
            {
                filename = args[0];
            }
        }
    }
}
