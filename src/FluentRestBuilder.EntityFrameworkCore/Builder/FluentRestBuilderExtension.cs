﻿// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Pipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            RegisterEntityFrameworkRelatedServices<TContext>(builder.Services);

            new FluentRestBuilderCore(builder.Services)
                .RegisterDeletionPipe()
                .RegisterInsertionPipe()
                .RegisterQueryableSourcePipe()
                .RegisterReloadPipe()
                .RegisterUpdatePipe();
            return builder;
        }

        private static void RegisterEntityFrameworkRelatedServices<TContext>(
            IServiceCollection services)
            where TContext : DbContext
        {
            services.TryAddScoped<IContextActions, ContextActions<TContext>>();
            services.TryAddScoped<IQueryableFactory, ContextQueryableFactory<TContext>>();
            services.TryAddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
            services.TryAddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>));
            services.AddSingleton(
                typeof(IQueryableTransformer<>), typeof(AsyncQueryableTransformer<>));
        }
    }
}
