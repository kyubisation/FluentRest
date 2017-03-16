﻿// <copyright file="ActionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.Actions
{
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class ActionPipeTest
    {
        private const string NewName = "ActionPipeTest";
        private readonly Entity entity = new Entity { Id = 1, Name = "test" };
        private readonly Source<Entity> source;

        public ActionPipeTest()
        {
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterActionPipe()
                .Services
                .BuildServiceProvider();
            this.source = new Source<Entity>(this.entity, provider);
        }

        [Fact]
        public async Task TestAsyncAction()
        {
            var result = await this.source
                .Do(async e => await Task.Run(() => e.Name = NewName))
                .ToObjectResultOrDefault();
            Assert.Equal(NewName, result.Name);
        }

        [Fact]
        public async Task TestSyncAction()
        {
            var result = await this.source
                .Do(e => e.Name = NewName)
                .ToObjectResultOrDefault();
            Assert.Equal(NewName, result.Name);
        }
    }
}