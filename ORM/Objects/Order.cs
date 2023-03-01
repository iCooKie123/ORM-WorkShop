namespace ORM
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime? Created { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public Customer Customer { get; set; }

    }
}
