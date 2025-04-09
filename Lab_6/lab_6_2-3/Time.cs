using DM;

class Time
{
    private byte _hours { get; set; }
    private byte _minutes { get; set; }

    public Time()
    {
        _hours = (byte)InputDataWithCheck.InputIntegerWithValidation($"\nВведите Часы (от 0 до 23)", 0, 23);
        _minutes = (byte)InputDataWithCheck.InputIntegerWithValidation($"Введите Минуты (от 0 до 59)", 0, 59);
    }

    public Time(byte hours, byte minutes)
    {
        this._hours = hours;
        this._minutes = minutes;
    }

    public Time Difference(Time time)
    {
        if (time._minutes > this._minutes)
        {
            this._hours -= 1;
            this._minutes = (byte)(60 - (time._minutes - this._minutes));
        }
        else
            this._minutes -= time._minutes;

        if (time._hours > this._hours)
            this._hours = (byte)(24 - (time._hours - this._hours));
        else
            this._hours -= time._hours;
        
        return this;
    }

    public static Time operator ++(Time obj)
    {
        if (obj._minutes == 59)
        {
            if (obj._hours == 23)
                obj._hours = 0;
            else
                obj._hours = (byte)(obj._hours + 1);

            obj._minutes = 0;
        }
        else
           obj._minutes = (byte)(obj._minutes + 1);


        return obj;
    }
    public static Time operator --(Time obj)
    {
        if (obj._minutes == 0)
        {
            if (obj._hours == 0)
                obj._hours = 23;
            else
                obj._hours = (byte)(obj._hours - 1);

            obj._minutes = 59;
        }
        else
            obj._minutes = (byte)(obj._minutes - 1);

        return obj;
    }

    public int TimeLength()
    {
        int lenght = (int)(this._hours * 60 + this._minutes);
        return lenght;
    }

    public bool TimeNull()
    {
        if (this._hours != 0 || this._minutes != 0)
            return true;
        else 
            return false;
    }

    public static bool operator >(Time time1, Time time2)
    {
        return (int)(time1.hours*60 + time1._minutes) > (int)(time2._hours*60 + time2._minutes);
    }
    public static bool operator <(Time time1, Time time2)
    {
        return (int)(time1._hours * 60 + time1._minutes) < (int)(time2._hours * 60 + time2._minutes);
    }

    public override string ToString()
    {
        if (_minutes < 10)
            return $"Время = {_hours}" + $":0{_minutes}";
        else
            return $"Время = {_hours}" + $":{_minutes}";
    }

}
