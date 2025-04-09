using System;

class Programm
{
    public static void Main()
    {
        Console.WriteLine("Программа для работы со временем. После начала работы необходимо будет ввести два временных значения в формате часы + минуты.");
        Cycle mng = new Cycle();
        mng.Starter();

    }

}