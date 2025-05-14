using DM;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

/// <summary>
/// ����� �������� ���������
/// </summary>
class Programm
{
    public static string binFile = "task.bin";

    /// <summary>
    /// ��������� ������������ ������� �������� � ���������� ��
    /// </summary>
    private static void Chooser()
    { 
        DataBase data = new DataBase();

        Console.WriteLine("�������� ��������: \n 1. �������������� ������ � �� \n 2. ������������� ������ �� �� \n 3. ����������� ������ �� ��" +
            "\n 4. ������� ������� �� �� \n 5. �������� ������� � �� \n 6. ������� ������ ����� ������������� ��������� \n 7. ������� ������ �����, ����� ���������� ������� �� ��������� ��������� �����" +
            "\n 8. ����� ����� ������� ������ \n 9. ����� ������ � ���������� �������� ���������� \n 10. ��������� � ������� ����");

        string? choose = Console.ReadLine();

        uint id;
        string name;
        string type;
        uint cost;
        TimeOnly time;
        string oper;

        switch (choose)
        {
            case "1":
                Console.WriteLine("�������");
                data.WriteData(binFile);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "2":
                Console.WriteLine("������");
                data.ReadData(binFile);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "3":
                Console.WriteLine("��������: ");
                data.ShowData();
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "4":
                Console.WriteLine("��������");
                id = (uint)InputDataWithCheck.InputIntegerWithValidation($"\n������� id ��������, ������� ������� �������", 0, Int32.MaxValue);
                data.DeleteService(id);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "5":
                Console.WriteLine("�������� ��������");
                id = (uint)InputDataWithCheck.InputIntegerWithValidation($"\n������� id ������ ��������", 0, Int32.MaxValue);
                Console.Write("������� �������� ����� ������: "); 
                name = Console.ReadLine();
                Console.Write("������� ��������� ����� ������: ");
                type = Console.ReadLine();
                cost = (uint)InputDataWithCheck.InputIntegerWithValidation($"\n������� ��������� ����� ������", 0, Int32.MaxValue);
                Console.Write("������� ��������� ��� ����� ������: ");
                oper = Console.ReadLine();
                try
                {
                    Console.Write("������ ����� ���������� ����� ������: ");
                    time = TimeOnly.ParseExact(Console.ReadLine(), "HH:mm:ss", null);

                    Service elem = new Service(id, name, type, cost, time, oper);
                    data.AddService(elem);
                }
                catch (Exception e)
                {
                    Console.WriteLine("������������ ����");
                }
                                
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "6":
                Console.Write("������� ���������, ������ �������� ������ ����������: ");
                oper = Console.ReadLine();

                if (data.SelectByOperator(oper).Count() != 0)
                {
                    foreach (var element in data.SelectByOperator(oper))
                    {
                        Console.WriteLine(element);
                    }
                }
                else
                {
                    Console.WriteLine("�� ������� ����� �������� � ��������� ����������");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "7":
                Console.Write("������ �����, ������� �� ������ ��������� ���������� ������: ");
                try
                {
                    time = TimeOnly.ParseExact(Console.ReadLine(), "HH:mm:ss", null);

                    if (data.SelectByTime(time).Count() != 0)
                    {
                        foreach (var element in data.SelectByTime(time))
                        {
                            Console.WriteLine(element);
                        }
                    }
                    else
                    {
                        Console.WriteLine("�� ������� ����� �������� � ��������� ����������");
                    }
                }
                catch (Exception e) 
                {
                    Console.WriteLine("������������ ����");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "8":
                if (data.MaxCost() != null)
                {
                    Console.WriteLine("����� ������� ������: " + data.MaxCost());
                }
                else
                {
                    Console.WriteLine("������, �������� ������ ��������� ����.");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "9":
                if (data.MinTime() != null)
                {
                    Console.WriteLine("������ � ���������� �������� ����������: " + data.MinTime());
                }
                else
                {
                    Console.WriteLine("������, �������� ������ ��������� ����.");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "10":
                Starter();
                break;

            default:
                Console.WriteLine("������ �����");
                Chooser();
                break;
        }

    }

    /// <summary>
    /// ��������� ����� �������� ��� ���������� ������ ���������
    /// </summary>
    private static void Starter()
    {
        bool flag = true;

        while (flag)
        {
            Console.WriteLine("\n�������� ��������: \n 1. ������ ������ \n 2. ��������� ������");
            string? choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Chooser();
                    break;

                case "2":
                    Console.WriteLine("���������� ������");
                    flag = false;
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("������ �����");
                    break;
            }

        }

        
    }

    static void Main()
    {
        Starter();
    }
}