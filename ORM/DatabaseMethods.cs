using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ORM.Objects;

namespace ORM
{
    public class DatabaseMethods
    {
        //Methods for reading stuff
        public static void ReadAllProducts(DatabaseContext db)
        {
            var items = db.Products.ToList();
            foreach (var item in items)
            {
                Console.WriteLine("{0} {1} {2}", item.ProductId, item.Description, item.Price);
            }
        }

        public static void ReadAllOrderItems(DatabaseContext db)
        {
            var orderItems = db.OrderItems.ToList();
            foreach (var orderItem in orderItems)
            {
                Console.WriteLine("{0} {1} {2}", orderItem.OrderItemId, orderItem.Quantity, orderItem.Product);
            }
        }

        public static void ReadAllOrders(DatabaseContext db)
        {
            var orders = db.Orders.Include(x => x.Customer).Include(x => x.Items).ThenInclude(c => c.Product).ToList();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order {order.OrderId}");
                Console.WriteLine($"{order.Created}");
                Console.WriteLine($"{order.Customer.CustomerId}");
                foreach (var item in order.Items)
                {
                    Console.WriteLine(item.Product.Description);
                }
                Console.WriteLine();
            }
        }

        public static void ReadAllCustomers(DatabaseContext db)
        {
            var customers = db.Customers.Include(x => x.Associations).Include(x => x.Orders).ToList();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
                foreach (var a in customer.Associations.ToList())
                {
                    Console.WriteLine(a.Name);
                }
                foreach (var c in customer.Orders)
                {
                    Console.WriteLine(c.OrderId);
                }
                Console.WriteLine();
            }
        }

        public static void ReadAllAssocs(DatabaseContext db)
        {
            var assocs = db.Associations.Include(x => x.Customers).ToList();
            foreach (var a in assocs)
            {
                Console.WriteLine(a.Name);
                foreach (var c in a.Customers.ToList())
                {
                    Console.WriteLine(c.Name);
                }
                Console.WriteLine();
            }
        }

        public static void ReadAllCustomersWithOnlyOneAssoc(DatabaseContext db)
        {
            var cust= db.Customers.Where(x => x.Associations.Count == 1).Include(x=>x.Associations).ToList();
            foreach (var c in cust)
            {
                Console.WriteLine(c.Name);
                foreach (var a in c.Associations)
                {
                    Console.WriteLine(a.Name);
                }
                Console.WriteLine();
            }
        }

        public static void ReadAllAssocsWithOnlyOneCust(DatabaseContext db)
        {
            var assocs = db.Associations.Where(x => x.Customers.Count == 1).ToList();
            foreach(var a in assocs)
            {
                Console.WriteLine(a.Name);
            }
        }

        //Methods for adding stuff
        public static void AddOrderItem(DatabaseContext db, OrderItem orderItem)
        {
            db.OrderItems.Add(orderItem);
            db.SaveChanges();
            Console.WriteLine($"OrderItem {orderItem.OrderItemId},{orderItem.Quantity},{orderItem.Product.Description} was added to the db");
        }

        public static void AddOrder(DatabaseContext db, Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            Console.WriteLine($"Order {order.OrderId},{order.Created},{order.Items.Count} items was added into the db");
        }

        public static void AddCustomer(DatabaseContext db, Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            Console.WriteLine($"Customer {customer.Name},{customer.Associations} was added to the db");
        }

        public static void AddAssociation(DatabaseContext db, Association association)
        {
            db.Associations.Add(association);
            db.SaveChanges();
            Console.WriteLine($"Assoc {association.Name} was added to the db");
        }

        public static void AddProduct(DatabaseContext db, Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            Console.WriteLine($"Product {product.Description},{product.Price} was added into the database");
        }

        public static void AddCustomerToAssoc(DatabaseContext db, int custId, int assocId)
        {
            var assoc = db.Associations.Include(x => x.Customers).ToList();
            var cust = FindCustomerById(db, custId);
            foreach (var a in assoc)
            {
                if (a.AssociationId == assocId)
                {
                    a.Customers.Add(cust);
                    db.SaveChanges();
                    Console.WriteLine($"Customer {cust.Name} was added into {a.Name}");
                }
            }
        }



        //Methods for removing stuff
        public static void RemoveProduct(DatabaseContext db, Product product)
        {
            db.Products.Remove(product);
            db.SaveChanges();
            Console.WriteLine($"Product {product.Description},{product.Price} was removed from the database");
        }
        //nuclear
        public static void RemoveAllProducts(DatabaseContext db)
        {
            var products = db.Products.ToList();
            db.RemoveRange(products);
            db.SaveChanges();
            Console.WriteLine("All products were removed from the database");
        }
        //nuclear
        public static void RemoveAllOrders(DatabaseContext db)
        {
            var orders = db.Orders.ToList();
            db.RemoveRange(orders);
            db.SaveChanges();
            Console.WriteLine("removed all orders");
        }



        //Methods for finding stuff
        public static Product FindProductById(DatabaseContext db, int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                throw new Exception("Product was not found");
            return product;
        }
        public static Customer FindCustomerById(DatabaseContext db, int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
                throw new Exception($"there is no customer with id {id}");
            return customer;
        }

        public static Association FindAssociationById(DatabaseContext db, int id)
        {
            var association = db.Associations.Find(id);
            if (association == null)
            {
                throw new Exception($"there is no association with id {id}");
            }
            return association;
        }

        //HOMEWORK

        public static double GetTotalOrdersForAssociation(DatabaseContext db, int associationId)
        {
            double sum = 0;


            var association = db.Associations.Include(a => a.Customers)
                                                        .ThenInclude(c=>c.Associations)
                                                        .Include(a => a.Customers)
                                                        .ThenInclude(c => c.Orders)
                                                        .ThenInclude(o => o.Items)
                                                        .ThenInclude(i => i.Product)
                                                        .FirstOrDefault(a => a.AssociationId == 1);

            if (association == null)
                throw new Exception("Association not found");
            foreach (var customer in association.Customers)
            {
                foreach (var order in customer.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        sum += item.Product.Price * item.Quantity;
                    }
                }
            }

            return sum;
        }


        public static double GetTotalOrdersForAssociationWithLoyalCustomer(DatabaseContext db, int associationId)
        {
            double sum = 0;

            var association = db.Associations.Include(a => a.Customers)
                                                        .ThenInclude(c => c.Associations)
                                                        .Include(a => a.Customers)
                                                        .ThenInclude(c => c.Orders)
                                                        .ThenInclude(o => o.Items)
                                                        .ThenInclude(i => i.Product)
                                                        .FirstOrDefault(a => a.AssociationId == 1);
            if (association == null)
                throw new Exception("Association not found");
            foreach (var customer in association.Customers)
            {
                if (customer.Associations.ToList().Count == 1)
                {
                    foreach (var order in customer.Orders)
                    {
                        foreach (var item in order.Items)
                        {
                            sum += item.Product.Price * item.Quantity;
                        }
                    }
                }

            }
            return sum;
        }
    }
}
