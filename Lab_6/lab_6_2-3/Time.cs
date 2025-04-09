using DM;

class Time
{
    public byte hours { get; set; }
    public byte minutes { get; set; }

    public Time()
    {
        hours = (byte)InputDataWithCheck.InputIntegerWithValidation($"\nВведите Часы (от 0 до 23)", 0, 23);
        minutes = (byte)InputDataWithCheck.InputIntegerWithValidation($"Введите Минуты (от 0 до 59)", 0, 59);
    }

    public Time(byte hours, byte minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
    }

    public Time Difference(Time time)
    {
        if (time.minutes > this.minutes)
        {
            this.hours -= 1;
            this.minutes = (byte)(60 - (time.minutes - this.minutes));
        }
        else
            this.minutes -= time.minutes;

        if (time.hours > this.hours)
            this.hours = (byte)(24 - (time.hours - this.hours));
        else
            this.hours -= time.hours;
        
        return this;
    }

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

    public int TimeLength()
    {
        int lenght = (int)(this.hours * 60 + this.minutes);
        return lenght;
    }

    public bool TimeNull()
    {
        if (this.hours != 0 || this.minutes != 0)
            return true;
        else 
            return false;
    }

    public static bool operator >(Time time1, Time time2)
    {
        return (int)(time1.hours*60 + time1.minutes) > (int)(time2.hours*60 + time2.minutes);
    }
    public static bool operator <(Time time1, Time time2)
    {
        return (int)(time1.hours * 60 + time1.minutes) < (int)(time2.hours * 60 + time2.minutes);
    }

    public override string ToString()
    {
        if (minutes < 10)
            return $"Время = {hours}" + $":0{minutes}";
        else
            return $"Время = {hours}" + $":{minutes}";
    }

}
