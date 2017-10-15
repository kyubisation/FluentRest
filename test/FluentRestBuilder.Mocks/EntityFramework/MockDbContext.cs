﻿// <copyright file="MockDbContext.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Mocks.EntityFramework
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class MockDbContext : DbContext
    {
        public MockDbContext()
            : this(ConfigureInMemoryContextOptions())
        {
        }

        public MockDbContext(DbContextOptions<MockDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entity> Entities { get; set; }

        public DbSet<MultiKeyEntity> MultiKeyEntities { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Child> Children { get; set; }

        public DbSet<OtherEntity> OtherEntities { get; set; }

        public static DbContextOptions<MockDbContext> ConfigureInMemoryContextOptions(string name = null)
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<MockDbContext>();
            return builder.UseInMemoryDatabase(name ?? Guid.NewGuid().ToString())
                   .UseInternalServiceProvider(serviceProvider)
                   .Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MultiKeyEntity>()
                .HasKey(e => new { e.FirstId, e.SecondId });
        }
    }
}
