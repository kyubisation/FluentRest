﻿// <copyright file="EntityValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;

    public class EntityValidationPipeFactory<TInput> : IEntityValidationPipeFactory<TInput>
        where TInput : class
    {
        public EntityValidationPipe<TInput> Resolve(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent) =>
            new EntityValidationPipe<TInput>(invalidCheck, statusCode, error, parent);

        public EntityValidationPipe<TInput> Resolve(
            Func<TInput, bool> invalidCheck,
            int statusCode,
            object error,
            IOutputPipe<TInput> parent) =>
            new EntityValidationPipe<TInput>(invalidCheck, statusCode, error, parent);
    }
}