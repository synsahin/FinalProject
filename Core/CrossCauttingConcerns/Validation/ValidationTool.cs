using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCauttingConcerns.Validation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator,object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            //result.IsValid, FluentValidation kütüphanesinde yaptığın doğrulama (validation)
            //işleminin başarılı olup olmadığını sana söyleyen bir "Evet/Hayır" (bool) anahtarıdır.
            //! değili anlamında kullanılır
        }
    }
}
