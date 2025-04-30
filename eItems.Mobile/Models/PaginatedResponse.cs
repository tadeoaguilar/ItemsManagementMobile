using System;

namespace eItems.Mobile.Models;


public class PaginatedResponse<T> where T : class
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public List<T> Data { get; set; } = new();
}