using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public partial class ProfileImagePageViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        string imageUrl;

        public ProfileImagePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("ImageUrl"))
            {
                ImageUrl = query["ImageUrl"] as string;
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
