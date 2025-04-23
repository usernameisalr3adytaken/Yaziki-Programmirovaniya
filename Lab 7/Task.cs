using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using DM;

class Task 
{
    // Заполнение txt файла числами
    private static void FillTextFile(string filename, int count)
    {
        Random rnd = new Random();
        StreamWriter fout;

        try
        {
            fout = new StreamWriter(filename);
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        for (int i = 0; i < count; i++)
        {
            fout.WriteLine(rnd.Next(rnd.Next(-50, 0), rnd.Next(1, 50)));
        }
        
        fout.Close();
    }
    // Заполнение bin файла числами
    private static void FillBinFile(string filename, int count)
    {
        Random rnd = new Random();
        BinaryWriter fout;

        try
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            fout = new BinaryWriter(f);
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        for (int i = 0; i < count; i++)
        {
            fout.Write((int)rnd.Next(-50, 50));
        }

        fout.Close();
    }
    // Заполнение xml
    public static void CreateToys(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
        FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
        Random rnd = new Random();

        string[] toyList = new string[] { "Toy1", "Toy2", "Toy3", "Toy4", "Toy5", "Toy6", "Toy7", "Toy8" };
        Toy[] toys = new Toy[8];
        for (int i = 0; i < 8; i++)
        {
            Toy toy = new Toy();
            toy.Name = toyList[i];
            Console.WriteLine("Название: " + toy.Name);
            toy.Price = rnd.Next(20, 200);
            Console.WriteLine("Цена: " + toy.Price + "руб");
            toy.MinAge = rnd.Next(2, 9);
            toy.MaxAge = rnd.Next(9, 16);
            Console.WriteLine("Можно с " + toy.MinAge + " до " + toy.MaxAge + " лет" + '\n');

            toys[i] = toy;
        }

        serializer.Serialize(fs, toys);

        fs.Close();
    }


    // Задание №1
    public static int GetMaxNumCount(string filename)
    {
        Random count = new Random();
        FillTextFile(filename, count.Next(3, 10));

        StreamReader fin;
        string? value;
        int maxVal = Int32.MinValue;
        int counter = 0;

        try
        {
            fin = new StreamReader(File.OpenRead(filename));
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        while ((value = fin.ReadLine()) != null)    
        {
            int.TryParse(value, out int temp);

            if (maxVal < temp)
            {
                maxVal = temp;
                counter = 1;    
            }

            else if (maxVal == temp)
            {
                counter++;
            }

        }

        fin.Close();

        return counter;
    }
    
    // Задание №2
    public static int EvenCounter(string filename)
    {
        Random count = new Random();
        FillTextFile(filename, count.Next(3, 10));

        StreamReader fin;
        string? value;
        int counter = 0;

        try
        {
            fin = new StreamReader(File.OpenRead(filename));
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        while ((value = fin.ReadLine()) != null)
        {
            int.TryParse(value, out int temp);

            if (Math.Abs(temp) % 2 == 0)
            {
                counter++;
            }

        }

        fin.Close();

        return counter;
    }

    // Задание №3
    public static void RewriteStrings(string filename1, string filename2, string combination)
    {
        StreamReader fin;
        StreamWriter fout;
        string? value;

        try
        {
            fin = new StreamReader(File.OpenRead(filename1));
            fout = new StreamWriter(File.OpenWrite(filename2));
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        while ((value = fin.ReadLine()) != null)
        {
            if (value.Contains(combination))
            {
                fout.WriteLine(value);
            }

        }

        fin.Close();
        fout.Close();
    }

    // Задание №4
    public static int DifferenceMaxMin(string filename)
    {
        Random rnd = new Random();
        FillBinFile(filename, 10);

        BinaryReader fin;

        try
        {
            FileStream f = new FileStream(filename, FileMode.Open);
            fin = new BinaryReader(f);
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n");
        }

        int[] arr = new int[10];

        for (int i = 0; i < 10; ++i)
        {
            arr[i] = fin.ReadInt32();
            Console.WriteLine(arr[i] + " ");
        }        

        fin.Close();

        return (arr.Max() - arr.Min());

    }

    // Задание "5
    public static void ToySearcher(string filename, int k)
    {
        CreateToys(filename);

        XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
        FileStream fs = new FileStream(filename, FileMode.Open);

        Toy[] toyList = new Toy[8];
        
        toyList = (Toy[])serializer.Deserialize(fs);
      

        int max = -1;
        int index = 0;

        for (int i = 0; i < 8; i++)
        {
            if (toyList[i].Price > max)
            {
                max = toyList[i].Price;
                index = i;
            }
        }

        for (int i = 0; i < 8; ++i)
        {
            if (Math.Abs(toyList[i].Price - toyList[index].Price) < k)
            {
                Console.Write(toyList[i].Name + " ");
            }
        }

    }

    // Задание №6
    public static void ReplaceElement<T>(ref List<T> list)
    {
        var temp = list.First();
        list.Remove(list.First());
        list.Add(temp);
    }

    // Задание №7
    public static void DeleteBetwenn<T>(ref LinkedList<T> list)
    { 

        var current = (list.First).Next;

        while (current != null && current != list.Last && list.Count >= 3)
        {
            var previuousCurrent = current;
            if (current.Previous.Value.ToString() == current.Next.Value.ToString())
            {
                previuousCurrent = current;
                list.Remove(current);

            }
            current = previuousCurrent.Next;
        }

        if (list.Last.Previous.Value.ToString() == list.First.Value.ToString())
        {
            list.Remove(list.Last);
        }

        if (list.Last.Value.ToString() == list.First.Next.Value.ToString())
        {
            list.Remove(list.First);
        }

    }

    // Задание №8
    public static void Fabrics(string filename)
    {
        StreamReader fin;
        try
        {
            fin = new StreamReader(filename);
        }
        catch (Exception e)
        {
            Console.WriteLine("Что-то пошло не так");
            return;
        }

        int fabNum = Convert.ToInt32(fin.ReadLine());
        int buyNum = Convert.ToInt32(fin.ReadLine());
        
        HashSet<string> fabrics = new HashSet<string>();
        HashSet<string>[] buyers = new HashSet<string>[buyNum];

        for (int i = 0; i < fabNum; i++)
        {
            fabrics.Add(fin.ReadLine());
        }

        string[] scan = new string[fabNum];
        for (int i = 0; i < buyNum; i++)
        {
            buyers[i] = new HashSet<string>();

            scan = fin.ReadLine().Split(' ');
            foreach (var fab in scan)
            {
                buyers[i].Add(fab);
            }
        }

        fin.Close();


        var buyersCopy = buyers;

        // Поиск мебели, которая есть у всех
        for (int i = 0; i < buyNum; i++)
        {
            buyers[i].IntersectWith(fabrics);
        }

        for (int i = 1; i < buyNum; i++)
        {
            buyers[0].IntersectWith(buyers[i]);
        }

        if (buyers[0].Count != 0)
        {
            Console.Write("Названия фабрик, изделия которых приобрел каждый ");
            foreach (var i in buyers[0])
            {
                Console.WriteLine(i + " ");
            }
        }
        else Console.WriteLine("Ни один предмет какой-либо фабрики не был куплен всеми покупателями");


        // Поиск мебели, которую купили хотя бы один раз
        for (int i = 0; i < buyNum; i++)
        {
            buyersCopy[i].IntersectWith(fabrics);
        }

        for (int i = 0; i < buyNum; i++)
        {
            buyersCopy[0].UnionWith(buyersCopy[i]);
        }

        if (buyersCopy[0].Count != 0)
        {
            Console.Write("Мебель этих фабрик присутствует хотя бы у одного из покупателей: ");
            foreach (var i in buyersCopy[0])
            {
                Console.Write(i + " ");
            }

            fabrics.ExceptWith(buyersCopy[0]);

            Console.WriteLine();

            if (fabrics.Count != 0)
            {
                Console.Write("У этих фабрик нет ни одного покупателя: ");
                foreach (var i in fabrics)
                {
                    Console.Write(i + " ");
                }
            }

            else
            {
                Console.WriteLine("У всех фабрик есть хотя бы один покупатель.");
            }

        }

        else
        {
            Console.WriteLine("Ни у одной из фабрик нет ни одного покупателя.");
        }

    }

    // Задание №9
    public static void AlphabetSearch(string filename)
    {
        StreamReader fin;
        try
        {
            fin = new StreamReader(filename);
        }
        catch (Exception e)
        {
            throw new Exception("Что-то пошло не так\n"); //Если что-то не так...
        }

        HashSet<char> letters = new HashSet<char>();
        HashSet<char> exists = new HashSet<char>();
        string cons = "ПФКТШСХЦЧЩпфктшсхцчщ";

        foreach (char i in cons)
        {
            letters.Add(i);
        }

        string line = fin.ReadLine();
        List<char> res = new List<char>();
        string[] split = line.Split(' ');

        // Поиск глухих согласных в нечетных словах
        for (int i = 0; i < split.Length; i++)
        {
            if (i % 2 == 0)
            {
                HashSet<char> temp = new HashSet<char>();

                foreach (char c in split[i])
                {
                    temp.Add(c);
                }

                temp.IntersectWith(letters);

                foreach (char c in temp)
                {
                    res.Add(c);
                }
            }
            
        }

        HashSet<char> chars = new HashSet<char>();
        HashSet<char> charsUnion = new HashSet<char>();

        // Поиск глухих согласных в четных словах
        for (int i = 0; i < split.Length; i++)
        {
            if (i % 2 != 0)
            {
                foreach (char c in split[i])
                {
                    chars.Add(c);
                }

                chars.IntersectWith(letters);
                charsUnion.UnionWith(chars);
            }

        }

        res.Sort();

        foreach (char c in res)
        {
            exists.Add(c);
        }

        exists.ExceptWith(charsUnion);

        Console.Write("Результат: ");

        foreach (char c in exists)
        {
            Console.Write(c + " ");
        }
        
    }

}

