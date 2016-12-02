﻿namespace KyubiCode.FluentRest.Test.SourcePipes.EntityCollection
{
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentRest.SourcePipes.EntityCollection;
    using Mocks;
    using Xunit;

    public class EntityCollectionSourceTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestBareUseCase()
        {
            this.CreateEntities();
            var source = this.CreateSource();
            var resultPipe = new MockResultPipe<IQueryable<Entity>>(source);
            await resultPipe.Execute();
            Assert.Equal(Entity.Entities.Count, resultPipe.Input.Count());
        }

        private EntityCollectionSource<Entity> CreateSource()
        {
            return new EntityCollectionSource<Entity>(
                this.ResolveScoped<IQueryableFactory<Entity>>().Queryable,
                this.ServiceProvider);
        }
    }
}
