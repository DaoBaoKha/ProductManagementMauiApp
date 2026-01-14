namespace MauiApp1.Services;

/// <summary>
/// Service for displaying dialogs and alerts from ViewModels
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Show a simple alert dialog
    /// </summary>
    Task ShowAlertAsync(string title, string message, string cancel = "OK");

    /// <summary>
    /// Show a confirmation dialog with Yes/No buttons
    /// </summary>
    Task<bool> ShowConfirmAsync(string title, string message, string accept = "Yes", string cancel = "No");
}
