using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using NorthwindContext context = new NorthwindContext();
            var result = from p in context.Products
                         join c in context.Categories
                         on p.CategoryId equals c.CategoryId
                         select new ProductDetailDto
                         {
                             ProductId = p.ProductId,
                             CategoryId = c.CategoryId,
                             ProductName = p.ProductName,
                             UnitsInStock = p.UnitsInStock,
                             UnitPrice = p.UnitPrice
                         };
            return [.. result];
        }
    }
}