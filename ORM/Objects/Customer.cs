using ORM.Objects;

namespace ORM
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Association> Associations { get; set; }
    }
}
