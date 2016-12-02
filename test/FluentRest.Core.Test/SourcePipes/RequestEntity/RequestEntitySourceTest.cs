﻿namespace KyubiCode.FluentRest.Test.SourcePipes.RequestEntity
{
    using System.Threading.Tasks;
    using FluentRest.SourcePipes.RequestEntity;
    using Mocks;
    using Xunit;

    public class RequestEntitySourceTest : TestBaseWithServiceProvider
    {
        [Fact]
        public async Task TestNoAttachedChild()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var source = new RequestEntitySource<Entity>(entity, this.ServiceProvider);
            await Assert.ThrowsAsync<NoPipeAttachedException>(async () => await ((IPipe)source).Execute());
        }

        [Fact]
        public async Task TestSimpleEntity()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var source = new RequestEntitySource<Entity>(entity, this.ServiceProvider);
            var resultPipe = new MockResultPipe<Entity>(source);

            var actionResult = await resultPipe.Execute();
            Assert.Same(entity, resultPipe.Input);
            Assert.IsType<MockActionResult>(actionResult);
        }
    }
}
