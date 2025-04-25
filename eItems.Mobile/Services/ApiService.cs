using System;
using System.Net.Http.Json;
using eItems.Mobile.Constants;
using eItems.Mobile.Models;
namespace eItems.Mobile.Services;
public class ApiService: IApiService
{
    private readonly HttpClient _httpClient;
    private readonly TimeSpan _timeout = TimeSpan.FromSeconds(30); // Configure timeout

    public ApiService()
    {
                var handler = new HttpClientHandler
        {
            // Allow self-signed certificates for development
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://10.0.2.2:7585"),
            Timeout = _timeout
        };
    }
public async Task<List<Item>> GetItemsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(_timeout);

            Console.WriteLine($"Attempting to connect to: {_httpClient.BaseAddress}/assets");
            
            var response = await _httpClient.GetAsync("assets", cts.Token);
            response.EnsureSuccessStatusCode();
            
            var items = await response.Content.ReadFromJsonAsync<List<Item>>(cancellationToken: cts.Token);
            return items ?? new List<Item>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
            throw new Exception($"Connection failed: {ex.Message}", ex);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Request timed out");
            throw new Exception("Request timed out");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }
   
}