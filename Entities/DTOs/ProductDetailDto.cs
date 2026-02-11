using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
    //DTO= Data Transfer Object = Entities'ten farklı olarak
    //arayüzde kod çağırılırken kolaylık sağlar bütün metot yazılmaz
{
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; } 
    }
}
