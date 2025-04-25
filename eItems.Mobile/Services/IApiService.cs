using System;
using System.Net.Http.Json;
using eItems.Mobile.Constants;
using eItems.Mobile.Models;
namespace eItems.Mobile.Services;

public interface IApiService
{
    Task<List<Item>> GetItemsAsync(CancellationToken cancellationToken = default);
}
