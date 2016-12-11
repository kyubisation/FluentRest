﻿// <copyright file="IQueryArgumentKeys.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.RestCollectionMutators
{
    public interface IQueryArgumentKeys
    {
        string Page { get; }

        string EntriesPerPage { get; }

        string Filter { get; }

        string OrderBy { get; }

        string Search { get; }
    }
}
