﻿// <copyright file="FirstOrDefaultPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FirstOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class FirstOrDefaultPipe<TInput> : MappingPipeBase<IQueryable<TInput>, TInput>
    {
        private readonly Expression<Func<TInput, bool>> predicate;
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public FirstOrDefaultPipe(
            Expression<Func<TInput, bool>> predicate,
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<FirstOrDefaultPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.predicate = predicate;
            this.queryableTransformer = queryableTransformer;
        }

        protected override Task<TInput> MapAsync(IQueryable<TInput> input)
        {
            var queryable = input.Where(this.predicate);
            return this.queryableTransformer.FirstOrDefault(queryable);
        }
    }
}
