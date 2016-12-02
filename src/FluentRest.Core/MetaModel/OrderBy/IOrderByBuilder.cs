﻿namespace KyubiCode.FluentRest.MetaModel.OrderBy
{
    using System;
    using System.Linq.Expressions;
    using RestCollectionMutators.OrderBy;

    public interface IOrderByBuilder<TEntity>
    {
        OrderByDirection ResolveDirection(string orderBy);

        Expression<Func<TEntity, object>> CreateOrderBy();
    }
}
