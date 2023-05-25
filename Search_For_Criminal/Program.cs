using System;
using System.Collections.Generic;
using System.Linq;

namespace Search_For_Criminal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Detective detective = new Detective();
            detective.Work();
        }
    }

    class Criminal
    {
        public Criminal()
        {
            int minHeight = 150;
            int maxHeight = 200;
            int minWeigth = 50;
            int maxWeight = 120;
            int prisoner = 1;
            int wanted = 0;
            FullName = UserUntils.GenerateRandomFullName();
            Height = UserUntils.GenerateRandomNumber(minHeight, maxHeight);
            Weigth = UserUntils.GenerateRandomNumber(minWeigth, maxWeight);
            Nationality = UserUntils.GenerateRandomNationality();
            IsPrisoner = UserUntils.GenerateRandomStatus(prisoner, wanted);
        }

        public string FullName { get; private set; }
        public int Height { get; private set; }
        public int Weigth { get; private set; }
        public string Nationality { get; private set; }
        public bool IsPrisoner { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{FullName} .Рост - {Height}.Вес - {Weigth}.Национальность - {Nationality}.Статус - {IsPrisonerInfo()}.");
        }

        private string IsPrisonerInfo()
        {
            string prisoner = "Заключен";
            string wanted = "В розыске";

            if (IsPrisoner == true)
                return prisoner;
            else
                return wanted;
        }
    }

    class CriminalDatabase
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public CriminalDatabase()
        {
            CreateCriminals();
        }

        public void ShowAllCriminals()
        {
            foreach (Criminal criminal in _criminals)
            {
                criminal.ShowInfo();
            }
        }

        public void Search(int height, int weight, string nationality)
        {
            var searchResult = _criminals.Where(criminal => criminal.Height == height && criminal.Weigth == weight
            && criminal.Nationality.ToLower() == nationality && criminal.IsPrisoner == false);

            foreach (var criminal in searchResult)
            {
                criminal.ShowInfo();
            }
        }

        private void CreateCriminals()
        {
            int quantityCriminals = 100000;

            for (int i = 0; i < quantityCriminals; i++)
            {
                _criminals.Add(new Criminal());
            }
        }
    }

    class UserUntils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static string GenerateRandomFullName()
        {
            string[] names = { "Алексей ", "Иван ", "Анатолий ", "Андрей ", "Евгений " };
            string[] surnames = { "Гладков ", "Маркин ", "Акишев ", "Сотсков ", "Заварницын " };
            string[] middleNames = { "Иванович", "Алексеевич", "Николаевич", "Андреевич", "Вячеславович" };
            string fullName = "";
            int quantity = 1;

            for (int i = 0; i < quantity; i++)
            {
                fullName += surnames[_random.Next(surnames.Length)];
                fullName += names[_random.Next(names.Length)];
                fullName += middleNames[_random.Next(middleNames.Length)];
            }

            return fullName;
        }

        public static string GenerateRandomNationality()
        {
            string[] narionalitys = { "русский", "татарин", "армянин", "белорус" };
            string nationality = "";
            int quantity = 1;

            for (int i = 0; i < quantity; i++)
            {
                nationality += narionalitys[_random.Next(narionalitys.Length)];
            }

            return nationality;
        }

        public static bool GenerateRandomStatus(int prisoner, int wanted)
        {
            int status = _random.Next(wanted, prisoner + 1);
            return status == prisoner;
        }
    }

    class Detective
    {
        private CriminalDatabase _database = new CriminalDatabase();

        public void Work()
        {
            const string CommandSearchCriminals = "1";
            const string CommandShowAllSriminals = "2";
            const string CommandExit = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("С возварщением, детектив! Приятной работы!");
                Console.WriteLine($"Нажмите {CommandSearchCriminals} , чтобы начать поиск преступника");
                Console.WriteLine($"Нажмите {CommandShowAllSriminals} , чтобы показать всех преступников в базе данных");
                Console.WriteLine($"Нажмите {CommandExit} , чтобы завершить работу");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandSearchCriminals:
                        SearchCriminal();
                        break;

                    case CommandShowAllSriminals:
                        _database.ShowAllCriminals();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Я такой команды не знаю...");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void SearchCriminal()
        {
            Console.WriteLine("Введите рост преступника: ");
            string heigth = Console.ReadLine();
            Console.WriteLine("Введите вес преступника: ");
            string weight = Console.ReadLine();
            Console.WriteLine("Введите национальность преступника: ");
            string nationality = Console.ReadLine();

            if (int.TryParse(heigth, out int criminalHeight))
            {
                if (int.TryParse(weight, out int criminalWeight))
                {
                    _database.Search(criminalHeight, criminalWeight, nationality);
                }
                else
                {
                    Console.WriteLine("Вес был ввёден неккоректно...");
                }
            }
            else
            {
                Console.WriteLine("Рост был введён неккоректно...");
            }
        }
    }
}
