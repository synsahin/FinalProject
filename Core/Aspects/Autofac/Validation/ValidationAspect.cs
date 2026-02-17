using Castle.DynamicProxy;
using Core.CrossCauttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                //IsAssignable from nesneyi interface in içine atanıp atanamayacağını kontrol eder
                //validator type koyulacak nesnein türünü belirtie doğrular
            {
                throw new System.Exception("bu bir doğrulama sınıfı değil");
                //exception = hata nesnesi
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //Gönderilen validator tipinden (örneğin ProductValidator)çalışma anında bir örnek (instance) oluştur.
            //Arka planda gizlice new ProductValidator() yapmış oluyor. intance= misal

            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //basetype = miras aldığı interface e gider
            //.GetGenericArguments(): C#'ta < > işaretleri arasındaki tipleri bir liste olarak getirir.
            //[0]: Listenin ilk (ve tek) elemanını al demektir.

            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            //metoda gönderilen tüm argümanları kontrol eder ve içlerinden istenen türe uyanları seçer
            //invoacation metodun bilgilerini toplar hazırda tutar, çağırma yürütme metodu
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
                //Bulduğun tüm o nesneleri (entity), az önce yazdığımız ValidationTool'a gönder ve kuralları işlet.
                //Eğer kurala aykırı bir şey varsa (UnitPrice < 10 gibi), burada
                //ValidationException fırlatılır ve metot daha hiç çalışmadan durur!
            }
        }
    }
}
