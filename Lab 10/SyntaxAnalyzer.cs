using ����������;

namespace ����������
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
                        //InputOutput.Error(99, lexicalAnalyzer.token); // ����������� ������
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
            // �������� ��������� ������ ����� 'procedure'
            lexicalAnalyzer.NextSym();

            // �������� ����� ��������� (��������������)
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.ident)
            {
                // ���������� ��� �� ����� � ������� �������
                while (!InputOutput.EndOfFile &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
                {
                    lexicalAnalyzer.NextSym();
                }
                return 35;
            }

            // ������� � ���������� ������� ����� �����
            lexicalAnalyzer.NextSym();
            int parenLevel = 0;

            // ��������� ���������� (���� ����)
            if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
            {
                parenLevel++;
                lexicalAnalyzer.NextSym();

                // ������� ����������� ���������
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

            // �������� ������������ ';' ����� ����������
            if (lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon)
            {
                // ������� ���� �� ����� ���������
                while (!InputOutput.EndOfFile &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon &&
                       lexicalAnalyzer.symbol != LexicalAnalyzer.beginsy)
                {
                    lexicalAnalyzer.NextSym();
                }
                return 85;
            }

            // ���������� � ������� ���� ���������
            lexicalAnalyzer.NextSym();
            beginLevel++;
            SemicolonEnable = false;

            return 0;
        }

        byte StatementHandle()
        {
            // ���������� ������� �������� ������
            TextPosition identPos = lexicalAnalyzer.token;

            lexicalAnalyzer.NextSym();

            // ���������� ��� ���������
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
            // ���������� ������� ��������� ������������
            TextPosition assignPos = lexicalAnalyzer.token;

            // ��������� � ��������� ����� :=
            lexicalAnalyzer.NextSym();
            int parenLevel = 0;

            // ������ ���������
            while (!InputOutput.EndOfFile &&
                   lexicalAnalyzer.symbol != LexicalAnalyzer.semicolon &&
                   lexicalAnalyzer.symbol != LexicalAnalyzer.endsy)
            {
                // �������� ���������� �������
                if (!IsValidInExpression(lexicalAnalyzer.symbol))
                {
                    InputOutput.Error(35, lexicalAnalyzer.token);
                    return 35;
                }

                // �������� ������
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

            // �������� ���������� ���������
            if (parenLevel != 0)
            {
                InputOutput.Error(89, assignPos);
                return 89;
            }

            return 0;
        }

        byte ProcedureCallHandle()
        {
            // ��������� ���������� (���� ����)
            if (lexicalAnalyzer.symbol == LexicalAnalyzer.leftpar)
            {
                TextPosition parenStartPos = lexicalAnalyzer.token; // ���������� ������� '('
                int parenLevel = 1;
                lexicalAnalyzer.NextSym();

                // ������ ����������
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

            // �������� ���������� ���������
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