using Autofac;
using Module = Autofac.Module;
using Zen.Core.Services.Catalog;
using Zen.Core.Services.Cart;
using Zen.Core.Services.Shipment;
using Zen.Data;
using Zen.Data.Entities;

namespace Zen.Core.Infrastructure
{
    public class InfrastructureModule : Module
    {
        #region Fields
        private bool _isDevelopment = false;
        #endregion

        #region Ctor
        public InfrastructureModule(bool isDevelopment)
        {
            _isDevelopment = isDevelopment;
        }
        #endregion

        #region Methods
        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfCoreRepository<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CampaignService>().As<ICampaignService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CouponService>().As<ICouponService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DeliveryService>().As<IDeliveryService>()
                .InstancePerLifetimeScope();
        }

        private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add development only services
        }

        private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
        {
            // TODO: Add production only services
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_isDevelopment)
            {
                RegisterDevelopmentOnlyDependencies(builder);
            }
            else
            {
                RegisterProductionOnlyDependencies(builder);
            }
            RegisterCommonDependencies(builder);
        }
        #endregion
    }
}
