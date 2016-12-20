﻿// <copyright file="MockActionResult.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Common.Mocks
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class MockActionResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}