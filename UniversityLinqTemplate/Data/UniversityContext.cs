using Microsoft.EntityFrameworkCore;
using UniversityLinqTemplate.Models;

namespace UniversityLinqTemplate.Data
{
    public class UniversityContext : DbContext
    {
        //регистрируем новые таблицы в контекст Entity Framework
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=shop.db");
        }
    }
}
