using System;
using System.Net.Http.Json;
using eItems.Mobile.Constants;
using eItems.Mobile.Models;
namespace eItems.Mobile.Services;

public interface IApiService
{
    Task<List<Item>> GetItemsAsync(int pageIndex = 0, int pageSize = 10,CancellationToken cancellationToken = default);
}
