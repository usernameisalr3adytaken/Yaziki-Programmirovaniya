using System;
using System.Windows;

namespace DM
{
    class InputDataWithCheck
    {
        static public int InputIntegerWithValidation(string s, int left, int right) // Ввод целого числа с проверкой правильности ввода, в том числе принадлежности к указанному диапазону.                                                                                         
        {
            bool ok;
            int a;

            ok = int.TryParse(s, out a);
            if (ok)
                if (a < left || a > right)
                    return -1;
                else
                    return a;
            else
                return -1;
        }

    }
}