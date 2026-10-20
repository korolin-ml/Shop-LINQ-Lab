using System;
using System.Collections.Generic;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UniversityContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var products = new List<Product>
            {
                new() { Name = "Смартфон iPhone 15", Category = "Электроника", Price = 85000, StockQuantity = 12 },
                new() { Name = "Ноутбук ASUS Vivobook", Category = "Электроника", Price = 65000, StockQuantity = 7 },
                new() { Name = "Кофемашина DeLonghi", Category = "Бытовая техника", Price = 42000, StockQuantity = 4 },
                new() { Name = "Электрочайник", Category = "Бытовая техника", Price = 2800, StockQuantity = 30 },
                new() { Name = "Беспроводные наушники", Category = "Электроника", Price = 11500, StockQuantity = 20 }
            };
            context.Products.AddRange(products);

            var customers = new List<Customer>
            {
                new() { FullName = "Александр Петров", Email = "alex@mail.ru", City = "Москва" },
                new() { FullName = "Мария Сидорова", Email = "maria@gmail.com", City = "Санкт-Петербург" },
                new() { FullName = "Дмитрий Иванов", Email = "dima@yandex.ru", City = "Москва" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();//делаем сохранение чтоб создались Id клиентов

            var order1 = new Order { OrderDate = DateTime.Now.AddDays(-3), Status = "Доставлен", CustomerId = customers[0].Id };
            var order2 = new Order { OrderDate = DateTime.Now.AddDays(-1), Status = "Новый", CustomerId = customers[1].Id };
            var order3 = new Order { OrderDate = DateTime.Now, Status = "Новый", CustomerId = customers[2].Id };

            context.Orders.AddRange(order1, order2, order3);
            context.SaveChanges();//создание Id заказов

            var orderItems = new List<OrderItem>
            {
                new() { OrderId = order1.Id, ProductId = products[0].Id, Quantity = 1, PriceAtPurchase = 85000 },
                new() { OrderId = order1.Id, ProductId = products[4].Id, Quantity = 2, PriceAtPurchase = 11500 },
                new() { OrderId = order2.Id, ProductId = products[1].Id, Quantity = 1, PriceAtPurchase = 65000 },
                new() { OrderId = order3.Id, ProductId = products[2].Id, Quantity = 1, PriceAtPurchase = 42000 },
                new() { OrderId = order3.Id, ProductId = products[3].Id, Quantity = 1, PriceAtPurchase = 2800 }
            };
            context.OrderItems.AddRange(orderItems);
            context.SaveChanges();
        }
    }
}
