﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Threading.Tasks;
    using FluentRestBuilder;
    using FluentRestBuilder.Results.Ok;
    using Microsoft.AspNetCore.Mvc;

    public static partial class Integration
    {
        public static Task<IActionResult> ToOkResult<TInput>(
            this IOutputPipe<TInput> pipe)
            where TInput : class
        {
            IPipe resultPipe = new OkResultPipe<TInput>(pipe);
            return resultPipe.Execute();
        }
    }
}
