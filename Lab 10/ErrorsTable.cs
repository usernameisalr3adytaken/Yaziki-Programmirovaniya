namespace ����������
{
    class ErrorsTable
    {
        static public Dictionary<byte, string> errors = new Dictionary<byte, string>()
        {
            // �������������� ������
            [33] = "��������� ��� (integer, real, boolean, char ��� string)",
            [35] = "��������� �������������",
            [36] = "�������� 'begin'",
            [37] = "�������� 'end'",
            [50] = "��������� 'do'",
            [54] = "��������� 'of'",
            [57] = "��������� 'then'",
            [58] = "��������� 'to' ��� 'downto'",
            [85] = "��������� ';'",
            [86] = "��������� ':'",
            [87] = "��������� ','",
            [89] = "��������� ����������� ������ ')'",
            [91] = "��������� ':='",
            [99] = "����������� ������",

            // ������ ����� � ��������
            [32] = "��������� ����� ��� ������������ ���������",
            [100] = "��������� ������������ ����� ������ (255 ��������)",
            [203] = "������������ �������������� ��������",

            // ������ ����������
            [40] = "��������� ���������� ��������������",
            [41] = "������������� �������������",

            // ������ �������� � �������
            [60] = "�������������� ���������� ����������",
            [61] = "�������������� ����� ����������",

            // ��������� ������
            [200] = "���������� ������ �����������",
            [201] = "������������ �����",
            [202] = "������������ ������",
            [255] = "������������ ��������� ���������"
        };
    }
}



