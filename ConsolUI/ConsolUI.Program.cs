using Business.Concrate;
using DataAccess.Concrate.EntityFramework;
using DataAccess.Concrate.InMemory;
using Entities.Concrate;

namespace ConsolUI

{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (Product product in productManager.GetByUnitPrice(50,100))
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}