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
public async Task<List<Item>> GetItemsAsync(int pageIndex = 0, int pageSize = 10,CancellationToken cancellationToken = default)
    {
        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(_timeout);

            var url = $"assets?pageIndex={pageIndex}&pageSize={pageSize}";
            Console.WriteLine($"Attempting to connect to: {_httpClient.BaseAddress}/{url}");
            
            var response = await _httpClient.GetAsync(url, cts.Token);
            response.EnsureSuccessStatusCode();
            
            var paginatedResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<Item>>(cancellationToken: cts.Token);
            return paginatedResponse?.Data ?? new List<Item>();
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