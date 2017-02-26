// <copyright file="QueryableSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class QueryableSourcePipe<TInput, TOutput> : MappingPipeBase<TInput, IQueryable<TOutput>>
        where TOutput : class
    {
        private readonly Func<DbContext, TInput, IQueryable<TOutput>> queryablePipe;
        private readonly IScopedStorage<DbContext> contextStorage;

        public QueryableSourcePipe(
            Func<DbContext, TInput, IQueryable<TOutput>> queryablePipe,
            IScopedStorage<DbContext> contextStorage,
            ILogger<QueryableSourcePipe<TInput, TOutput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.queryablePipe = queryablePipe;
            this.contextStorage = contextStorage;
        }

        protected override IQueryable<TOutput> Map(TInput input) =>
            this.queryablePipe(this.contextStorage.Value, input);
    }
}
