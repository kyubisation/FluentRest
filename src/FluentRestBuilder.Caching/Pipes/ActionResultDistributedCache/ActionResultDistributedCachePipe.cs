﻿// <copyright file="ActionResultDistributedCachePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.Pipes.ActionResultDistributedCache
{
    using System.Threading.Tasks;
    using DistributedCache;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;

    public class ActionResultDistributedCachePipe<TInput> : ChainPipe<TInput>
        where TInput : class
    {
        private readonly string key;
        private readonly IDistributedCache distributedCache;
        private readonly IByteMapper<TInput> byteMapper;
        private readonly DistributedCacheEntryOptions options;

        public ActionResultDistributedCachePipe(
            string key,
            DistributedCacheEntryOptions options,
            IByteMapper<TInput> byteMapper,
            IDistributedCache distributedCache,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.key = key;
            this.options = options;
            this.distributedCache = distributedCache;
            this.byteMapper = byteMapper;
        }

        protected override async Task<IActionResult> Execute(TInput input)
        {
            if (input != null)
            {
                await this.SaveToCache(input);
            }

            return await base.Execute(input);
        }

        protected override async Task<IActionResult> Execute()
        {
            // TODO: Fix action result
            var cacheEntry = await this.RetrieveFromCache();
            if (cacheEntry != null)
            {
                return await this.ExecuteChild(cacheEntry);
            }

            return await base.Execute();
        }

        private async Task SaveToCache(TInput input)
        {
            var cacheBytes = this.byteMapper.ToByteArray(input);
            await this.distributedCache.SetAsync(this.key, cacheBytes, this.options);
        }

        private async Task<TInput> RetrieveFromCache()
        {
            var bytes = await this.distributedCache.GetAsync(this.key);
            if (bytes != null && bytes.Length > 0)
            {
                return this.byteMapper.FromByteArray(bytes);
            }

            return null;
        }
    }
}