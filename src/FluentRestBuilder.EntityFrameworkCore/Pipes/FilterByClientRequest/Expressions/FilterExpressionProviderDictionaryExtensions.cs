﻿// <copyright file="FilterExpressionProviderDictionaryExtensions.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Pipes.FilterByClientRequest.Expressions;

    public static class FilterExpressionProviderDictionaryExtensions
    {
        public static FilterExpressionProviderDictionary<TEntity> AddEqualAndContainsStringFilter<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            Expression<Func<TEntity, string>> propertySelector)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddEqualAndContainsStringFilter(member.Member.Name);
        }

        public static FilterExpressionProviderDictionary<TEntity> AddEqualAndContainsStringFilter<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary, string property) =>
            dictionary.AddFilter(
                property,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<string>(e, property) == f)
                    .AddContains(e => EF.Property<string>(e, property).Contains(f)));

        public static FilterExpressionProviderDictionary<TEntity> AddIntegerFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            Expression<Func<TEntity, int>> propertySelector,
            Func<string, int> conversion = null)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddIntegerFilters(member.Member.Name, conversion);
        }

        public static FilterExpressionProviderDictionary<TEntity> AddIntegerFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            string property,
            Func<string, int> conversion = null) =>
            dictionary.AddTypedFilter(
                property,
                conversion ?? int.Parse,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<int>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<int>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<int>(e, property) >= f)
                    .AddLessThan(e => EF.Property<int>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<int>(e, property) <= f));

        public static FilterExpressionProviderDictionary<TEntity> AddDoubleFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            Expression<Func<TEntity, double>> propertySelector,
            Func<string, double> conversion = null)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddDoubleFilters(member.Member.Name, conversion);
        }

        public static FilterExpressionProviderDictionary<TEntity> AddDoubleFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            string property,
            Func<string, double> conversion = null) =>
            dictionary.AddTypedFilter(
                property,
                conversion ?? double.Parse,
                (f, expressions) => expressions
                    //// ReSharper disable once CompareOfFloatsByEqualityOperator
                    .AddEquals(e => EF.Property<double>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<double>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<double>(e, property) >= f)
                    .AddLessThan(e => EF.Property<double>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<double>(e, property) <= f));

        public static FilterExpressionProviderDictionary<TEntity> AddDateTimeFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            Expression<Func<TEntity, DateTime>> propertySelector,
            Func<string, DateTime> conversion)
        {
            var member = (MemberExpression)propertySelector.Body;
            return dictionary.AddDateTimeFilters(member.Member.Name, conversion);
        }

        public static FilterExpressionProviderDictionary<TEntity> AddDateTimeFilters<TEntity>(
            this FilterExpressionProviderDictionary<TEntity> dictionary,
            string property,
            Func<string, DateTime> conversion) =>
            dictionary.AddTypedFilter(
                property,
                conversion,
                (f, expressions) => expressions
                    .AddEquals(e => EF.Property<DateTime>(e, property) == f)
                    .AddGreaterThan(e => EF.Property<DateTime>(e, property) > f)
                    .AddGreaterThanOrEqual(e => EF.Property<DateTime>(e, property) >= f)
                    .AddLessThan(e => EF.Property<DateTime>(e, property) < f)
                    .AddLessThanOrEqual(e => EF.Property<DateTime>(e, property) <= f));
    }
}
