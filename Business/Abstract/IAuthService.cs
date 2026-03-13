using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Text;


namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        //register = kayıt
        //sadece User nesnesi değil, UserForRegisterDto alınır. Yani kullanıcının adı, soyadı, e-postası gibi
        //bilgilerle birlikte şifresini string olarak alıp, veri tabanına güvenli bir şekilde kaydedecğini belirtir.
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        // IDataResult<User> : IDataResult içinden sadece user datalarının çekileceğini belirtir.
        //Sisteme girmek isteyen kişinin bilgilerini doğrular
        //Kullanıcının e-posta ve şifresini (UserForLoginDto) alır. Veri tabanındaki kaydedilen şifreyle, kullanıcının yazdığı şifreyi karşılaştırır.
        IResult UserExists(string email);
        //Kayıt aşamasında "Aynı e-posta ile ikinci kez kayıt olunmasın" kontrolünü yapar.
        IDataResult<AccessToken> CreateAccessToken(User user);
        // Kullanıcı giriş yaptıktan sonra, onun her işlemde kullanacağı "Dijital Kimlik Kartını" (Token) oluşturur.
    }
}
