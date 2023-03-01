using ORM.Objects;

namespace ORM
{
    public class MethodsForRandomGeneratingThings
    {
        public static void GenerateRandom100Products()
        {
            var products = new List<Product>();
            for (int i = 0; i < 100; i++)
            {
                var product = GenerateRandomProduct();
                products.Add(product);
            }
            using var context = new DatabaseContext();
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static double GenerateRandomPrice()
        {
            var random = new Random();
            return random.NextDouble() * 100;
        }

        public static string GenerateRandomDescription()
        {
            var random = new Random();

            String[] desc = { "book", "car", "broom", "phone", "headphones", "charger", "TV", "Rubik's cube", "knife", "screwdriver", "WD40",
            "pack of cigars","pen","pencil","cheeseburger","beer","wine","whiskey","pizza"};

            return desc[random.Next(desc.Length)];
        }

        public static Product GenerateRandomProduct()
        {
            var price = GenerateRandomPrice();
            var desc = GenerateRandomDescription();
            var product = new Product { Description = desc, Price = price };
            return product;
        }

        public static OrderItem GenerateRandomOrderItem(DatabaseContext db)
        {
            var random = new Random();
            var quantity = random.Next(1, 10);
            var product = GetARandomProduct(db);
            var orderItem = new OrderItem { Quantity = quantity, Product = product };
            return orderItem;


        }

        public static Product GetARandomProduct(DatabaseContext db)
        {
            var possibleItems = db.Products.ToList();
            if (possibleItems.Any())
            {
                var random = new Random();
                var product = possibleItems[random.Next(possibleItems.Count)];
                return product;
            }
            else
            {
                throw new Exception("There are no products in the database");
            }
        }

        public static OrderItem GetARandomOrderItem(DatabaseContext db)
        {
            var possibleOrderItems = db.OrderItems.ToList();
            if (possibleOrderItems.Any())
            {
                var random= new Random();
                return possibleOrderItems[random.Next(possibleOrderItems.Count)];
            }
            else
            {
                throw new Exception("No orderItems in the db");
            }
        }

        public static Customer GetARandomCustomer(DatabaseContext db)
        {
            var possibleCustomers = db.Customers.ToList();
            if (possibleCustomers.Any())
            {
                var random = new Random();
                return possibleCustomers[random.Next(possibleCustomers.Count)];
            }
            else
            {
                throw new Exception("No customers in the db");
            }
        }

        public static Association GetARandomAssociation(DatabaseContext db)
        {
            var possibleAssociations = db.Associations.ToList();
            if (possibleAssociations.Any())
            {
                var random = new Random();
                return possibleAssociations[random.Next(possibleAssociations.Count)];
            }
            else
            {
                throw new Exception("No associations in the db");
            }
        }

        public static Order GenerateRandomOrder(DatabaseContext db)
        {
            var random = new Random();
            var numberOfItems = random.Next(10);
            var customer = GetARandomCustomer(db);
            List<OrderItem> OrderItemList = new List<OrderItem>();
            
            for (int i = 0; i < numberOfItems; i++)
            {
                var orderItem = GetARandomOrderItem(db);
                OrderItemList.Add(orderItem);
            }
            var order = new Order() { Created = DateTime.Now, Items = OrderItemList,Customer=customer };
            return order;
        }

        public static string GenerateRandomCustomerName()
        {
            var random = new Random();
            string[] names = { "Alex", "Antonia", "Bogdan", "Cristina", "Daniel", "Elena", "Florin", "Gabriela", "Ionut", "Larisa", "Mihai", "Nicoleta", "Ovidiu", "Raluca", "Stefan", "Teodora", "Victor", "Zoia", "Andrei", "Maria" };
            return names[random.Next(names.Length)];
        }

        public static string GenerateRandomAssociationName()
        {
            var random= new Random();
            string[] associationNames = { "ARE", "Pro Arte", "APA", "Dinamo", "ANB", "ATE", "ADO", "ADR", "APS", "AAA" };
            return associationNames[random.Next(associationNames.Length)];
        }

        public static Customer GenerateRandomCustomer(DatabaseContext db)
        {
            
            var name = GenerateRandomCustomerName();
            List<Association> assocList= new List<Association>();
            var assoc = GetARandomAssociation(db);
            assocList.Add(assoc);
            var customer = new Customer { Name = name, Associations=assocList };
            return customer;
        }

        public static Association GenerateRandomAssociation()
        {
            var name = GenerateRandomAssociationName();
            return new Association { Name = name };
        }
    }
}
