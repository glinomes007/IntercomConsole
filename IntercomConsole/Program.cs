using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace IntercomConsole
{
    internal class Program
    {
        private static List<Shape> _shapes;
        private static List<Material> _materials;
        private static List<string> _colors;

        private static int _autoIncrement = 0;

        private readonly static bool _running = true;

        private readonly static string _path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\IntercomConsole";

        private static List<FreeIntercom> _freeIntercom = new List<FreeIntercom>();

        static void Main()
        {
            Console.CursorVisible = false;

            Directory.CreateDirectory(_path);
            LoadDataFromBinaryFile();

            _shapes = new List<Shape>()
            {
                new Shape("Аудиодомофон", 3000),
                new Shape("Видеодомофон", 7000)
            };

            _materials = new List<Material>()
            {
                new Material("Пластик", 1000),
                new Material("Алюминий", 2000),
                new Material("Сталь", 2500)
            };
            _colors = new List<string>()
            {
                "неокрашенный",
                "чёрный",
                "белый",
                "жёлтый",
                "зелёный",
                "фиолетовый"
            };

            while (_running)
            {
                Console.WriteLine("Здравствуйте");
                ShowStartMenu();
            }
        }

        private static void ShowStartMenu()
        {
            Console.Clear();
            Console.WriteLine("Выберите пункты меню\n" +
                "1) Купить домофон\n" +
                "2) Показать готовые домофоны\n");

            ConsoleKeyInfo charKey = Console.ReadKey();
            switch (charKey.Key)
            {
                case ConsoleKey.D1:
                    CreateIntercom();
                    break;
                case ConsoleKey.D2:
                    ShowListIntercoms();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Такого пункта меню нету, попробуйте ещё раз");
                    Task.Delay(500).Wait();
                    break;
            }
        }
        private static void CreateIntercom()
        {
            string caption = ChooseCaption();
            Material material = ChooseMaterial();
            Shape shape = ChooseShape();
            string color = ChooseColor();

            Console.Clear();

            _freeIntercom.Add(new FreeIntercom(caption, shape, material, color));
            SaveDataToBinaryFile();

            Console.WriteLine("Замок успешно создан!\nНажмите клавишу, чтобы попасть в главное меню");
            Console.ReadKey();
        }

        private static string ChooseCaption()
        {
            Console.CursorVisible = true;

            Task.Delay(300);
            Console.Clear();

            string caption;

            Console.WriteLine("Придумайте надпись на домофон");
            caption = Console.ReadLine();

            if (caption == null)
                return "Intercom";
            else
                return caption;

        }

        private static Material ChooseMaterial()
        {
            Console.CursorVisible = false;

            while (true)
            {
                Task.Delay(300);
                Console.Clear();

                Console.WriteLine("Выберите материал\n");

                for (int i = 0; i < _materials.Count; i++)
                    Console.WriteLine($"{i + 1}) {_materials[i].Name} {_materials[i].Price}руб");

                ConsoleKeyInfo charKey = Console.ReadKey();
                switch (charKey.Key)
                {
                    case ConsoleKey.D1:
                        return _materials[0];

                    case ConsoleKey.D2:
                        return _materials[1];

                    case ConsoleKey.D3:
                        return _materials[2];

                    default:
                        Console.WriteLine("Такого материала нету, попробуйте ещё раз");
                        Task.Delay(500).Wait();
                        continue;
                }
            }
        }
        private static Shape ChooseShape()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Выберите вид домофона\n");

                for (int i = 0; i < _shapes.Count; i++)
                    Console.WriteLine($"{i + 1}) {_shapes[i].Name} {_shapes[i].Price}руб");

                ConsoleKeyInfo charKey = Console.ReadKey();
                switch (charKey.Key)
                {
                    case ConsoleKey.D1:
                        return _shapes[0];

                    case ConsoleKey.D2:
                        return _shapes[1];

                    default:
                        Console.WriteLine("Такого вида нету, попробуйте ещё раз");
                        Task.Delay(500).Wait();
                        continue;
                }
            }
        }
        private static string ChooseColor()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Выберите цвет\n");

                for (int i = 0; i < _colors.Count; i++)
                    Console.WriteLine($"{i + 1}) {_colors[i]}");

                ConsoleKeyInfo charKey = Console.ReadKey();
                switch (charKey.Key)
                {
                    case ConsoleKey.D1:
                        return _colors[0];

                    case ConsoleKey.D2:
                        return _colors[1];

                    case ConsoleKey.D3:
                        return _colors[2];

                    case ConsoleKey.D4:
                        return _colors[3];

                    case ConsoleKey.D5:
                        return _colors[4];

                    case ConsoleKey.D6:
                        return _colors[5];

                    default:
                        Console.WriteLine("Такого цвета нету, попробуйте ещё раз");
                        Task.Delay(500).Wait();
                        continue;
                }
            }
        }

        private static void ShowListIntercoms()
        {
            while (true)
            {
                Console.Clear();

                for (int i = 0; i < _freeIntercom.Count; i++)
                    Console.WriteLine($"{i + 1}) {_freeIntercom[i].Caption} {_freeIntercom[i].Shape}" +
                        $" {_freeIntercom[i].Material} {_freeIntercom[i].Color} {_freeIntercom[i].Price}");

                Console.WriteLine("\nВыберите действие\n" +
                    "1) удалить домофон\n" +
                    "2) редактировать домофон\n" +
                    "3) отсортировать список\n" +
                    "4) вернуться в главное меню\n");

                ConsoleKeyInfo charKey = Console.ReadKey();
                switch (charKey.Key)
                {
                    case ConsoleKey.D1:
                        DeleteIntercom();
                        break;
                    case ConsoleKey.D2:
                        EditIntercom();
                        break;
                    case ConsoleKey.D3:
                        SortIntercoms();
                        break;
                    case ConsoleKey.D4:
                        ShowStartMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Такого пункта меню нету, попробуйте ещё раз");
                        Task.Delay(500).Wait();
                        break;
                }
            }
        }

        private static void DeleteIntercom()
        {
            Console.WriteLine("Введите номер домофона, который хотите удалить");
            int indexToRemove;
            try
            {
                indexToRemove = int.Parse(Console.ReadKey().KeyChar.ToString()) - 1;
            }
            catch 
            {
                Console.WriteLine("Индекс находится вне границ массива");
                Task.Delay(500).Wait();
                return;
            }

            if (indexToRemove < 0 || indexToRemove >= _freeIntercom.Count)
            {
                Console.WriteLine("Индекс находится вне границ массива");
                Task.Delay(500).Wait();
                return;
            }

            _freeIntercom.RemoveAt(indexToRemove);
            SaveDataToBinaryFile();
            ShowListIntercoms();
        }

        private static void EditIntercom()
        {
            Console.WriteLine("Введите номер домофона, который хотите редактировать");
            int indexToEdit;
            try
            {
                indexToEdit = int.Parse(Console.ReadKey().KeyChar.ToString()) - 1;
            }
            catch
            {
                Console.WriteLine("Индекс находится вне границ массива");
                Task.Delay(500).Wait();
                return;
            }

            if (indexToEdit < 0 || indexToEdit >= _freeIntercom.Count)
            {
                Console.WriteLine("Индекс находится вне границ массива");
                Task.Delay(500).Wait();
                return;
            }

            string caption = ChooseCaption();
            Material material = ChooseMaterial();
            Shape shape = ChooseShape();
            string color = ChooseColor();

            _freeIntercom[indexToEdit] = new FreeIntercom(caption, shape, material, color);
            SaveDataToBinaryFile();
            ShowListIntercoms();

        }
        private static void SortIntercoms()
        {
            if (_autoIncrement % 2 == 0)
                _freeIntercom = _freeIntercom.OrderBy(x => x.Price).ToList();
            else
                _freeIntercom = _freeIntercom.OrderByDescending(x => x.Price).ToList();

            _autoIncrement++;
            SaveDataToBinaryFile();
            ShowListIntercoms();
        }

        private static void SaveDataToBinaryFile()
        {
            using (FileStream fileStream = new FileStream($@"{_path}\intercom_data.dat", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, _freeIntercom);
            }
        }
        private static void LoadDataFromBinaryFile()
        {
            if (File.Exists($@"{_path}\intercom_data.dat"))
            {
                using (FileStream fileStream = new FileStream($@"{_path}\intercom_data.dat", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _freeIntercom = (List<FreeIntercom>)formatter.Deserialize(fileStream);
                }
            }
        }
    }

    [Serializable]
    internal class FreeIntercom : Intercom
    {
        public FreeIntercom(string caption, Shape shape, Material material, string color) : base(caption, shape, material, color) { }
    }
}
