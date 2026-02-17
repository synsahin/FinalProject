using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=> p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p=> p.UnitPrice).NotEmpty();
            RuleFor(p=> p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1).WithMessage("ürün fiyatı 10dan büyük veya eşit olmalı");
            //Kuralİçin(p değerei => UnitPrice ın p değeri 10dan daha büyük or eşit p => CategoryId si 1 olanların.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("ProductName A harfi ile başlamalı");
        }

        private void WithMessage(string x)
        {
            throw new NotImplementedException();
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
