using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Перша частина лабораторної: порівняння масивів через делегат.
        int[] firstArray = { 5, 8, 2, 10, 7 };
        int[] secondArray = { 3, 8, 6, 4, 7 };

        Console.WriteLine("ПЕРША ЧАСТИНА. ПОРІВНЯННЯ МАСИВІВ");
        Console.WriteLine("Перший масив:  " + string.Join(" ", firstArray));
        Console.WriteLine("Другий масив:  " + string.Join(" ", secondArray));

        // Виклик через делегат, створений анонімним методом.
        ArrayComparisonDelegate compareAnonymous = delegate (int[] first, int[] second)
        {
            ArrayComparer.CompareValues(first, second);
        };

        Console.WriteLine("\nВиклик через анонімний метод:");
        compareAnonymous(firstArray, secondArray);

        // Виклик через делегат, створений лямбда-виразом.
        ArrayComparisonDelegate compareLambda = (first, second) => ArrayComparer.CompareValues(first, second);

        Console.WriteLine("\nВиклик через лямбда-вираз:");
        compareLambda(firstArray, secondArray);

        // Друга частина лабораторної: робота Internet-послуги та події.
        Console.WriteLine("\n\nДРУГА ЧАСТИНА. INTERNET-ПОСЛУГА");

        InternetService internet = new InternetService();
        EventHandlers handlers = new EventHandlers();

        // Підписка на подію.
        internet.TrafficExceeded += handlers.OnTrafficExceeded;

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Обрати тариф");
            Console.WriteLine("2. Поповнити рахунок");
            Console.WriteLine("3. Активувати доступ до Internet");
            Console.WriteLine("4. Використати Internet-трафік");
            Console.WriteLine("5. Показати стан послуги");
            Console.WriteLine("0. Вийти");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SelectTariff(internet);
                    break;
                case "2":
                    Console.Write("Введіть суму поповнення (грн): ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        internet.TopUp(amount);
                    else
                        Console.WriteLine("Некоректна сума.");
                    break;
                case "3":
                    internet.Connect();
                    break;
                case "4":
                    Console.Write("Введіть обсяг використаного трафіку (ГБ): ");
                    if (double.TryParse(Console.ReadLine(), out double traffic))
                        internet.UseInternet(traffic);
                    else
                        Console.WriteLine("Некоректне значення.");
                    break;
                case "5":
                    internet.ShowStatus();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Такого пункту меню немає.");
                    break;
            }
        }

        // Відписка від події перед завершенням програми.
        internet.TrafficExceeded -= handlers.OnTrafficExceeded;
    }

    static void SelectTariff(InternetService internet)
    {
        Console.WriteLine("\nОберіть тариф:");
        Console.WriteLine("1. Старт — 10 ГБ, 100 грн/місяць");
        Console.WriteLine("2. Стандарт — 30 ГБ, 200 грн/місяць");
        Console.WriteLine("3. Максимум — 100 ГБ, 350 грн/місяць");
        Console.Write("Ваш вибір: ");

        switch (Console.ReadLine())
        {
            case "1":
                internet.SelectTariff("Старт", 100, 10);
                break;
            case "2":
                internet.SelectTariff("Стандарт", 200, 30);
                break;
            case "3":
                internet.SelectTariff("Максимум", 350, 100);
                break;
            default:
                Console.WriteLine("Невідомий тариф.");
                break;
        }
    }
}
