 public class Service
{
    private uint _id;
    private string _name;
    private string _type;
    private uint _cost;
    private TimeOnly _leadTime;
    private string _oper;

    internal uint id => _id;
    internal string name => _name;
    internal string type => _type;
    internal uint cost => _cost;
    internal TimeOnly leadTime => _leadTime;
    internal string oper => _oper;

    internal Service(uint id, string name, string type, uint cost, TimeOnly leadTime, string oper)
    {
        this._id = id;
        this._name = name;
        this._type = type;
        this._cost = cost;
        this._leadTime = leadTime;
        this._oper = oper;
    }

    public override string ToString()
    {
        return $"id: {id} | Название услуги: {name} | Вид услуги: {type} | Стоимость: {cost} | Время выполнения: {leadTime} | Оператор: {oper}";
    }
}