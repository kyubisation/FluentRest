﻿// <copyright file="LinkExtensionTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Links
{
    using System.Collections.Generic;
    using System.Linq;
    using HypertextApplicationLanguage.Links;
    using Xunit;

    public class LinkExtensionTest
    {
        [Fact]
        public void TestEmptyAggregation()
        {
            var result = Enumerable.Empty<NamedLink>().BuildLinks();
            Assert.Empty(result);
        }

        [Fact]
        public void TestNull()
        {
            IEnumerable<NamedLink> links = null;
            //// ReSharper disable once ExpressionIsAlwaysNull
            var result = links.BuildLinks();
            Assert.Null(result);
        }

        [Fact]
        public void TestSingleLinkAggregation()
        {
            var self = new LinkToSelf(new Link("/"));
            var result = new[] { self }.BuildLinks();
            Assert.Equal(1, result.Count);
            Assert.True(result.ContainsKey(self.Name));
            var link = result[self.Name];
            Assert.IsType<Link>(link);
            Assert.Equal(self.Link.Href, ((Link)link).Href);
        }

        [Fact]
        public void TestMultipleLinksAggregation()
        {
            const string name = "name";
            var namedLinks = new[]
            {
                new NamedLink(name, new Link("/")),
                new NamedLink(name, new Link("/asdf")),
            };
            var result = namedLinks.BuildLinks();
            Assert.Equal(1, result.Count);
            Assert.True(result.ContainsKey(name));
            var links = result[name];
            Assert.IsType<List<Link>>(links);
            Assert.Equal(namedLinks.Length, ((List<Link>)links).Count);
        }

        [Fact]
        public void TestMultipleDifferentLinksAggregation()
        {
            const string name = "name";
            var namedLinks = new[]
            {
                new LinkToSelf(new Link("/")),
                new NamedLink(name, new Link("/qwer")),
                new NamedLink(name, new Link("/asdf")),
            };
            var result = namedLinks.BuildLinks();
            Assert.Equal(2, result.Count);
            Assert.True(result.ContainsKey(name));
            var links = result[name];
            Assert.IsType<List<Link>>(links);
            Assert.Equal(2, ((List<Link>)links).Count);
        }
    }
}
