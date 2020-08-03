﻿using Autofac;
using Module = Autofac.Module;
using MediatR;
using MediatR.Pipeline;
using System.Collections.Generic;
using System.Reflection;
using Zen.Core.Services.Catalog;

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
