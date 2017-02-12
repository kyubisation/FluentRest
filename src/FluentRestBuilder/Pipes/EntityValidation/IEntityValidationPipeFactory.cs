﻿// <copyright file="IEntityValidationPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.EntityValidation
{
    using System;
    using System.Threading.Tasks;

    public interface IEntityValidationPipeFactory<TInput>
        where TInput : class
    {
        OutputPipe<TInput> Create(
            Func<TInput, Task<bool>> invalidCheck,
            int statusCode,
            Func<TInput, object> errorFactory,
            IOutputPipe<TInput> parent);
    }
}
