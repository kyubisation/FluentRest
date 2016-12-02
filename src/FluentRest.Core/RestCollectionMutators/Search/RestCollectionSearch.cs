﻿namespace KyubiCode.FluentRest.RestCollectionMutators.Search
{
    using System.Collections.Generic;
    using System.Linq;
    using MetaModel;
    using MetaModel.Filters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public class RestCollectionSearch<TEntity> : IRestCollectionSearch<TEntity>
    {
        private readonly IQueryCollection queryCollection;
        private readonly IExpressionFactory<TEntity> expressionFactory;
        private readonly IQueryArgumentKeys queryArgumentKeys;
        private readonly List<IFilterBuilder<TEntity>> filters;

        public RestCollectionSearch(
            IQueryCollection queryCollection,
            IExpressionFactory<TEntity> expressionFactory,
            IQueryArgumentKeys queryArgumentKeys)
        {
            this.queryCollection = queryCollection;
            this.expressionFactory = expressionFactory;
            this.queryArgumentKeys = queryArgumentKeys;
            this.filters = this.expressionFactory.FilterExpressions.Values
                .OfType<StringPropertyFilterBuilder<TEntity>>()
                .ToList<IFilterBuilder<TEntity>>();
        }

        public IQueryable<TEntity> Apply(IQueryable<TEntity> queryable)
        {
            var searchValue = this.queryCollection[this.queryArgumentKeys.Search];
            return StringValues.IsNullOrEmpty(searchValue)
                ? queryable : this.Search(queryable, searchValue);
        }

        private IQueryable<TEntity> Search(IQueryable<TEntity> queryable, StringValues search)
        {
            var expressions = search.ToArray()
                .SelectMany(s => this.filters.Select(f => f.CreateFilter(s)));
            var expression = this.expressionFactory.JoinExpressionsByOr(expressions);
            return queryable.Where(expression);
        }
    }
}