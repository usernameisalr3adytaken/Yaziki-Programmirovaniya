using System;
using DM;

class Programm
{
    static void Main()
    {
        string task1 = "task1.txt";
        string task2 = "task2.txt";
        string task31 = "task3_1.txt";
        string task32 = "task3_2.txt";
        string task4 = "task4.bin";
        string task5 = "task5.xml";
        string task8 = "task8.txt";
        string task9 = "task9.txt";


        Console.WriteLine("������� 1");
        int res1 = Task.GetMaxNumCount(task1);
        Console.WriteLine("���������� ��������� ������������� �������� � ���� " + task1 + $": {res1} \n _____ \n");

        Console.WriteLine("������� 2");
        int res2 = Task.EvenCounter(task2);
        Console.WriteLine("���������� ������ ��������� � ����� " + task2 + $": {res2} \n _____ \n");

        Console.WriteLine("������� 3");
        Console.Write("������� ���������� ��� ������ � ������� �����: ");
        string? combination = Console.ReadLine();
        Task.RewriteStrings(task31, task32, combination);
        Console.WriteLine("��������� ������� � ����� " + task32 + "\n _____ \n");

        Console.WriteLine("������� 4");
        int res4 = Task.DifferenceMaxMin(task4);
        Console.WriteLine("������� ������������� � ������������ ��������� � ����� " + task4 + $": {res4} \n _____ \n");

        Console.WriteLine("������� 5");
        int k = InputDataWithCheck.InputIntegerWithValidation($"������� ������� � ���� ���  �������, ����������� ��� ������ (�� {int.MinValue} �� {int.MaxValue})", int.MinValue, int.MaxValue);
        Task.ToySearcher(task5, k);
        Console.WriteLine("\n _____ \n");

        Console.WriteLine("������� 6");
        List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
        Task.ReplaceElement(ref list1);
        Console.WriteLine("��������� ��������� ������: " + string.Join(" ", list1) + "\n _____ \n");


        Console.WriteLine("������� 7");
        LinkedList<int> list2 = new LinkedList<int>(new int[] { 1, 2, 1, 4, 5, 1, 6 });
        Task.DeleteBetwenn(ref list2);
        Console.WriteLine("��������� ��������� ������: " + string.Join(" ", list2) + "\n _____ \n");


        Console.WriteLine("������� 8");
        Task.Fabrics(task8);
        Console.WriteLine("\n _____ \n");

        Console.WriteLine("������� 9");
        Task.AlphabetSearch(task9);
        Console.WriteLine("\n _____ \n");


    }

}