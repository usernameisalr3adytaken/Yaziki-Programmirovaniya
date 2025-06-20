using Компилятор;

namespace Компилятор
{
    class SyntaxAnalyzer
    {
        static LexicalAnalyzer lexicalAnalyzer;
        static int beginLevel = 0;
        bool skipNext = false;
        bool SemicolonEnable = false;
        bool NewLine = false;

        public SyntaxAnalyzer()
        {
            lexicalAnalyzer = new LexicalAnalyzer();
            InputOutput.AddOnNewLineListener(OnNewLine);
        }

        public bool OnNewLine()
        {
            NewLine = false;
            if (SemicolonEnable)
            {
                if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
                {
                    InputOutput.Error(85, lexicalAnalyzer.token);
                }
                NewLine = true;
            }
            return true;
        }

        byte LoadHeader()
        {
            lexicalAnalyzer.NextSym();
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.programsy)
            {
                return 255;
            }
            lexicalAnalyzer.NextSym();
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.ident)
            {
                return 255;
            }
            lexicalAnalyzer.NextSym();
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
            {
                return 255;
            }
            return 0;
        }

        public void Analyze()
        {

            byte code = LoadHeader();
            if (code != 0)
            {
                InputOutput.Error(code, lexicalAnalyzer.token);
                return;
            }
            beginLevel = 0;
            while (true)
            {
                code = 0;
                if (!skipNext)
                    lexicalAnalyzer.NextSym();
                skipNext = false;
                switch (lexicalAnalyzer.symbol)
                {
                    case LexicalAnalyzer.endsy:
                        beginLevel--;
                        lexicalAnalyzer.NextSym();
                        if (lexicalAnalyzer.symbol == LexicalAnalyzer.point)
                        {
                            if (beginLevel > 0)
                            {
                                InputOutput.Error(37, lexicalAnalyzer.token);
                            }
                            if (beginLevel < 0)
                            {
                                InputOutput.Error(36, lexicalAnalyzer.token);
                            }
                            return;
                        }
                        break;
                    case LexicalAnalyzer.beginsy:
                        beginLevel++;
                        break;
                    case LexicalAnalyzer.varsy:
                        code = VarConstHandle();
                        break;
                    case LexicalAnalyzer.constsy:
                        code = VarConstHandle();
                        break;
                    case LexicalAnalyzer.ident:
                        byte statementResult = StatementHandle();
                        if (statementResult != 0)
                        {
                            InputOutput.Error(statementResult, lexicalAnalyzer.token);
                        }
                        break;
                    case LexicalAnalyzer.procedurensy:
                        code = ProcedurenHandle();
                        break;
                    default:
                        //InputOutput.Error(99, lexicalAnalyzer.token); // Неизвестный символ
                        break;
                }
                if (code != 0)
                {
                    InputOutput.Error(code, lexicalAnalyzer.token);
                }
                if (InputOutput.EndOfFile)
                {
                    InputOutput.Error(37, lexicalAnalyzer.token);
                    break;
                }
            }
        }

        byte VarConstHandle()
        {
            lexicalAnalyzer.NextSym();
            while (lexicalAnalyzer.symbol == LexicalAnalyzer.ident)
            {
                while (lexicalAnalyzer.symbol != LexicalAnalyzer.colon)
                {
                    if (lexicalAnalyzer.prevSymbol == LexicalAnalyzer.ident && lexicalAnalyzer.symbol != LexicalAnalyzer.comma)
                    {
                        return 87;
                    }
                    if (lexicalAnalyzer.symbol != LexicalAnalyzer.ident && lexicalAnalyzer.prevSymbol == LexicalAnalyzer.comma)
                    {
                        return 35;
                    }
                    lexicalAnalyzer.NextSym();
                }
                lexicalAnalyzer.NextSym();
                switch (lexicalAnalyzer.symbol)
                {
                    case LexicalAnalyzer.tinteger:
                    case LexicalAnalyzer.treal:
                    case LexicalAnalyzer.tboolean:
                    case LexicalAnalyzer.tstring:
                    case LexicalAnalyzer.tchar:
                        break;
                    default:
                        return 33;               
                }
                lexicalAnalyzer.NextSym();
                if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
                {
                    return 85;
                }
                lexicalAnalyzer.NextSym();
            }
            skipNext = true;
            return 0;
        }

        byte ProcedurenHandle()
        {
            // Получаем следующий символ после 'procedure'
            lexicalAnalyzer.NextSym();

            // Проверка имени процедуры (идентификатора)
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.ident)
            {
                // Пропускаем все до точки с запятой вручную
                while (!InputOutput.EndOfFile &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
                {
                    lexicalAnalyzer.NextSym();
                }
                return 35;
            }

            // Переход к следующему символу после имени
            lexicalAnalyzer.NextSym();
            int parenLevel = 0;

            // Обработка параметров (если есть)
            if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
            {
                parenLevel++;
                lexicalAnalyzer.NextSym();

                // Вручную анализируем параметры
                while (parenLevel > 0 && !InputOutput.EndOfFile)
                {
                    if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
                        parenLevel++;
                    else if (lexicalAnalyzer.symbol == LexicalAnalyzer.rightpar)
                        parenLevel--;
                    else if (lexicalAnalyzer.symbol == LexicalAnalyzer.semicolon)
                        return 85;

                    lexicalAnalyzer.NextSym();
                }

                if (parenLevel != 0)
                    return 89;
            }

            // Проверка обязательной ';' после объявления
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
            {
                // Вручную идем до конца оператора
                while (!InputOutput.EndOfFile &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.beginsy)
                {
                    lexicalAnalyzer.NextSym();
                }
                return 85;
            }

            // Подготовка к анализу тела процедуры
            lexicalAnalyzer.NextSym();
            beginLevel++;
            SemicolonEnable = false;

            return 0;
        }

        byte StatementHandle()
        {
            // Запоминаем позицию текущего токена
            TextPosition identPos = lexicalAnalyzer.token;

            lexicalAnalyzer.NextSym();

            // Определяем тип оператора
            if (lexicalAnalyzer.symbol == LexicalAnalyzer.assign)
            {
                byte result = AssignmentHandle();
                if (result != 0)
                {
                    InputOutput.Error(result, identPos);
                    return result;
                }
            }
            else if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar ||
                     lexicalAnalyzer.symbol == LexicalAnalyzer.semicolon)
            {
                byte result = ProcedureCallHandle();
                if (result != 0)
                {
                    InputOutput.Error(result, lexicalAnalyzer.token);
                    return result;
                }
            }
            else
            {
                InputOutput.Error(99, lexicalAnalyzer.token);
                return 99;
            }

            return 0;
        }

        byte AssignmentHandle()
        {
            // Запоминаем позицию оператора присваивания
            TextPosition assignPos = lexicalAnalyzer.token;

            // Переходим к выражению после :=
            lexicalAnalyzer.NextSym();
            int parenLevel = 0;

            // Анализ выражения
            while (!InputOutput.EndOfFile &&
                   lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon &&
                   lexicalAnalyzer.symbol != LexicalAnalyzer.endsy)
            {
                // Проверка валидности символа
                if (!IsValidInExpression(lexicalAnalyzer.symbol))
                {
                    InputOutput.Error(35, lexicalAnalyzer.token);
                    return 35;
                }

                // Контроль скобок
                if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
                {
                    parenLevel++;
                }
                else if (lexicalAnalyzer.symbol == LexicalAnalyzer.rightpar)
                {
                    parenLevel--;
                    if (parenLevel < 0)
                    {
                        InputOutput.Error(89, lexicalAnalyzer.token);
                        return 89;
                    }
                }

                lexicalAnalyzer.NextSym();
            }

            // Проверка завершения выражения
            if (parenLevel != 0)
            {
                InputOutput.Error(89, assignPos);
                return 89;
            }

            return 0;
        }

        byte ProcedureCallHandle()
        {
            // Обработка параметров (если есть)
            if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
            {
                TextPosition parenStartPos = lexicalAnalyzer.token; // Запоминаем позицию '('
                int parenLevel = 1;
                lexicalAnalyzer.NextSym();

                // Анализ параметров
                while (parenLevel > 0 && !InputOutput.EndOfFile)
                {
                    if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
                    {
                        parenLevel++;
                    }
                    else if (lexicalAnalyzer.symbol == LexicalAnalyzer.rightpar)
                    {
                        parenLevel--;
                    }
                    else if (lexicalAnalyzer.symbol == LexicalAnalyzer.semicolon)
                    {
                        InputOutput.Error(85, lexicalAnalyzer.token);
                        return 85;
                    }

                    lexicalAnalyzer.NextSym();
                }

                if (parenLevel != 0)
                {
                    InputOutput.Error(89, parenStartPos);
                    return 89;
                }
            }

            // Проверка завершения оператора
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
            {
                InputOutput.Error(85, lexicalAnalyzer.token);
                return 85;
            }

            return 0;
        }

        private bool IsValidInExpression(byte symbol)
        {
            return symbol == LexicalAnalyzer.ident ||
                   symbol == LexicalAnalyzer.intc ||
                   symbol == LexicalAnalyzer.floatc ||
                   symbol == LexicalAnalyzer.plus ||
                   symbol == LexicalAnalyzer.minus ||
                   symbol == LexicalAnalyzer.star ||
                   symbol == LexicalAnalyzer.slash ||
                   symbol == LexicalAnalyzer.leftpar ||
                   symbol == LexicalAnalyzer.rightpar;
        }
    }
}