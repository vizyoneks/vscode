
using System.Net.Http;

namespace dotnet.Repositories
{
    public interface IHttpClientFactoryService
    {
        HttpClient CreateClient();
    }
}