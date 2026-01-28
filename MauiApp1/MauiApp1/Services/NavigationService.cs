using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Messages;
using MauiApp1.ViewModels;

namespace MauiApp1.Services;

public class NavigationService : INavigationService
{
    // Map ViewModel types to route names
    private readonly Dictionary<Type, string> _viewModelRouteMapping = new()
    {
        { typeof(MainPageViewModel), "//main" },
        { typeof(UserManagePageViewModel), nameof(UserManagePage) },
        { typeof(ProductManagePageViewModel), nameof(ProductManagePage) },
        { typeof(ProfilePageViewModel), "//profile" },
        { typeof(ProfileImagePageViewModel), nameof(ProfileImagePage) },
        { typeof(AddUserPageViewModel), nameof(AddUserPage) },
        { typeof(DemoPageViewModel), nameof(DemoPage) },
        { typeof(LoginPageViewModel), nameof(LoginPage) },
        { typeof(AddProductPageViewModel), nameof(AddProductPage) },
    };

    public NavigationService()
    {
    }

    public async Task NavigateToAsync<TViewModel>() where TViewModel : class
    {
        await NavigateToAsync<TViewModel>(null);
    }

    public async Task NavigateToAsync<TViewModel>(IDictionary<string, object>? parameters) where TViewModel : class
    {
        if (!_viewModelRouteMapping.TryGetValue(typeof(TViewModel), out var route))
        {
            throw new InvalidOperationException($"No route found for ViewModel type {typeof(TViewModel).Name}");
        }

        if (parameters != null)
        {
            await Shell.Current.GoToAsync(route, parameters);
        }
        else
        {
            await Shell.Current.GoToAsync(route);
        }
    }

    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public async Task NavigateToAsync(string route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public async Task NavigateToAsync(string route, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task NavigateToAsyncAndClearStack<TViewModel>(IDictionary<string, object>? parameters = null) where TViewModel : class
    {
        // For Shell, navigating to main app is handled by App.NavigateToMainApp()
        // This method is kept for compatibility but delegates to App
        if (typeof(TViewModel) == typeof(MainPageViewModel))
        {
            if (Application.Current is App app)
            {
                app.NavigateToMainApp();
            }

            // if there are parameters, navigate to MainPage with parameters
            // check if parameters contain a key named "toast_message"
            if (parameters != null && parameters.ContainsKey("toast_message"))
            {

                // get message from parameters
                string message = parameters["toast_message"].ToString();

                await Task.Delay(500);

                // send toast message 
                WeakReferenceMessenger.Default.Send(new ShowToastMessage(message));
            }

            return;
        }

        if (!_viewModelRouteMapping.TryGetValue(typeof(TViewModel), out var route))
        {
            throw new InvalidOperationException($"No route found for ViewModel type {typeof(TViewModel).Name}");
        }

        // Use absolute route with // to go to root and clear stack
        var absoluteRoute = route.StartsWith("//") ? route : $"//{route}";

        // assign parameters if any
        if (parameters != null)
        {
            await Shell.Current.GoToAsync(absoluteRoute, parameters);
        }
        else
        {
            await Shell.Current.GoToAsync(absoluteRoute);
        }
    }
}
