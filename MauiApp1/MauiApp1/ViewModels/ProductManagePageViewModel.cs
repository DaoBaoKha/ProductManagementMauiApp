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

        private List<ProductDto> AllProducts { get; set; } = new();

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

            AllProducts.Clear();
            Products.Clear();

            var product1 = new ProductDto
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
            };

            var product2 = new ProductDto
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
            };

            AllProducts.Add(product1);
            AllProducts.Add(product2);
            
            Products.Add(product1);
            Products.Add(product2);

            IsRefreshing = false;
        }

        [RelayCommand]
        async Task GoToAddProductPage()
        {
            await _navigationService.NavigateToAsync<AddProductPageViewModel>();
        }

        [RelayCommand]
        void ShowProductDetails(ProductDto product)
        {
            if (product != null)
            {
                SelectedProduct = product;
                IsPopupOpen = true;
            }
        }

        [RelayCommand]
        void ClosePopup()
        {
            IsPopupOpen = false;
        }

        [RelayCommand]
        void SearchProduct(string searchText)
        {
            if(string.IsNullOrWhiteSpace(searchText))
            {
                Products.Clear();
                foreach (var product in AllProducts)
                {
                    Products.Add(product);
                }
            }
            else
            {
                var filteredProducts = AllProducts
                    .Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                Products.Clear();
                foreach (var product in filteredProducts)
                {
                    Products.Add(product);
                }
            }
        }
    }
}
