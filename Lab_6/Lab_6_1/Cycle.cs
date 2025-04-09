using DM;

class Cycle
{
    public void Chooser()
    {
        
        Console.WriteLine("Выберите класс, с которым хотите работать: \n 1. Родительский класс Integers \n 2. Дочерний класс Quadratic");
        string? choose = Console.ReadLine();

        switch (choose)
        {
            case "1":
                Integers number = new Integers();
                Console.WriteLine("_____\n" + number.ToString());
                Console.WriteLine("Произведение полей: " + number.Multiply());

                Integers numberCopy = new Integers(number);
                Console.WriteLine("Копия объекта:\n     " + numberCopy.ToString() + "\n_____\n");
                break;

            case "2":
                int a = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число A (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
                int b = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число B (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
                int c = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число C (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);

                Quadratic coefficients = new Quadratic(a, b, c);
                Console.WriteLine("Дискриминант = " + coefficients.Discriminant());

                if (coefficients.X1() != float.MinValue)
                {
                    Console.WriteLine("x1 = " + coefficients.X1());
                    Console.WriteLine("x2 = " + coefficients.X2());
                }
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
