using System.Collections.Generic;

namespace UniversityLinqTemplate.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string City { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
