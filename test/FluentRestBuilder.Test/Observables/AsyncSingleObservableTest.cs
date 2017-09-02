﻿// <copyright file="AsyncSingleObservableTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Observables
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder.Observables;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AsyncSingleObservableTest
    {
        [Fact]
        public void TestProvider()
        {
            var collection = new ServiceCollection();
            collection.AddTransient<SingleObservableTest>();
            var single = new AsyncSingleObservable<string>(
                () => string.Empty, collection.BuildServiceProvider());
            var instance = single.ServiceProvider.GetService<SingleObservableTest>();
            Assert.NotNull(instance);
        }

        [Fact]
        public async Task TestFunc()
        {
            const string expected = "expected";
            var single = new AsyncSingleObservable<string>(
                () => expected, new ServiceCollection().BuildServiceProvider());
            Assert.Equal(expected, await single);
        }

        [Fact]
        public async Task TestTask()
        {
            const string expected = "expected";
            var single = new AsyncSingleObservable<string>(
                () => new Task<string>(() => expected),
                new ServiceCollection().BuildServiceProvider());
            Assert.Equal(expected, await single);
        }

        [Fact]
        public async Task TestLazy()
        {
            const string expected = "expected";
            var lazy = new Lazy<string>(() => expected);
            var single = new AsyncSingleObservable<string>(
                lazy, new ServiceCollection().BuildServiceProvider());
            Assert.False(lazy.IsValueCreated);
            Assert.Equal(expected, await single);
            Assert.True(lazy.IsValueCreated);
        }

        [Fact]
        public async Task TestException()
        {
            var single = new AsyncSingleObservable<string>(
                (Func<string>)(() => throw new InvalidOperationException()),
                new ServiceCollection().BuildServiceProvider());
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await single);
        }
    }
}
