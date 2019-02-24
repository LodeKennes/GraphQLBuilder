using System;
using System.Threading.Tasks;

namespace GraphQLBuilder.Abstractions
{
    public interface IGraphQLRequest
    {
        IGraphQLRequest WithHeader(string header, string value);
        IGraphQLRequest WithUri(Uri uri);
        IGraphQLRequest WithUri(string uri);

        Task<T> GetResponseAsync<T>();
    }
}
