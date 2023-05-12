using System;
using System.IO;

public class Record
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string IsActive { get; set; }
    public double Salary { get; set; }

    public Record()
    {
        Id = 0;
        Name = "";
        Age = 0;
        IsActive = "no";
        Salary = 0.0;
    }

    public Record(Record other)
    {
        Id = other.Id;
        Name = other.Name;
        Age = other.Age;
        IsActive = other.IsActive;
        Salary = other.Salary;
    }

    public static Record ReadFromStream(TextReader stream)
    {
        var record = new Record();
        var fields = stream.ReadLine().Split(',');
        record.Id = int.Parse(fields[0]);
        record.Name = fields[1];
        record.Age = int.Parse(fields[2]);
        record.IsActive = fields[3];
        record.Salary = double.Parse(fields[4]);
        return record;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var data = new Record[50];

        using (var file = new StreamReader("records.txt"))
        {
            for (int i = 0; i < 50; i++)
            {
                data[i] = Record.ReadFromStream(file);
            }
        }

        Console.WriteLine("Массив данных до сортировки:");
        PrintData(data);

        var bubbleSortedData = (Record[])data.Clone();
        BubbleSort(bubbleSortedData);

        var insertionSortedData = (Record[])data.Clone();
        InsertionSort(insertionSortedData);

        var selectionSortedData = (Record[])data.Clone();
        SelectionSort(selectionSortedData);

        Console.WriteLine("\nМассив данных после сортировки пузырьком:");
        PrintData(bubbleSortedData);

        Console.WriteLine("\nМассив данных после сортировки вставками:");
        PrintData(insertionSortedData);

        Console.WriteLine("\nМассив данных после сортировки выбором:");
        PrintData(selectionSortedData);
    }

    static void BubbleSort(Record[] data)
    {
        int n = data.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (data[j].Id > data[j + 1].Id)
                {
                    Swap(data, j, j + 1);
                }
            }
        }
    }

    static void SelectionSort(Record[] data)
    {
        int n = data.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (data[j].Id < data[minIndex].Id)
                {
                    minIndex = j;
                }
            }

            if (minIndex != i)
            {
                Swap(data, i, minIndex);
            }
        }
    }

    static void InsertionSort(Record[] data)
    {
        int n = data.Length;

        for (int i = 1; i < n; i++)
        {
            Record current = data[i];
            int j = i - 1;

            while (j >= 0 && data[j].Id > current.Id)
            {
                data[j + 1] = data[j];
                j--;
            }

            data[j + 1] = current;
        }
    }

    static void Swap(Record[] data, int index1, int index2)
    {
        Record temp = data[index1];
        data[index1] = data[index2];
        data[index2] = temp;
    }

    static void PrintData(Record[] data)
    {
        foreach (var record in data)
        {
            Console.WriteLine(
                $"Id: {record.Id}, Name: {record.Name}, Age: {record.Age}, IsActive: {record.IsActive}, Salary: {record.Salary}"
            );
        }
    }
}
