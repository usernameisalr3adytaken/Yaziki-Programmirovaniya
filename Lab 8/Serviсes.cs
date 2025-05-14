/// <summary>
/// Класс, описывающий объект каталога
/// </summary>
public class Service
{
    private uint _id;
    private string _name;
    private string _type;
    private uint _cost;
    private TimeOnly _leadTime;
    private string _oper;


    internal uint id 
    {
        get { return _id; }
        set {
            if (value < 0)
                _id = (uint)Math.Abs(value);
            else
                _id = value;
        }
    }
    internal string name
    {
        get { return _name; }
        set { _name = value; }
    }
    internal string type
    {
        get { return _type; }
        set { _type = value; }
    }
    internal uint cost
    {
        get { return _cost; }
        set { 
            if (value < 0)  
                _cost = (uint)Math.Abs(value); 
            else
                _cost = value;
        }
    }
    internal TimeOnly leadTime
    {
        get { return _leadTime; }
        set { _leadTime = value; }
    }
    internal string oper
    {
        get { return _oper; }
        set { _oper = value; }
    }

    internal Service(uint id, string name, string type, uint cost, TimeOnly leadTime, string oper)
    {
        this._id = (uint)Math.Abs(id);
        this._name = name;
        this._type = type;
        this._cost = (uint)Math.Abs(cost);
        this._leadTime = leadTime;
        this._oper = oper;
    }

    public override string ToString()
    {
        return $"id: {id} | Название услуги: {name} | Вид услуги: {type} | Стоимость: {cost} | Время выполнения: {leadTime} | Оператор: {oper}";
    }
}