﻿// <copyright file="IOrderByExpression.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Operators.ClientRequest.OrderByExpressions
{
    using System.Linq;

    public interface IOrderByExpression<TEntity>
    {
        IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable);

        IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> queryable);
    }
}
