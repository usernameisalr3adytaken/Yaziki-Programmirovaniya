using System;

namespace DM
{
    class InputDataWithCheck
    {
        static public int InputIntegerWithValidation(string s, int left, int right) // Ввод целого числа с проверкой правильности ввода, в том числе принадлежности к указанному диапазону.                                                                                         
        {
            bool ok;
            int a;
            do
            {
                Console.WriteLine(s);
                ok = int.TryParse(Console.ReadLine(), out a);
                if (ok)
                    if (a < left || a > right)
                        ok = false;
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВведенные данные имеют неверный формат или не принадлежат диапазону [{left}; {right}]");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return a;
        }

    }
}