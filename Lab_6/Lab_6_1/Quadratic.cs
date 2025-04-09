class Quadratic : Integers
{
    private int aCoef { get; set; }
    private int bCoef { get; set; }
    private int cCoef { get; set; }

    public Quadratic() : base() { }
    public Quadratic(int a, int b, int c) : base (a, b, c)
    {
        this.aCoef = a;
        this.bCoef = b;
        this.cCoef = c;
    }

    public float Discriminant()
    {
        return bCoef * bCoef - 4 * aCoef * cCoef;
    }
    public double X1()
    {
        if (Discriminant() < 0)
        {
            Console.WriteLine("Дискриминант отрицательный, корней нет\n");
            return float.MinValue;
        }
        else
            return (bCoef + Math.Pow(this.Discriminant(), 0.5)) / 2 * aCoef;
    }
    public double X2()
    {
        if (Discriminant() < 0)
        {
            return float.MinValue;
        }
        else
            return (bCoef - Math.Pow(this.Discriminant(), 0.5)) / 2 * aCoef;
    }
}
