namespace MauiApp1.Services;

/// <summary>
/// Service for handling navigation between pages from ViewModels
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigate to a page associated with the specified ViewModel type
    /// </summary>
    Task NavigateToAsync<TViewModel>() where TViewModel : class;

    /// <summary>
    /// Navigate to a page associated with the specified ViewModel type with parameters
    /// </summary>
    Task NavigateToAsync<TViewModel>(IDictionary<string, object> parameters) where TViewModel : class;

    /// <summary>
    /// Navigate back to the previous page
    /// </summary>
    Task GoBackAsync();

    /// <summary>
    /// Navigate to a specific route (for Shell-based navigation)
    /// </summary>
    Task NavigateToAsync(string route);

    /// <summary>
    /// Navigate to a specific route with parameters (for Shell-based navigation)
    /// </summary>
    Task NavigateToAsync(string route, IDictionary<string, object> parameters);

    /// <summary>
    /// Navigate to a page and clear the navigation stack (no back button)
    /// </summary>
    Task NavigateToAsyncAndClearStack<TViewModel>(IDictionary<string, object>? parameters = null) where TViewModel : class;
}
