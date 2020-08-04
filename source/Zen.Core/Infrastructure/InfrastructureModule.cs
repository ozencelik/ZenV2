﻿using Autofac;
using Module = Autofac.Module;
using Zen.Core.Services.Catalog;
using Zen.Core.Services.Cart;
using Zen.Core.Services.Shipment;

namespace Zen.Core.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private bool _isDevelopment = false;

        public InfrastructureModule(bool isDevelopment)
        {
            _isDevelopment = isDevelopment;
        }

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>().As<IProductService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CampaignService>().As<ICampaignService>()
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

    }
}
