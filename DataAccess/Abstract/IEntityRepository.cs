using DataAccess.Abstract;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        //ürünleri filtreler ve getirir.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        // bunu sayesinde her seferinde yeniden
        // void metodlarından eklemeye gerek kalmayacak
    }
}