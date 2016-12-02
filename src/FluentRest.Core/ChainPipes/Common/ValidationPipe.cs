﻿namespace KyubiCode.FluentRest.ChainPipes.Common
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ValidationPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly int statusCode;
        private readonly object error;

        protected ValidationPipe(
            int statusCode,
            object error,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.statusCode = statusCode;
            this.error = error;
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            if (!await this.IsInvalid(entity))
            {
                return null;
            }

            return this.error == null
                ? this.CreateErrorActionResult() : this.CreateErrorObjectResult();
        }

        protected abstract Task<bool> IsInvalid(TInput entity);

        private IActionResult CreateErrorActionResult()
        {
            switch (this.statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestResult();
                case StatusCodes.Status404NotFound:
                    return new NotFoundResult();
                default:
                    return new StatusCodeResult(this.statusCode);
            }
        }

        private IActionResult CreateErrorObjectResult()
        {
            switch (this.statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return new BadRequestObjectResult(this.error);
                case StatusCodes.Status404NotFound:
                    return new NotFoundObjectResult(this.error);
                default:
                    return new ObjectResult(this.error) { StatusCode = this.statusCode };
            }
        }
    }
}
