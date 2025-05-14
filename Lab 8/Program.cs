using DM;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;

/// <summary>
/// Класс основной программы
/// </summary>
class Programm
{
    public static string binFile = "task.bin";

    /// <summary>
    /// Позволяет пользоваетлю выбрать действие с элементами БД
    /// </summary>
    private static void Chooser()
    { 
        DataBase data = new DataBase();

        Console.WriteLine("Выберите действие: \n 1. Экспортировать данные в БД \n 2. Импортировать данные из БД \n 3. Просмотреть данные из БД" +
            "\n 4. Удалить элемент из БД \n 5. Добавить элемент в БД \n 6. Вывести список услуг определенного оператора \n 7. Вывести список услуг, время выполнения которых не превышает указанный лимит" +
            "\n 8. Найти самую дорогую услугу \n 9. Найти услугу с наименьшим временем выполнения \n 10. Вернуться в главное меню");

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
                Console.WriteLine("Экспорт");
                data.WriteData(binFile);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "2":
                Console.WriteLine("Импорт");
                data.ReadData(binFile);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "3":
                Console.WriteLine("Элементы: ");
                data.ShowData();
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "4":
                Console.WriteLine("Удаление");
                id = (uint)InputDataWithCheck.InputIntegerWithValidation($"\nВведите id элемента, который хотиите удалить", 0, Int32.MaxValue);
                data.DeleteService(id);
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "5":
                Console.WriteLine("Создание элемента");
                id = (uint)InputDataWithCheck.InputIntegerWithValidation($"\nВведите id нового элемента", 0, Int32.MaxValue);
                Console.Write("Введите название новой услуги: "); 
                name = Console.ReadLine();
                Console.Write("Введите категорию новой услуги: ");
                type = Console.ReadLine();
                cost = (uint)InputDataWithCheck.InputIntegerWithValidation($"\nВведите стоимость новой услуги", 0, Int32.MaxValue);
                Console.Write("Введите оператора для новой услуги: ");
                oper = Console.ReadLine();
                try
                {
                    Console.Write("Ввеите время выполнения новой услуги: ");
                    time = TimeOnly.ParseExact(Console.ReadLine(), "HH:mm:ss", null);

                    Service elem = new Service(id, name, type, cost, time, oper);
                    data.AddService(elem);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Некорректный ввод");
                }
                                
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "6":
                Console.Write("Введите оператора, услуги которого хотите посмотреть: ");
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
                    Console.WriteLine("Не удалось найти элементы с указанным параметром");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "7":
                Console.Write("Ввеите время, которое не должно превышать выполнение услуги: ");
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
                        Console.WriteLine("Не удалось найти элементы с указанным параметром");
                    }
                }
                catch (Exception e) 
                {
                    Console.WriteLine("Некорректный ввод");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "8":
                if (data.MaxCost() != null)
                {
                    Console.WriteLine("Самая дорогая услуга: " + data.MaxCost());
                }
                else
                {
                    Console.WriteLine("Ошибка, возможно список элементов пуст.");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "9":
                if (data.MinTime() != null)
                {
                    Console.WriteLine("Услуга с наименьшим временем выполнения: " + data.MinTime());
                }
                else
                {
                    Console.WriteLine("Ошибка, возможно список элементов пуст.");
                }
                Console.WriteLine("\n_____\n");

                Chooser();
                break;

            case "10":
                Starter();
                break;

            default:
                Console.WriteLine("Ошибка ввода");
                Chooser();
                break;
        }

    }

    /// <summary>
    /// Запускает выбор действий или прекращает работу программы
    /// </summary>
    private static void Starter()
    {
        bool flag = true;

        while (flag)
        {
            Console.WriteLine("\nВыберите действие: \n 1. Начать работу \n 2. Завершить работу");
            string? choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    Chooser();
                    break;

                case "2":
                    Console.WriteLine("Завершение работы");
                    flag = false;
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Ошибка ввода");
                    break;
            }

        }

        
    }

    static void Main()
    {
        Starter();
    }
}