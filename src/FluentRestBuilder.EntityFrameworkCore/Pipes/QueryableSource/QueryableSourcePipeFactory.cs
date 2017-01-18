// <copyright file="QueryableSourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class QueryableSourcePipeFactory<TInput, TOutput> :
        IQueryableSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public QueryableSourcePipeFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public QueryableSourcePipe<TInput, TOutput> Resolve(
            Func<DbContext, TInput, IQueryable<TOutput>> selection, IOutputPipe<TInput> pipe) =>
            new QueryableSourcePipe<TInput, TOutput>(selection, this.contextStorage, pipe);
    }
}