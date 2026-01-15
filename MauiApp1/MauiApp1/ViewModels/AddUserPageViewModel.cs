using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public partial class AddUserPageViewModel : BaseViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public AddUserPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            Title = "Add User Page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        async Task SaveUser()
        {
            await _dialogService.ShowAlertAsync("Add User", "User saved successfully!");
        }
    }
}
