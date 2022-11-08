using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostService
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage respone = await client.PostAsJsonAsync("/posts", dto);
        if (!respone.IsSuccessStatusCode)
        {
            string content = await respone.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}