using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Messages;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public partial class AddProductPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public AddProductPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            Title = "Add Product Page";
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNameValid), nameof(IsFormValid))]
        private string name = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsDescriptionValid), nameof(IsFormValid))]
        private string description = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPriceValid), nameof(IsFormValid))]
        private string priceText = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsStockValid), nameof(IsFormValid))]
        private string stockText = string.Empty;

        /// <summary>
        /// Name is valid if not empty
        /// </summary>
        public bool IsNameValid => !string.IsNullOrWhiteSpace(Name);

        /// <summary>
        /// Description is valid if not empty
        /// </summary>
        public bool IsDescriptionValid => !string.IsNullOrWhiteSpace(Description);

        /// <summary>
        /// Price is valid if it's a valid decimal > 0
        /// </summary>
        public bool IsPriceValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PriceText))
                    return false;
                if (decimal.TryParse(PriceText, out decimal price))
                    return price > 0;
                return false;
            }
        }

        /// <summary>
        /// Stock is valid if it's a valid integer >= 0
        /// </summary>
        public bool IsStockValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(StockText))
                    return false;
                if (int.TryParse(StockText, out int stock))
                    return stock >= 0;
                return false;
            }
        }

        /// <summary>
        /// Form is valid when all fields are valid
        /// </summary>
        public bool IsFormValid => IsNameValid && IsDescriptionValid && IsPriceValid && IsStockValid;

        [RelayCommand]
        async Task SaveProduct()
        {
            var price = decimal.Parse(PriceText);
            var stock = int.Parse(StockText);

            WeakReferenceMessenger.Default.Send(new ShowToastMessage($"Product '{Name}' added successfully!"));

            // Clear form after save
            Name = string.Empty;
            Description = string.Empty;
            PriceText = string.Empty;
            StockText = string.Empty;
        }
    }
}
