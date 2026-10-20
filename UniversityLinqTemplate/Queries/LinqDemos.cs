using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniversityLinqTemplate.Data;

namespace UniversityLinqTemplate.Queries
{
    public static class LinqDemos
    {
        //фильтрация с логическими операторами и
        public static void RunFilterDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 1. ФИЛЬТРАЦИЯ (Категория 'Электроника' дороже 15 000 руб.) ---");
            var result = db.Products
                .Where(p => p.Category == "Электроника" && p.Price > 15000)
                .ToList();

            foreach (var p in result)
            {
                Console.WriteLine($"Товар: {p.Name} | Категория: {p.Category} | Цена: {p.Price} руб.");
            }
        }

        //сортировка
        public static void RunSortDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 2. СОРТИРОВКА (По алфавиту категорий, затем по цене убывания) ---");
            var result = db.Products
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.Price)
                .ToList();

            foreach (var p in result)
            {
                Console.WriteLine($"[{p.Category}] {p.Name} — Цена: {p.Price} руб. (Остаток: {p.StockQuantity})");
            }
        }

        //select в новый анонимный тип)
        public static void RunProjectionDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 3. ПРОЕКЦИЯ (Выборка только определенных полей: Имя и Город) ---");
            var result = db.Customers
                .Select(c => new
                {
                    Покупатель = c.FullName,
                    ГородПроживания = c.City
                })
                .ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"Клиент: {item.Покупатель}, г. {item.ГородПроживания}");
            }
        }

        //агрегация
        public static void RunAggregationDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 4. АГРЕГАЦИЯ (Статистические показатели по товарам) ---");
            int totalCount = db.Products.Count();
            decimal maxPrice = db.Products.Max(p => p.Price);
            double avgPrice = (double)db.Products.Average(p => p.Price);

            Console.WriteLine($"Всего уникальных товаров в базе: {totalCount}");
            Console.WriteLine($"Самый дорогой товар стоит: {maxPrice} руб.");
            Console.WriteLine($"Средняя стоимость товаров в магазине: {avgPrice:F2} руб.");
        }

        //группировка записей
        public static void RunGroupDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 5. ГРУППИРОВКА (Анализ категорий товаров) ---");
            var groups = db.Products
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    НазваниеКатегории = g.Key,
                    КоличествоВидов = g.Count(),
                    МинимальнаяЦена = g.Min(p => p.Price)
                })
                .ToList();

            foreach (var g in groups)
            {
                Console.WriteLine($"Категория: {g.НазваниеКатегории} | Количество позиций: {g.КоличествоВидов} | Мин. цена: {g.МинимальнаяЦена} руб.");
            }
        }

        //загрузка связей Include и ThenInclude чтобы не было ошибки "NullReferenceException"
        public static void RunEagerLoadingDemo(UniversityContext db)
        {
            Console.WriteLine("\n--- 6. ЖАДНАЯ ЗАГРУЗКА СВЯЗЕЙ (Заказы -> Клиенты -> Позиции -> Детали товаров) ---");
            var orders = db.Orders
                .Include(o => o.Customer)                    //инфа о покупателе
                .Include(o => o.OrderItems)                   //список позиций в чеке
                    .ThenInclude(oi => oi.Product)            //данные товара для позиции
                .ToList();

            foreach (var o in orders)
            {
                Console.WriteLine($"Заказ №{o.Id} от {o.OrderDate.ToShortDateString()} | Покупатель: {o.Customer.FullName} | Статус: {o.Status}");
                foreach (var item in o.OrderItems)
                {
                    Console.WriteLine($"   -> [Товар] {item.Product.Name} | Кол-во: {item.Quantity} шт. | Цена покупки: {item.PriceAtPurchase} руб.");
                }
            }
        }
    }
}
