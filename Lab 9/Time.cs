using DM;

/// <summary>
/// Класс для работы со временем
/// </summary>
class Time
{
    private byte hours;
    private byte minutes;

    public Time() { }

    public Time(byte hours, byte minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
    }

    /// <summary>
    /// Находит разность во времени между текущим объектом класса и указанным
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public Time Difference(Time obj)
    {
        if (obj.minutes > this.minutes)
        {
            if (this.hours == 0)
                this.hours = 23;
            else
                this.hours -= 1;

            this.minutes = (byte)(60 - (obj.minutes - this.minutes));
        }
        else
            this.minutes -= obj.minutes;

        if (obj.hours > this.hours)
            this.hours = (byte)(24 - (obj.hours - this.hours));
        else
            this.hours -= obj.hours;
        
        return this;
    }

    /// <summary>
    /// Операция добавления 1 минуты к объекту класса
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Time operator ++(Time obj)
    {
        if (obj.minutes == 59)
        {
            if (obj.hours == 23)
                obj.hours = 0;
            else
                obj.hours = (byte)(obj.hours + 1);

            obj.minutes = 0;
        }
        else
           obj.minutes = (byte)(obj.minutes + 1);


        return obj;
    }

    /// <summary>
    /// Операция вычитания 1 минуты из объекта класса
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Time operator --(Time obj)
    {
        if (obj.minutes == 0)
        {
            if (obj.hours == 0)
                obj.hours = 23;
            else
                obj.hours = (byte)(obj.hours - 1);

            obj.minutes = 59;
        }
        else
            obj.minutes = (byte)(obj.minutes - 1);

        return obj;
    }

    /// <summary>
    /// Приведение к типу int
    /// </summary>
    /// <param name="obj"></param>
    public static implicit operator int(Time obj) => obj.minutes + 60*obj.hours;

    /// <summary>
    /// Результатом является true, если часы и минуты не равны нулю, и false в противном случае
    /// </summary>
    /// <param name="obj"></param>
    public static implicit operator bool(Time obj) => (obj.minutes != 0) || (obj.hours != 0);

    /// <summary>
    /// Операция "больше" для сравнения двух объектов класса 
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <returns></returns>
    public static bool operator >(Time time1, Time time2)
    {
        return (int)time1 > (int)time2;
    }

    /// <summary>
    /// Операция "меньше" для сравнения двух объектов класса
    /// </summary>
    /// <param name="time1"></param>
    /// <param name="time2"></param>
    /// <returns></returns>
    public static bool operator <(Time time1, Time time2)
    {
        return (int)time1 < (int)time2;
    }

    public override string ToString()
    {
        if (minutes < 10)
            if (hours < 10)
                return $"0{hours}" + $":0{minutes}";
            else
                return $"{hours}" + $":0{minutes}";
        else
            if (hours < 10)
            return $"0{hours}" + $":{minutes}";
        else
            return $"{hours}" + $":{minutes}";
    }

}
