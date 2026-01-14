using MauiApp1.ViewModels;

namespace MauiApp1.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<Type, Type> _viewModelPageMapping = new()
    {
        { typeof(MainPageViewModel), typeof(MainPage) },
        { typeof(DemoPageViewModel), typeof(DemoPage) },
        { typeof(DashboardBarViewModel), typeof(DashboardBar) }
    };

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task NavigateToAsync<TViewModel>() where TViewModel : class
    {
        var viewModelType = typeof(TViewModel);
        
        if (!_viewModelPageMapping.TryGetValue(viewModelType, out var pageType))
        {
            throw new InvalidOperationException($"No page found for ViewModel type {viewModelType.Name}");
        }

        // Resolve the page from DI
        var page = _serviceProvider.GetRequiredService(pageType) as Page;
        
        if (page == null)
        {
            throw new InvalidOperationException($"Could not resolve page of type {pageType.Name}");
        }

        // Get the correct navigation (handle FlyoutPage)
        var navigation = GetNavigation();
        
        if (navigation != null)
        {
            await navigation.PushAsync(page);
        }
    }

    public async Task GoBackAsync()
    {
        var navigation = GetNavigation();
        
        if (navigation != null && navigation.NavigationStack.Count > 0)
        {
            await navigation.PopAsync();
        }
    }

    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    /// <summary>
    /// Gets the correct INavigation instance, handling FlyoutPage
    /// </summary>
    private INavigation? GetNavigation()
    {
        var mainPage = Application.Current?.MainPage;

        // If MainPage is a FlyoutPage, get the Detail page's navigation
        if (mainPage is FlyoutPage flyoutPage)
        {
            return flyoutPage.Detail?.Navigation;
        }

        // Otherwise, use MainPage's navigation directly
        return mainPage?.Navigation;
    }
}
