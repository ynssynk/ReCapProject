using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.Storages;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Business
            builder.RegisterType<CarManager>().As<ICarService>();
            builder.RegisterType<BrandManager>().As<IBrandService>();
            builder.RegisterType<ColorManager>().As<IColorService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<RentalManager>().As<IRentalService>();
            builder.RegisterType<CarImageManager>().As<ICarImageService>();

            builder.RegisterType<LocalStorage>().As<IStorage>();
            //Dal
            builder.RegisterType<EfCarDal>().As<ICarDal>();
            builder.RegisterType<EfBrandDal>().As<IBrandDal>();
            builder.RegisterType<EfColorDal>().As<IColorDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<EfRentalDal>().As<IRentalDal>();
            builder.RegisterType<EfCarImageDal>().As<ICarImageDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}