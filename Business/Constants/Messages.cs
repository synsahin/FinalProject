using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "ürün eklendi";
        public static string ProductNameInvalid = "ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductsListed= "Ürünler Listelendi";
        public static string ProductCountOfCategoryError = "bir kategoride en fazla 10 adet ürün bulunabilir";
        public static string ProductNameAllreadyExists = "Bu isimde bir ürün zaten var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni kategori eklenemiyor";
        public static string AuthorizationDenied = "Yetkisizsin";
        public static string AccessTokenCreated = "Giriş başarılı!";
    }
}