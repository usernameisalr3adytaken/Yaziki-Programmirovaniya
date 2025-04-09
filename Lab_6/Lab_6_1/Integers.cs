using DM;

class Integers
{
    private int _aValue { get; set; }
    private int _bValue { get; set; }
    private int _cValue { get; set; }

    public Integers()
    {
        _aValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число A (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
        _bValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число B (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);
        _cValue = InputDataWithCheck.InputIntegerWithValidation($"Введите натуральное число C (от {int.MinValue} до {int.MaxValue})", int.MinValue, int.MaxValue);

    }
    public Integers(int a, int b, int c)
    {
        _aValue = a;
        _bValue = b;
        _cValue = c;
    }
    public Integers(Integers number)
    {
        _aValue = number._aValue;
        _bValue = number._bValue;
        _cValue = number._cValue;
    }

    public int Multiply()
    {
        return _aValue * _bValue * _cValue;
    }

    public override string ToString()
    {
        return $"Число А = {_aValue}" + $", число В = {_bValue}" + $", число С = {_cValue}";
    }

}
