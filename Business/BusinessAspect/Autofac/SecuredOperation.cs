using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IOC;
using Business.Constants;
using Core.Extentions;

namespace Business.BusinessAspect.Autofac
{
    public class SecuredOperation:MethodInterception
    //MethodInterception: Bu sınıf bir AOP yapısıdır.
    //Metodun çalışma anına müdahale edebileceğini araya girebileceğini belirtir.
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            //roles.Split(','): "x,y" diye bir metin gönderildiğinde, bu kod virgülleri ayırır ve ["x", "y"] şeklinde bir liste verir.
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            //IHttpContextAccessor:O an sisteme istek atan kişinin kim olduğuna, hangi "token" (kimlik) olduğuna bakabilmek için tarayıcı isteğine erişimi sağlar.
            //ServiceProvider:product manager istendiğinde EfProductDal ver dendiğinde EfProductDal ı hazırlar newlemekten kurtarır
            //GetService: < IHttpContextAccessor > ile ServiceProvider içinden IHttpContextAccessor tipinde olan nesneyi getirir

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            //_httpContextAccessor: O anki HTTP isteğinin içine bakar.
            //User: İsteği atan kişinin(Token sahibi) kimlik bilgilerine ulaşır.
            //ClaimRoles(): Kullanıcının (Token içindeki) rollerini çıkarır. Örneğin: ["editor", "guest"].

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    // rolleri,n içini gezer yetkili rol ise true döndürür
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
            //değilse yetkisiz erroru döneer
        }
    }
}
