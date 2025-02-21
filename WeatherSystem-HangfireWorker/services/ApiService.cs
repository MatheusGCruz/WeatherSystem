using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> FetchDataAsync(string city)
    {
        var response = await _httpClient.GetStringAsync("https://wttr.in/"+city+"?format=j1&lang=en");
        return response;
    }

}
