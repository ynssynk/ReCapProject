using Core.Entities.Concrete;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ArgumentNull = "İçerik null olamaz";
        public static string CarDailyPriceInValid = "Girilen günlük 0 dan büyük olmaldır.";
        public static string CarAdded = "Yeni araç başarı ile eklendi";
        public static string CarUpdated = "Araç başarı ile güncellendi";
        public static string CarDeleted = "Silme işlemi başarılı";
        public static string CarsListed = "Tüm araçlar listelendi";
        public static string BrandNameInValid = "Marka ismi geçersiz.";
        public static string SuccessAdded = "Başarılı bir şekilde eklendi";
        public static string ErrorAdded = "Ekleme işlemi başarılı değil";
        public static string Success = "Yapılan işlem başarılı";
        public static string Error = "Yapılan işlem başarısız";
        public static string CarAlreadyExists = "Araç zaten şu anda kiralık";
        public static string CarImagesCountLimit = "Bir araça en fazla 5 resim yüklenebilir.";
        public static string UserRegistered = "Kullanıcı başarılı bir şekilde kaydedildi";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError="Parola hatalı";
        public static string SuccessfulLogin = "Giriş işlemi başarılı";
        public static string UserAlreadtExists = "Kullanıcı zaten mevcut";
        public static string AccessTokenCreated = "Token başarılı bir şekilde oluşturuldu";
        public static string AuthorizationDenied = "Yetkiniz yok";
    }
}