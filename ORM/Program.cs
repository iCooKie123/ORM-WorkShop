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
            //ReadAllAssocs(db);

            Console.WriteLine("All Customers");
            //ReadAllCustomers(db);

            Console.WriteLine("All orders");
            //ReadAllOrders(db);

            Console.WriteLine("all orderitems");
            //ReadAllOrderItems(db);
            
            Console.WriteLine("all products");
            //ReadAllProducts(db);

            Console.WriteLine("All customers in only one assoc");
            //ReadAllCustomersWithOnlyOneAssoc(db);

            Console.WriteLine("All assocs with only one customer");
            //ReadAllAssocsWithOnlyOneCust(db);

            Console.WriteLine("find a customer by id");
            //FindCustomerById(db,GetARandomCustomer(db).CustomerId);

            Console.WriteLine("find an assoc by id");
            //FindAssociationById(db,GetARandomAssociation(db).AssociationId);

            Console.WriteLine("find a product by id");
            //FindProductById(db,GetARandomProduct(db).ProductId);

            Console.WriteLine("Add a customer");
            //AddCustomer(db,GenerateRandomCustomer(db));
            
            Console.WriteLine("Add an order");
            //AddOrder(db,GenerateRandomOrder(db));

            Console.WriteLine("Add an orderitem");
            //AddOrderItem(db,GenerateRandomOrderItem(db));

            Console.WriteLine("Add an association");
            //AddAssociation(db,GenerateRandomAssociation());

            Console.WriteLine("Add a product");
            //AddProduct(db,GenerateRandomProduct());

            Console.WriteLine("add a customer to a assoc");
            //AddCustomerToAssoc(db,GetARandomCustomer(db).CustomerId, GetARandomAssociation(db).AssociationId);

            Console.WriteLine("total orders from an assoc");
            //GetTotalOrdersForAssociation(db,GetARandomAssociation(db).AssociationId);

            Console.WriteLine("Total orders from an assoc with only loyal customers");
            //GetTotalOrdersForAssociationWithLoyalCustomer(db,GetARandomAssociation(db).AssociationId);

        }
    }

}