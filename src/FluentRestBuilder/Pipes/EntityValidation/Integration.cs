﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using FluentRestBuilder;
    using FluentRestBuilder.Pipes.EntityValidation;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        public static EntityValidationPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            pipe.GetService<IEntityValidationPipeFactory<TEntity>>()
                .Resolve(invalidCheck, statusCode, error, pipe);

        public static EntityValidationPipe<TEntity> InvalidWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            int statusCode,
            object error = null)
            where TEntity : class =>
            pipe.GetService<IEntityValidationPipeFactory<TEntity>>()
                .Resolve(invalidCheck, statusCode, error, pipe);

        public static EntityValidationPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static EntityValidationPipe<TEntity> ForbiddenWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status403Forbidden, error);

        public static EntityValidationPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static EntityValidationPipe<TEntity> BadRequestWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status400BadRequest, error);

        public static EntityValidationPipe<TEntity> NotFoundWhenEmpty<TEntity>(
            this IOutputPipe<TEntity> pipe, object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, e => e == null, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> NotFoundWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status404NotFound, error);

        public static EntityValidationPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, bool> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);

        public static EntityValidationPipe<TEntity> GoneWhen<TEntity>(
            this IOutputPipe<TEntity> pipe,
            Func<TEntity, Task<bool>> invalidCheck,
            object error = null)
            where TEntity : class =>
            InvalidWhen(pipe, invalidCheck, StatusCodes.Status410Gone, error);
    }
}