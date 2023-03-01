namespace ORM.Objects
{
    public class Association
    {
        public int AssociationId { get; set; }
        public string Name { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
