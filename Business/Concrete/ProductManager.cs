using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrate.EntityFramework;
using Entities.Concrate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrate
{
    public class ProductManager(IProductDal productDal) : IProductService
    {
        public List<Product> GetAll()
        {
            return productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int Id)
        {
            return productDal.GetAll(p => p.CategoryId == Id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max );

            //p.UnitPrice >= min: Ürünün fiyatı, senin belirlediğin minimum fiyattan başlasın (büyük veya eşit olsun).
            //&& (VE): "Hem soldaki şart, hem de sağdaki şart aynı anda doğru olsun" demek.
            //p.UnitPrice <= max: Ürünün fiyatı, senin belirlediğin maksimum fiyatı geçmesin(küçük veya eşit olsun).
            //Sonuç: Bu kod, fiyatı min ile max arasında olan ürünleri listeler.
        }
    }
}