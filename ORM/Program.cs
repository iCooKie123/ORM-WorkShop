using Microsoft.EntityFrameworkCore;
using ORM;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var db = new DatabaseContext())
        {
            //AddProduct(db);
            ///AddOrderItem(db);

            // AddOrder(db);
            RemoveProduct(db);

            var orders = db.Orders;
            foreach (var order in orders)
            {
                Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            }
        }
    }

    private static void RemoveProduct(DatabaseContext db)
    {
        var product = db.Products.Find(3);
        db.Products.Remove(product);
        db.SaveChanges();
    }

    private static void AddOrder(DatabaseContext db)
    {
        var orderItem = db.OrderItems.First();
        var order = new Order() { Created = DateTime.Now };
        db.Orders.Add(order);
        db.SaveChanges();

        order = db.Orders.Include(o => o.Items).First();
        order.Items.Add(orderItem);
        db.SaveChanges();
    }

    private static void AddOrderItem(DatabaseContext db)
    {
        var product = db.Products.First();

        if (product != null)
        {
            var item = new OrderItem
            {
                Quantity = 1,
                Product = product
            };
            db.OrderItems.Add(item);
            db.SaveChanges();

            Console.WriteLine("{0} {1} Product: {2}", item.OrderItemId, item.Quantity, item.Product.Description);
        }
    }

    private static void AddProduct(DatabaseContext db)
    {
        var product = new Product() { Price = 50, Description = "BooK" };
        db.Products.Add(product);
        db.SaveChanges();

        foreach (var p in db.Products)
        {
            Console.WriteLine("{0} {1} {2}", p.ProductId, p.Description, p.Price);
        }
    }
}