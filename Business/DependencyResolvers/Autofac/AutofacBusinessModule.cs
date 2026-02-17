using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            /*Sisteme diyorsun ki: "Eğer birisi senden IProductService (arayüz) isterse, ona git ProductManager (gerçek sınıf) nesnesini ver."
            SingleInstance(): "Bunu sadece bir kere üret."*/
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            //Autofac'e " şu an içinde bulunduğun bu projeyi tara "komutunu vermek için bir adres alınır.
            //assembly = dosya paketi yakalama tüm sınıfları barındırır busines ın derlenmiş hali, executting = çalışıyor olma durumunu belirtir
            //reflection = kod çalışırken kendi yapısını (sınıflarını, metotlarını, niteliklerini) göremez. Reflection,
            //koda çalışma anında kendi dosyalarını inceleme yeteneği verir.
            //GetExecutingAssembly() = Şu an yürütülmekte (çalışmakta) olan paketi getirir
            //  Bu satırı hangi projenin içine yazdıysan, o projenin tamamını bir nesne olarak hafızaya alır.

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                //register= kayıt, assembly içindeki tüm sınıfları kaydeder
                //assembly değişkenine daha önce GetExecutingAssembly() ile atılan Business katmanının Autofac içine girer;
                //ProductManager, CategoryManager, CustomerManager gibi ne kadar sınıf varsa hepsini listesine ekler. Tek tek RegisterType yazma gereği oluşmaz
                //AsImplementedInterfaces = sınıfları interface e tanıtır.
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                //enable = olanak, interceptor = önleyici, Normal şartlarda, sen IProductService üzerinden bir metot çağırdığında
                //sistem direkt ProductManager’a gider. Arada hiçbir durak yoktur.bu sayede onbefore gibi metotlar işleve girer
                //new ProxyGenerationOptions() = Bir Proxy (Vekil) oluştururken, bu vekilin nasıl davranması gerektiği belirtir
                //Eğer bu parantez boş kalsaydı, her metot için standart bir yol izlenirdi. Ancak içine bir Selector (Seçici) koyarak bir zeka katılır.

                {
                    Selector = new AspectInterceptorSelector()
                    //AspectInterceptorSelector : Sınıfa Bakar, Sınıfın üzerinde bir Attribute var mı? ([LogAspect]) ;
                    //Metoda Bakar , Metodun üzerinde bir etiket var mı? ([ValidationAspect])
                    //Hepsini Toplar: Bulunan tüm (Interceptor'ları) bir liste haline getirir ve o metot çağrıldığında sırasıyla çalıştırılır.
                }).SingleInstance();
                  //bellek tasarrufu sağlar bir kere product manager oluşur ve hepsi için aynı alan kullanılır
        }         // VE bunları tek bir nesne olarak üret der
    }
}
