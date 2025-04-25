using eItems.Mobile.Models;
using eItems.Mobile.Services;

namespace eItems.Mobile;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;
    
    public MainPage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
        LoadItemsAsync();
    }

    private async Task LoadItemsAsync()
    {
        try
        {
            LoadingIndicator.IsRunning = true;
            List<Item> items = await  _apiService.GetItemsAsync();
            ItemsCollection.ItemsSource = items;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to load items: " + ex.Message, "OK");
             LoadingIndicator.IsRunning = false;
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
        }
    }
}

