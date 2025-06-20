using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Компилятор
{
    class Program
    {
        static SyntaxAnalyzer syntaxAnalyzer = new SyntaxAnalyzer();
        static void Main()
        {
            InputOutput.Init("test.pas");
            syntaxAnalyzer.Analyze();
            InputOutput.End();
        }
    }
}
