using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal
    {
        List<Product> GetAll(Expression<Func<Product, bool>> filter = null);
        void Update(Product entity);
    }
}
