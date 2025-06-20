﻿using System;
using System.Security.Cryptography.X509Certificates;
using Компилятор;
namespace Компилятор
{
    class LexicalAnalyzer
    {
        public const byte
            star = 21, // *
            slash = 60, // /
            equal = 16, // =
            comma = 20, // ,
            semicolon = 14, // ;
            colon = 5, // :
            point = 61,	// .
            arrow = 62,	// ^
            leftpar = 9,	// (
            rightpar = 4,	// )
            lbracket = 11,	// [
            rbracket = 12,	// ]
            flpar = 63,	// {
            frpar = 64,	// }
            later = 65,	// <
            greater = 66,	// >
            laterequal = 67,	//  <=
            greaterequal = 68,	//  >=
            latergreater = 69,	//  <>
            plus = 70,	// +
            minus = 71,	// –
            lcomment = 72,	//  (*
            rcomment = 73,	//  *)
            assign = 51,	//  :=
            twopoints = 74,	//  ..
            ident = 2,	// идентификатор
            floatc = 82,	// вещественная константа
            intc = 15,	// целая константа
            casesy = 31,
            elsesy = 32,
            filesy = 57,
            gotosy = 33,
            thensy = 52,
            typesy = 34,
            untilsy = 53,
            dosy = 54,
            withsy = 37,
            ifsy = 56,
            insy = 100,
            ofsy = 101,
            orsy = 102,
            tosy = 103,
            endsy = 104,
            varsy = 105,
            divsy = 106,
            andsy = 107,
            notsy = 108,
            forsy = 109,
            modsy = 110,
            nilsy = 111,
            setsy = 112,
            beginsy = 113,
            whilesy = 114,
            arraysy = 115,
            constsy = 116,
            labelsy = 117,
            downtosy = 118,
            packedsy = 119,
            recordsy = 120,
            repeatsy = 121,
            programsy = 122,
            functionsy = 123,
            procedurensy = 124,
            stringc = 130;

        // Types
        public const byte
            tinteger = 125, treal = 126, tboolean = 127, tchar = 128, tstring = 129;

        public byte symbol { get; private set; } // код символа
        public byte prevSymbol { get; private set; } // код символа
        public TextPosition token; // позиция символа
        public TextPosition prevToken;
        public string addrName { get; private set; } // адрес идентификатора в таблице имен
        public string symbol_const { get; private set; }
        int nmb_int; // значение целой константы
        float nmb_float; // значение вещественной константы
        char one_symbol; // значение символьной константы

        Keywords keywords;
        StreamWriter debugFile;

        bool afterNewLine = false;
        public bool lastInLine = false;

        public LexicalAnalyzer()
        {
            keywords = new Keywords();
            debugFile = new StreamWriter("debug-lex.txt");
            InputOutput.AddOnNewLineListener(OnNewLine);
        }

        public bool OnNewLine()
        {
            debugFile.WriteLine();
            debugFile.Flush();
            afterNewLine = true;
            return true;
        }

        public byte NextSym()
        {
            while (InputOutput.Ch == ' ') InputOutput.NextCh();
            prevSymbol = symbol;
            prevToken.lineNumber = token.lineNumber;
            prevToken.charNumber = token.charNumber;
            token.lineNumber = InputOutput.positionNow.lineNumber;
            token.charNumber = InputOutput.positionNow.charNumber;
            //сканировать символ
            addrName = "";
            symbol = 0;
            switch (InputOutput.Ch)
            {
                case var ch when ch >= '0' && ch <= '9':
                    byte digit;
                    Int16 maxint = Int16.MaxValue;
                    nmb_int = 0;
                    while (InputOutput.Ch >= '0' && InputOutput.Ch <= '9')
                    {
                        digit = (byte)(InputOutput.Ch - '0');
                        if (nmb_int < maxint / 10 ||
                        (nmb_int == maxint / 10 &&
                        digit <= maxint % 10))
                            nmb_int = 10 * nmb_int + digit;
                        else
                        {
                            // константа превышает предел
                            InputOutput.Error(203, InputOutput.positionNow);
                            nmb_int = 0;
                            while (InputOutput.Ch >= '0' && InputOutput.Ch <= '9') InputOutput.NextCh();
                        }
                        InputOutput.NextCh();
                    }
                    symbol = intc;
                    break;
                case var ch when (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '_':
                    string name = "";
                    while ((InputOutput.Ch >= 'a' && InputOutput.Ch <= 'z') ||
                            (InputOutput.Ch >= 'A' && InputOutput.Ch <= 'Z') ||
                            (InputOutput.Ch >= '0' && InputOutput.Ch <= '9') || InputOutput.Ch == '_')
                    {
                        name += InputOutput.Ch;
                        InputOutput.NextCh();
                    }
                    symbol = keywords.GetCode(name);
                    if (symbol == ident)
                    {
                        addrName = name;
                    }
                    break;
                case '\'':
                    //    сканировать символьную константу;
                    symbol_const = "";
                    InputOutput.NextCh();
                    while (InputOutput.Ch != '\'')
                    {
                        symbol_const += InputOutput.Ch;
                        InputOutput.NextCh();
                    }
                    InputOutput.NextCh();
                    if (symbol_const.Length > 255)
                    {
                        InputOutput.Error(100, token);
                        symbol_const = "";
                        break;
                    }
                    symbol = stringc;
                    break;
                case '<':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        symbol = laterequal; InputOutput.NextCh();
                    }
                    else
                     if (InputOutput.Ch == '>')
                    {
                        symbol = latergreater; InputOutput.NextCh();
                    }
                    else
                        symbol = later;
                    break;
                case ':':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        symbol = assign; InputOutput.NextCh();
                    }
                    else
                        symbol = colon;
                    break;
                case ';':
                    symbol = semicolon;
                    InputOutput.NextCh();
                    break;
                case '.':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '.')
                    {
                        symbol = twopoints; InputOutput.NextCh();
                    }
                    else symbol = point;
                    break;
                case '{':
                    // Comment handle
                    while (InputOutput.Ch != '}')
                    {
                        InputOutput.NextCh();
                    }
                    InputOutput.NextCh();
                    break;
                case '(':
                    symbol = leftpar;
                    InputOutput.NextCh();
                    break;
                case ')':
                    symbol = rightpar;
                    InputOutput.NextCh();
                    break;
                case '>':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        symbol = greaterequal;
                    }
                    else
                    {
                        symbol = greater;
                    }
                    break;
                case '-':
                    symbol = minus;
                    InputOutput.NextCh();
                    break;
                case ',':
                    symbol = comma;
                    InputOutput.NextCh();
                    break;
                case '\"':
                    InputOutput.NextCh();
                    string _string = "";
                    while (InputOutput.Ch != '\"')
                    {
                        _string += InputOutput.Ch;
                        InputOutput.NextCh();
                    }
                    InputOutput.NextCh();
                    break;
            }

            if (symbol == 0)
            {
                return NextSym();
            }
            if (afterNewLine)
            {
                for (int i = 0; i < token.charNumber; i++)
                {
                    debugFile.Write(" ");
                }
                afterNewLine = false;
            }
            debugFile.Write(symbol);
            debugFile.Write(" ");
            debugFile.Flush();
            return symbol;
        }
    }
}