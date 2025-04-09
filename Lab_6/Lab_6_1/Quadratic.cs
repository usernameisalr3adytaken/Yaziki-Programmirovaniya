class Quadratic : Integers
{
    private int _aCoef { get; set; }
    private int _bCoef { get; set; }
    private int _cCoef { get; set; }

    public Quadratic() : base() { }
    public Quadratic(int a, int b, int c) : base (a, b, c)
    {
        this._aCoef = a;
        this._bCoef = b;
        this._cCoef = c;
    }

    public float Discriminant()
    {
        return _bCoef * _bCoef - 4 * _aCoef * _cCoef;
    }
    public double X1()
    {
        if (Discriminant() < 0)
        {
            Console.WriteLine("Дискриминант отрицательный, корней нет\n");
            return float.MinValue;
        }
        else
            return (_bCoef + Math.Pow(this.Discriminant(), 0.5)) / 2 * _aCoef;
    }
    public double X2()
    {
        if (Discriminant() < 0)
        {
            return float.MinValue;
        }
        else
            return (_bCoef - Math.Pow(this.Discriminant(), 0.5)) / 2 * _aCoef;
    }
}
