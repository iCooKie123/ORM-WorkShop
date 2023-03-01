using Microsoft.EntityFrameworkCore;
using ORM;
using static ORM.MethodsForRandomGeneratingThings;
using static ORM.DatabaseMethods;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var db = new DatabaseContext())
        {
            Console.WriteLine("All Assocs");
            ReadAllAssocs(db);

            Console.WriteLine("All Customers");
            ReadAllCustomers(db);

            Console.WriteLine("All orders");
            //ReadAllOrders(db);

            Console.WriteLine("all orderitems");
            //ReadAllOrderItems(db);
            
            Console.WriteLine("all products");
            //ReadAllProducts(db);

            Console.WriteLine("Add a customer");
            //AddCustomer(db,GenerateRandomCustomer(db));
            
            Console.WriteLine("Add an order");
            //AddOrder(db,GenerateRandomOrder(db));

            Console.WriteLine("Add an orderitem");
            //AddOrderItem(db,GenerateRandomOrderItem(db));

            Console.WriteLine("Add an association");
            //AddAssociation(db,GenerateRandomAssociation(db));

            Console.WriteLine("Add a product");
            //AddProduct(db,GenerateRandomProduct(db));

            Console.WriteLine("add a customer to a assoc");
            //AddCustomerToAssoc(db,GetARandomCustomer(db).CustomerId, GetARandomAssociation(db).AssociationId);

            

        }
    }

}