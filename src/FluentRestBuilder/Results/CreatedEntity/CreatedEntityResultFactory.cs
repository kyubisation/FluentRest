﻿// <copyright file="CreatedEntityResultFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Results.CreatedEntity
{
    using System;

    public class CreatedEntityResultFactory<TInput> : ICreatedEntityResultFactory<TInput>
        where TInput : class
    {
        public ResultBase<TInput> Create(
            Func<TInput, object> routeValuesFactory,
            string routeName,
            IOutputPipe<TInput> parent) =>
            new CreatedEntityResult<TInput>(routeValuesFactory, routeName, parent);
    }
}