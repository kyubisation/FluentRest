﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Security.Claims;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.ClaimValidation;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterClaimValidationPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>),
                typeof(ClaimValidationPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, TInput, bool> predicate,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.GetRequiredService<IClaimValidationPipeFactory<TInput>>()
                .Create(predicate, errorFactory, pipe);

        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, TInput, bool> predicate,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas(predicate, i => error);

        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, bool> predicate,
            Func<TInput, object> errorFactory = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => predicate(p), errorFactory);

        public static OutputPipe<TInput> CurrentUserHas<TInput>(
            this IOutputPipe<TInput> pipe,
            Func<ClaimsPrincipal, bool> predicate,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => predicate(p), error);

        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            string claim,
            Func<TInput, object> error = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim), error);

        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe, string claimType, string claim, object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim), error);

        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            Func<TInput, string> claim,
            Func<TInput, object> error = null)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim(e)), error);

        public static OutputPipe<TInput> CurrentUserHasClaim<TInput>(
            this IOutputPipe<TInput> pipe,
            string claimType,
            Func<TInput, string> claim,
            object error)
            where TInput : class =>
            pipe.CurrentUserHas((p, e) => p.HasClaim(claimType, claim(e)), error);
    }
}
