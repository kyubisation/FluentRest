﻿// <copyright file="NotFoundWhenNullAlias.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Operators.Exceptions;

    public static class NotFoundWhenNullAlias
    {
        /// <summary>
        /// If the received value is null, <see cref="ValidationException"/>
        /// is emitted as an error with the status code 404 (Not Found).
        /// Otherwise the given value is emitted.
        /// </summary>
        /// <typeparam name="TSource">The type of the value.</typeparam>
        /// <param name="observable">The parent observable.</param>
        /// <param name="errorFactory">The error factory method.</param>
        /// <returns>An instance of <see cref="IProviderObservable{TSource}"/>.</returns>
        public static IProviderObservable<TSource> NotFoundWhenNull<TSource>(
            this IProviderObservable<TSource> observable,
            Func<TSource, object> errorFactory = null) =>
            observable.InvalidWhen(s => s == null, StatusCodes.Status404NotFound, errorFactory);
    }
}
