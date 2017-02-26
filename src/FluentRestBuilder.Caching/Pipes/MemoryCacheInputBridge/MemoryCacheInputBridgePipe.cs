﻿// <copyright file="MemoryCacheInputBridgePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.MemoryCacheInputBridge
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MemoryCacheInputBridgePipe<TInput> : ChainPipe<TInput>
    {
        private readonly object key;
        private readonly IMemoryCache memoryCache;

        public MemoryCacheInputBridgePipe(
            object key,
            IMemoryCache memoryCache,
            ILogger<MemoryCacheInputBridgePipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.key = key;
            this.memoryCache = memoryCache;
        }

        protected override Task<IActionResult> Execute()
        {
            object cacheEntry;
            if (this.memoryCache.TryGetValue(this.key, out cacheEntry) && cacheEntry is TInput)
            {
                return this.ExecuteChild((TInput)cacheEntry);
            }

            return base.Execute();
        }
    }
}
