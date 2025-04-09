using DM;

class Cycle
{
    public void Chooser()
    {
        Time time1 = new Time();
        Time time2 = new Time();

        Console.WriteLine("_____\n" + time1.ToString() + "; " + time2.ToString() + "\n_____\n");
        
        Console.WriteLine("Выберите действие: \n 1. Найти разницу двух времен (вычесть из первого второе) \n 2. Увеличить на 1-цу минуты в первом времени \n 3. Увеличить на 1-цу минуты во втором времени" +
            "\n 4. Уменьшить на 1-цу минуты в первом времени \n 5. Уменьшить на 1-цу минуты во второом времени \n 6. Выразить первое время в минутах \n 7. Выразить второе время в минутах" +
            "\n 8. Проверить первое время на 00:00 \n 9. Проверить второе время на 00:00 \n 10. Сравнить два времени");
        string? choose = Console.ReadLine();

        switch (choose)
        {
            case "1":
                Console.WriteLine("_____\n Разница времен: " + time1.Difference(time2) + "\n_____\n");
                break;

            case "2":
                Console.WriteLine("_____\n Первое время + 1 минута: " + time1++ + "\n_____\n");
                break;
            case "3":
                Console.WriteLine("_____\n Второе время + 1 минута: " + time2++ + "\n_____\n");
                break;

            case "4":
                Console.WriteLine("_____\n Первое время - 1 минута: " + time1-- + "\n_____\n");
                break;
            case "5":
                Console.WriteLine("_____\n Второе время - 1 минута: " + time2-- + "\n_____\n");
                break;

            case "6":
                Console.WriteLine("_____\n Первое время в минутах: " + time1.TimeLength() + "\n_____\n");
                break;
            case "7":
                Console.WriteLine("_____\n Второе время в минутах: " + time2.TimeLength() + "\n_____\n");
                break;

            case "8":
                Console.WriteLine("_____\n Первое время не равно 00:00 : " + time1.TimeNull() + "\n_____\n");
                break;
            case "9":
                Console.WriteLine("_____\n Второе время не равно 00:00 : " + time2.TimeNull() + "\n_____\n");
                break;

            case "10":
                Console.WriteLine("_____\n time1 > time2 : " + (time1 > time2));
                Console.WriteLine("\n time1 < time2 : " + (time1 < time2) + "\n_____\n");
                break;

            default:
                Console.WriteLine("Ошибка ввода");
                Chooser();
                break;
        }
    }

    public void Starter()
    {
        bool flag = true;

        while (flag)
        {
            Console.WriteLine("\nВыберите действие: \n 1. Начать работу \n 2. Завершить работу");
            string? choose = Console.ReadLine();

            switch (choose) {
                case "1":
                    Chooser();
                    break;

                case "2":
                    Console.WriteLine("Завершение работы");
                    flag = false;
                    break;

                default:
                    Console.WriteLine("Ошибка ввода");
                    break;
            }

        }
    }
}
