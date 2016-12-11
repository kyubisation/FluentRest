﻿// <copyright file="EntityDeletionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes.Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EntityDeletionPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly DbContext context;

        public EntityDeletionPipe(
            DbContext context,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.context = context;
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            this.context.Set<TInput>().Remove(entity);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return null;
        }
    }
}