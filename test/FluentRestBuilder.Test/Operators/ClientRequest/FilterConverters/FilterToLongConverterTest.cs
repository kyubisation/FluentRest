﻿// <copyright file="FilterToLongConverterTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Operators.ClientRequest.FilterConverters
{
    using System.Globalization;
    using FluentRestBuilder.Operators.ClientRequest.FilterConverters;
    using Mocks;
    using Xunit;

    public class FilterToLongConverterTest
    {
        private readonly FilterToLongConverter converter;

        public FilterToLongConverterTest()
        {
            this.converter = new FilterToLongConverter(new CultureInfoConversionPriorityCollection());
            new CultureInfo("fr-FR").AssignAsCurrentUiCulture();
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("1", 1)]
        [InlineData("-1", -1)]
        public void TestConversion(string filter, long expectedResult)
        {
            var result = this.converter.Parse(filter);
            Assert.True(result.Success);
            Assert.Equal(expectedResult, result.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("true")]
        public void TestFailure(string filter)
        {
            var result = this.converter.Parse(filter);
            Assert.False(result.Success);
        }
    }
}
