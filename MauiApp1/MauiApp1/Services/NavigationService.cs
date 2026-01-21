using MauiApp1.ViewModels;

namespace MauiApp1.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<Type, Type> _viewModelPageMapping = new()
    {
        { typeof(MainPageViewModel), typeof(MainPage) },
        { typeof(DemoPageViewModel), typeof(DemoPage) },
        { typeof(DashboardBarViewModel), typeof(DashboardBar) },
        { typeof(AddUserPageViewModel), typeof(AddUserPage) },
        { typeof(LoginPageViewModel), typeof(LoginPage) },
        { typeof(UserManagePageViewModel), typeof(UserManagePage) },
        { typeof(ProductManagePageViewModel), typeof(ProductManagePage) },
        { typeof(ProfilePageViewModel), typeof(ProfilePage) },
        { typeof(ProfileImagePageViewModel), typeof(ProfileImagePage) },
    };

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task NavigateToAsync<TViewModel>() where TViewModel : class
    {
        await NavigateToAsync<TViewModel>(null);
    }

    public async Task NavigateToAsync<TViewModel>(IDictionary<string, object> parameters) where TViewModel : class
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

        // Pass parameters if ViewModel implements IQueryAttributable
        if (parameters != null && page.BindingContext is IQueryAttributable queryAttributable)
        {
            queryAttributable.ApplyQueryAttributes(parameters);
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

    public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task NavigateToAsyncAndClearStack<TViewModel>() where TViewModel : class
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

        // Replace the navigation stack by setting a new NavigationPage as Detail
        var mainPage = Application.Current?.MainPage;
        
        if (mainPage is FlyoutPage flyoutPage)
        {
            // Create a new NavigationPage with the target page as root
            var newNavigationPage = new NavigationPage(page)
            {
                BarBackgroundColor = Colors.LightBlue,
                BarTextColor = Colors.White
            };
            
            flyoutPage.Detail = newNavigationPage;
            
            // Close flyout if open
            flyoutPage.IsPresented = false;
        }
        else
        {
            // Fallback: just navigate normally
            await NavigateToAsync<TViewModel>();
        }

        await Task.CompletedTask;
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
