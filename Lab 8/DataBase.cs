using System;

class DataBase
{

    private static List<Service> _services = new List<Service>();

    /// <summary>
    /// Запись данных в бинарный файл с БД
    /// </summary>
    /// <param name="filename"></param>
    public void WriteData(string filename)
    {

        try
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            BinaryWriter fout = new BinaryWriter(f);

            fout.Write(_services.Count);
            foreach (Service element in _services)
            {
                    fout.Write(element.id);
                    fout.Write(element.name);
                    fout.Write(element.type);
                    fout.Write(element.cost);
                    fout.Write(element.leadTime.ToString());
                    fout.Write(element.oper);
            
            }

            fout.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine("Что-то пошло не так" + ex.Message);
        }
    }

    /// <summary>
    ///  Чтение данных из файла, запись в список элементов
    /// </summary>
    /// <param name="filename"></param>
    public void ReadData(string filename)
    {
        try
        {
            BinaryReader fin = new BinaryReader(File.Open(filename, FileMode.Open));

            int count = fin.ReadInt32();
            _services.Clear();

            for (int i = 0; i < count; i++)
            {
                uint id = fin.ReadUInt32();
                string name = fin.ReadString();
                string type = fin.ReadString();
                uint cost = fin.ReadUInt32();
                TimeOnly leadTime;  TimeOnly.TryParse(fin.ReadString(), out leadTime);
                string oper = fin.ReadString();

                Service element = new Service(id, name, type, cost, leadTime, oper);
                _services.Add(element);
            }

            fin.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Что-то пошло не так");
        }
    }

    /// <summary>
    /// Выводит список элементов
    /// </summary>
    public void ShowData()
    {
        if (_services.Count() == 0)
        {
            Console.WriteLine("Список услуг пуст. Необходимо экспортирвать данные из БД");
        }
        else
        {
            foreach(Service elem in _services)
            {
                Console.WriteLine(elem);
            }
        }
    }

    /// <summary>
    /// Удаляет элемент с указанным ID из списка услуг
    /// </summary>
    /// <param name="id"></param>
    public void DeleteService(uint id)
    {
        var del = _services.FirstOrDefault(el => el.id == id);
        if (del != null)
        {
            _services.Remove(del);
        }
        else
        {
            Console.WriteLine("Элемент не найден");
        }
    }

    /// <summary>
    /// Добавляет новый элемент в список услуг
    /// </summary>
    /// <param name="element"></param>
    public void AddService(Service elem)
    {
        if (_services.Any(el => el.id == elem.id) || _services.Any(el => el.name == elem.name))
        {
            Console.WriteLine("Указанная услуга уже существует");
            return;
        }

        _services.Add(elem);
    }

    /// <summary>
    /// Возвращает список услуг, которые выполняются определенным оператором
    /// </summary>
    /// <param name="oper"></param>
    /// <returns></returns>
    public List<Service> SelectByOperator(string oper)
    {
        List<Service> res = _services.Where(el => el.oper == oper).ToList();
        return res;
    }

    /// <summary>
    /// Возвращает список услуг, длительность выполнения которых не превышает указанного времени
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public List<Service> SelectByTime(TimeOnly time)
    {
        List<Service> res = _services.Where(el => el.leadTime < time).ToList();
        return res;
    }

    /// <summary>
    /// Возвращает услугу с максимальной стоимостью
    /// </summary>
    /// <returns></returns>
    public Service? MaxCost()
    {
        if (_services.Count() != 0)
        {
            _services.OrderBy(el => el.cost).ToList();
            return _services.Last();
        }
        return null;
    }

    /// <summary>
    ///  Возвращает услугу с наименьшим временем выполнения
    /// </summary>
    /// <returns></returns>
    public Service MinTime()
    {
        _services.OrderBy(el => el.leadTime).ToList();
        return _services.FirstOrDefault();
    }
}
