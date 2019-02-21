﻿using System;
using System.Threading.Tasks;

namespace GraphQL.IQueryable.Abstractions
{
    public interface IGraphQLRequest<T>
    {
        IGraphQLRequest<T> WithHeader(string header, string value);
        IGraphQLRequest<T> WithUri(Uri uri);
        IGraphQLRequest<T> WithUri(string uri);

        Task<T> Response();
    }
}
