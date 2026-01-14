namespace MauiApp1.Services;

/// <summary>
/// Implementation of IDialogService for displaying dialogs
/// </summary>
public class DialogService : IDialogService
{
    public async Task ShowAlertAsync(string title, string message, string cancel = "OK")
    {
        var mainPage = Application.Current?.MainPage;
        
        if (mainPage != null)
        {
            await mainPage.DisplayAlert(title, message, cancel);
        }
    }

    public async Task<bool> ShowConfirmAsync(string title, string message, string accept = "Yes", string cancel = "No")
    {
        var mainPage = Application.Current?.MainPage;
        
        if (mainPage != null)
        {
            return await mainPage.DisplayAlert(title, message, accept, cancel);
        }

        return false;
    }
}
