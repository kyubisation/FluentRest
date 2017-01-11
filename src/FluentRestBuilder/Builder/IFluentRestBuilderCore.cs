﻿// <copyright file="IFluentRestBuilderCore.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IFluentRestBuilderCore
    {
        IServiceCollection Services { get; }
    }
}