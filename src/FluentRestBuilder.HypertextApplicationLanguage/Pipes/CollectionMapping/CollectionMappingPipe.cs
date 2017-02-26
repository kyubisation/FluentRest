﻿// <copyright file="CollectionMappingPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Pipes.CollectionMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using HypertextApplicationLanguage;
    using Links;
    using Microsoft.Extensions.Logging;

    public class CollectionMappingPipe<TInput, TOutput>
        : MappingPipeBase<IQueryable<TInput>, RestEntityCollection>
    {
        private readonly IRestCollectionLinkGenerator linkGenerator;
        private readonly ILinkAggregator linkAggregator;
        private readonly IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage;
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly Func<TInput, TOutput> mapping;
        private RestEntityCollection restEntityCollection;

        public CollectionMappingPipe(
            Func<TInput, TOutput> mapping,
            IRestCollectionLinkGenerator linkGenerator,
            ILinkAggregator linkAggregator,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<CollectionMappingPipe<TInput, TOutput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.mapping = mapping;
            this.linkGenerator = linkGenerator;
            this.linkAggregator = linkAggregator;
            this.paginationMetaInfoStorage = paginationMetaInfoStorage;
            this.queryableTransformer = queryableTransformer;
        }

        protected override async Task<RestEntityCollection> MapAsync(IQueryable<TInput> input)
        {
            var entities = await this.queryableTransformer.ToList(input);

            this.restEntityCollection = new RestEntityCollection();
            this.GenerateEmbeddedEntities(entities);
            this.GenerateLinks();

            return this.restEntityCollection;
        }

        private void GenerateEmbeddedEntities(IEnumerable<TInput> entities)
        {
            var mappedEntities = entities
                .Select(e => this.mapping(e))
                .ToList();
            this.restEntityCollection.Embedded.Add("items", mappedEntities);
        }

        private void GenerateLinks()
        {
            var links = this.linkGenerator.GenerateLinks(this.paginationMetaInfoStorage.Value);
            this.restEntityCollection.Links = this.linkAggregator.BuildLinks(links);
        }
    }
}
