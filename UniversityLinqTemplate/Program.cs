using System;
using System.Globalization; //этот импорт для работы с культурой
using System.Threading;
using UniversityLinqTemplate.Data;
using UniversityLinqTemplate.Queries;

namespace UniversityLinqTemplate
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //задаем культуру, где разделитель дробной части — точка
            //решает проблему "FormatException: The input string '42000.0' was not in a correct format"
            //ваще не додумался бы сам по этому коментарий
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            using (var db = new UniversityContext())
            {
                Console.WriteLine("=== ЗАПУСК СИСТЕМЫ ИНТЕРНЕТ-МАГАЗИНА ===");
                Console.WriteLine("сброс старой схемы и наполнение базы данных тестовыми строками...");
                DbInitializer.Initialize(db);
                Console.WriteLine("база данных 'shop.db' (SQLite) успешно создана\n");

                LinqDemos.RunFilterDemo(db);
                LinqDemos.RunSortDemo(db);
                LinqDemos.RunProjectionDemo(db);
                LinqDemos.RunAggregationDemo(db);
                LinqDemos.RunGroupDemo(db);
                LinqDemos.RunEagerLoadingDemo(db);
            }

            Console.WriteLine("все запросы выполнены успешно. нажмите любую клавишу для выхода");
            Console.ReadKey();
        }
    }
}
