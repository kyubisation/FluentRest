﻿// <copyright file="FluentRestBuilderConfigurationIntegration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Operators.ClientRequest.FilterExpressions;
    using Operators.ClientRequest.OrderByExpressions;
    using Storage;

    public static class FluentRestBuilderConfigurationIntegration
    {
        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddFluentRestBuilder(
            this IMvcBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="builder">The MVC core builder.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddFluentRestBuilder(
            this IMvcCoreBuilder builder) =>
            builder.Services.AddFluentRestBuilder();

        /// <summary>
        /// Registers FluentRestBuilder.
        /// </summary>
        /// <param name="collection">The service collection.</param>
        /// <returns>The FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration AddFluentRestBuilder(
            this IServiceCollection collection) =>
            new FluentRestBuilderConfiguration(collection);

        /// <summary>
        /// Registers the <see cref="IHttpContextAccessor"/> to be used as the access for the
        /// <see cref="HttpContext"/>.
        /// By default the controller extension methods to create the sources use the
        /// <see cref="HttpContext"/> provided by the controller.
        /// This is only necessary if you want to use pipes that require the
        ///  <see cref="HttpContext"/> and want to create sources without a controller.
        /// </summary>
        /// <remarks>
        /// The ASP.NET Core Identity package registers <see cref="IHttpContextAccessor"/>.
        /// </remarks>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration UseHttpContextAccessor(
            this IFluentRestBuilderConfiguration builder)
        {
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IScopedStorage<HttpContext>, HttpContextStorage>();
            return builder;
        }

        /// <summary>
        /// Configures filters and order by expressions for all non-complex properties
        /// of an entity.
        /// <para>
        /// Allows <see cref="ApplyFilterByClientRequestOperator"/> and
        /// <see cref="ApplyOrderByClientRequestOperator"/> to be used without parameters.
        /// </para>
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="builder">The FluentRestBuilder configuration instance.</param>
        /// <returns>The provided FluentRestBuilder configuration instance.</returns>
        public static IFluentRestBuilderConfiguration ConfigureFiltersAndOrderByExpressionsForEntity<TEntity>(
            this IFluentRestBuilderConfiguration builder)
        {
            builder.Services.AddSingleton<IOrderByExpressionDictionary<TEntity>>(
                s => new OrderByExpressionDictionary<TEntity>().AddProperties());
            builder.Services.AddSingleton<IFilterExpressionProviderDictionary<TEntity>>(
                s => new FilterExpressionProviderDictionary<TEntity>(s).AddPropertyFilters());
            return builder;
        }
    }
}
