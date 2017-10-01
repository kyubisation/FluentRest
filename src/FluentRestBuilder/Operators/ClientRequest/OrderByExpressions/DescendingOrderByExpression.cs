﻿// <copyright file="DescendingOrderByExpression.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class DescendingOrderByExpression<TEntity, TKey> : IOrderByExpression<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> orderBy;

        public DescendingOrderByExpression(Expression<Func<TEntity, TKey>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable) =>
            Queryable.OrderByDescending(queryable, this.orderBy);

        public IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> queryable) =>
            queryable.ThenByDescending(this.orderBy);
    }
}