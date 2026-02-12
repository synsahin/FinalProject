using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
/*GetProductDetails() (Metot Adı)
Bu isimlendirme, metodun amacını söyler: 
"Ürün detaylarını getir." Genellikle sadece ürün tablosundaki verileri değil,
ürünle ilişkili diğer tabloların (Kategori adı, Tedarikçi adı vb.) 
birleştirilmiş halini getirmek için kullanılır.


ProductDetailDto (Listenin İçeriği)
Bu liste Product (Saf ürün tablosu) nesnelerini değil, senin özel olarak tasarladığın 
DTO (Data Transfer Object) nesnelerini taşıyor.

Product: Sadece ProductId, ProductName, CategoryId gibi ham verileri içerir.
ProductDetailDto: ProductName yanına CategoryName (Kategori Adı) gibi 
birleştirilmiş ve filtrelenmiş verileri içerir.

Yani bu kod "Bana öyle bir metot yap ki; veritabanına gitsin, ürünler ile 
diğer ilişkili tabloları birleştirsin ve bu birleşmiş verileri
ProductDetailDto kalıbına dökerek bana bir liste halinde geri getirsin." der.*/


    }
}
 