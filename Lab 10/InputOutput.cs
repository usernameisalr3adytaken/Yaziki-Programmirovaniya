using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using Компилятор;

namespace Компилятор
{
    struct TextPosition
    {
        public uint lineNumber; // номер строки
        public byte charNumber; // номер позиции в строке

        public TextPosition(uint ln = 0, byte c = 0)
        {
            lineNumber = ln;
            charNumber = c;
        }
    }

    struct Err
    {
        public TextPosition errorPosition;
        public byte errorCode;

        public Err(TextPosition errorPosition, byte errorCode)
        {
            this.errorPosition = errorPosition;
            this.errorCode = errorCode;
        }
    }


    class InputOutput
    {
        const byte ERRMAX = 9;
        public static char Ch { get; set; }
        public static TextPosition positionNow = new TextPosition();
        static string? line;
        static byte lastInLine = 0;
        public static List<Err> err;
        static StreamReader File { get; set; }
        static uint errCount = 0;
        public static bool EndOfFile
        {
            get
            {
                return File.EndOfStream && (positionNow.charNumber >= lastInLine);
            }
        }

        static List<Func<bool>> OnNewLineListeners;
        static bool NewLine = false;

        static public void Init(string filename)
        {
            File = new StreamReader(filename);
            ReadNextLine();
            OnNewLineListeners = new List<Func<bool>>();
        }

        static public void AddOnNewLineListener(Func<bool> cb)
        {
            OnNewLineListeners.Add(cb);
        }

        static public void NextCh()
        {

            if (positionNow.charNumber >= lastInLine)
            {
                foreach (var cb in OnNewLineListeners)
                {
                    cb();
                }
                ListThisLine();
                if (err.Count > 0)
                    ListErrors();
                positionNow.lineNumber++;
                positionNow.charNumber = 0;
                ReadNextLine();
            }
            else ++positionNow.charNumber;
            if (line != null && lastInLine > 0)
            {
                try
                {
                    Ch = line[positionNow.charNumber];
                }
                catch (System.Exception)
                {
                    Console.WriteLine($"{line} {positionNow.charNumber}");
                    throw;
                }
            }
        }

        private static void ListThisLine()
        {
            Console.WriteLine(line);
        }

        private static void ReadNextLine()
        {
            if (!File.EndOfStream)
            {
                line = File.ReadLine();
                err = new List<Err>();
                if (line.Length == 0)
                {
                    Ch = ' ';
                    lastInLine = 0;
                }
                else
                {
                    lastInLine = (byte)(line.Length - 1);
                    Ch = line[positionNow.charNumber];
                }
            }
        }

        public static void End()
        {
            if (err.Count > 0)
                ListErrors();
            Console.WriteLine($"Компиляция завершена: : ошибок — {errCount}!");
        }

        static void ListErrors()
        {
            int pos = 3 - $"{positionNow.lineNumber} ".Length;
            string s;
            foreach (Err item in err)
            {
                ++errCount;
                s = "**";
                if (errCount < 10) s += "0";
                s += $"{errCount}**";
                while (s.Length - 1 < pos + item.errorPosition.charNumber) s += " ";
                s += $"^ ошибка {ErrorsTable.errors[item.errorCode]} в строке {item.errorPosition.lineNumber + 1}:{item.errorPosition.charNumber + 1}";
                Console.WriteLine(s);
            }
        }



        static public void Error(byte errorCode, TextPosition position)
        {
            Err e;
            if (err.Count <= ERRMAX)
            {
                e = new Err(position, errorCode);
                err.Add(e);
            }
        }
    }
}