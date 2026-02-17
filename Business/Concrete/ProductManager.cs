using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCauttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager(IProductDal productDal,ICategoryService categoryService) : IProductService
    {
        [ValidationAspect(typeof(ProductValidator))]
        //ValidationAspect ile attributeler devreye girer 
        //ValidationAspect bir motordur, typeof(ProductValidator) ile ProductValidator sınıfındaki kuralları metoda göre uygular.
        //bu sayede kod tekrarı oluşmaz, her metodda Validation context newlenmez.
        public IResult Add(Product product)
        {
               
          IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExcided(),
              CheckIfProductCountOfCategoryCorrect(product.CategoryId));


            if (result != null)
            {
                return result;
            }
            productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);
            
            
        }
        //Required = zorunlu olduğunu belirtir,Örnek: Müşterinin Tc No verisi alınması zorunlu.
        
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                productDal.Update(product);
                return new SuccessResult();
            }
            return  new ErrorResult(Messages.ProductCountOfCategoryError);
        }



        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
            //IDataResult: Hediyenin yanındaki not kartıdır. "İşlem başarılı mı?" (Success: true/false)
            //ve "Bir mesaj var mı?" (Message: "Ürünler listelendi") gibi bilgileri taşır.
            //productList şeklinde döndürür.
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.CategoryId == Id));
        }



        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));

            //p.UnitPrice >= min: Ürünün fiyatı, senin belirlediğin minimum fiyattan başlasın (büyük veya eşit olsun).
            //&& (VE): "Hem soldaki şart, hem de sağdaki şart aynı anda doğru olsun" demek.
            //p.UnitPrice <= max: Ürünün fiyatı, senin belirlediğin maksimum fiyatı geçmesin(küçük veya eşit olsun).
            //Sonuç: Bu kod, fiyatı min ile max arasında olan ürünleri listeler.
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(productDal.GetProductDetails());
        }


       

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
            //Dal ile veri tabanına gider ve oradaki verilere bakar, categorymanager da yazılı olmamasının sebebi ürünleri kontrol ediyor olmasıdır

        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = productDal.GetAll(p => p.ProductName == productName).Any();
            //any bool, döndürür şarta uyan eleman var mı 
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAllreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExcided()
        {
            var result = categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}