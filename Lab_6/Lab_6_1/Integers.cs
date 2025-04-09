using DM;

class Integers
{
    public int aValue { get; set; }
    public int bValue { get; set; }
    public int cValue { get; set; }

    public Integers()
    {
        aValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число A (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
        bValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число B (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
        cValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число C (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);

    }
    public Integers(int a, int b, int c)
    {
        aValue = a;
        bValue = b;
        cValue = c;
    }
    public Integers(Integers number)
    {
        aValue = number.aValue;
        bValue = number.bValue;
        cValue = number.cValue;
    }

    public int Multiply()
    {
        return aValue * bValue * cValue;
    }

    public override string ToString()
    {
        return $"Число А = {aValue}" + $", число В = {bValue}" + $", число С = {cValue}";
    }

}
