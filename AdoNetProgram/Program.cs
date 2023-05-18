using AdoNetMediumLib.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

class Program
{
    static void Main(string[] args)
    {
        var connector = new MainConnector();

        var result = connector.ConnectAsync();

        var data = new DataTable();


        if (result.Result)
        {
            Console.WriteLine("Подключено успешно!");

            var db = new DbExecutor(connector);

            var tablename = "NetworkUser";

            Console.WriteLine("Получаем данные таблицы " + tablename);

            data = db.SelectAll(tablename);

            Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);


            Console.WriteLine("Отключаем БД!");
            connector.DisconnectAsync();

            result = connector.ConnectAsync();

            if (result.Result)
            {

                /// это вариант отсоеденненой модели
                {
                    Console.WriteLine("Количество строк в " + tablename + ": " + data.Rows.Count);
                    foreach (DataColumn column in data.Columns)
                    {
                        Console.Write($"{column.ColumnName}\t");
                    }
                    foreach (DataRow row in data.Rows)
                    {
                        var cells = row.ItemArray;
                        foreach (var cell in cells)
                        {
                            Console.Write($"{cell}\t");
                            Console.Write($"{row[data.Columns[0].ColumnName]}\t");
                        }
                        Console.WriteLine();

                    }

                }
                /// вот так работает ридер
                var reader = db.SelectAllCommandReader(tablename);
                if (reader != null)
                {
                    var columnList = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var name = reader.GetName(i);
                        columnList.Add(name);
                    }
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        Console.Write($"{columnList[i]}\t");
                    }
                    Console.WriteLine();
                    while (reader.Read())
                    {
                        for (int i = 0; i < columnList.Count; i++)
                        {
                            var value = reader[columnList[i]];
                            Console.Write($"{value}\t");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }

        else
        {
            Console.WriteLine("Ошибка подключения!");
        }

        Console.ReadKey();

    }
}