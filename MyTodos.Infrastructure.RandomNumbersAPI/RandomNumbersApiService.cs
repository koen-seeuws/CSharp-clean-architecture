using System.Net.Http.Json;
using System.Web;
using MyTodos.Application.Contracts.RandomNumbers;

namespace MyTodos.Infrastructure.RandomNumbersAPI;

public class RandomNumbersApiService(IHttpClientFactory httpClientFactory) : IRandomNumberGenerator
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("RandomNumbersClient");
    
    public async Task<int[]> GenerateRandomNumbers(int count, int min, int max)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["count"] = count.ToString();
        query["min"] = min.ToString();
        query["max"] = max.ToString();

        var response = await _httpClient.GetAsync($"random?{query}");
        response.EnsureSuccessStatusCode();
        var numbers = await response.Content.ReadFromJsonAsync<int[]>();
        if(numbers == null) throw new NullReferenceException("The numbers array is null");
        return numbers;
    }
}