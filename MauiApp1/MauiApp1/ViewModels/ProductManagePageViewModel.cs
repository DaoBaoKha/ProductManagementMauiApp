using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.AppLogic.DTOs;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class ProductManagePageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isPopupOpen;

        [ObservableProperty]
        ProductDto selectedProduct;

        public ObservableCollection<ProductDto> Products { get; } = new();

        public ProductManagePageViewModel(INavigationService navigationService)
        {
            Title = "Product Manage Page";
            _navigationService = navigationService;

            Task.Run(async () => await LoadProduct());
        }

        [RelayCommand]
        async Task LoadProduct()
        {
            IsRefreshing = true;
            await Task.Delay(2000); // Simulate a data load

            Products.Clear();

            Products.Add(new ProductDto
            {
                Id = "1",
                Name = "Coca Cola",
                Description = "Beverage Drink",
                Price = 19.99m,
                StockQuantity = 100,
                ImageUrl = "https://example.com/productA.png",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                Status = "Active"
            });

            Products.Add(new ProductDto
            {
                Id = "2",
                Name = "Pepsi",
                Description = "Beverage Drink",
                Price = 19.99m,
                StockQuantity = 100,
                ImageUrl = "https://example.com/productA.png",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                Status = "Active"
            });

            IsRefreshing = false;
        }
    }
}
