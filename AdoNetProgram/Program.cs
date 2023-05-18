using AdoNetMediumLib.Configuration;
using AdoNetProgram;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using static AdoNetProgram.Manager;

class Program
{

    static void Main(string[] args)
    {
        var manager = new Manager();

        manager.Connect();


        Console.WriteLine("Список команд для работы консоли:");
        Console.WriteLine(Commands.stop + ": прекращение работы");
        Console.WriteLine(Commands.add + ": добавление данных");
        Console.WriteLine(Commands.delete + ": удаление данных");
        Console.WriteLine(Commands.update + ": обновление данных");
        Console.WriteLine(Commands.show + ": просмотр данных");

        string command;

        do
        {
            Console.WriteLine("Введите команду:");
            command = Console.ReadLine();
            switch (command)
            {
                case
                nameof(Commands.add):
                    {
                        Add(manager);
                        break;
                    }

                case
                nameof(Commands.delete):
                    {
                        Delete(manager);
                        break;
                    }
                case
                nameof(Commands.update):
                    {
                        Update(manager);
                        break;
                    }
                case
                nameof(Commands.show):
                    {
                        manager.ShowDataUsers();
                        break;
                    }

            }
            Console.WriteLine();

        }
        while (command != nameof(Commands.stop));


        static void Update(Manager manager)
        {
            Console.WriteLine("Введите логин для обновления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для обновления:");
            var name = Console.ReadLine();

            var count = manager.UpdateUserByLogin(login, name);

            Console.WriteLine("Строк обновлено" + count);

            manager.ShowDataUsers();
        }
        static void Add(Manager manager)
        {
            Console.WriteLine("Введите логин для добавления:");

            var login = Console.ReadLine();

            Console.WriteLine("Введите имя для добавления:");
            var name = Console.ReadLine();

            manager.AddUser(login, name);

            manager.ShowDataUsers();
        }
        static void Delete(Manager manager)
        {
            Console.WriteLine("Введите логин для удаления:");

            var count = manager.DeleteUserByLogin(Console.ReadLine());

            Console.WriteLine("Количество удаленных строк " + count);

            manager.ShowDataUsers();
        }

        manager.Disconnect();

        Console.ReadKey();

    }
}