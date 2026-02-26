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

        [ObservableProperty]
        string searchText;

        public ObservableCollection<ProductDto> Products { get; } = new();

        private List<ProductDto> AllProducts { get; set; } = new();

        public ProductManagePageViewModel(INavigationService navigationService)
        {
            Title = "Product Manage Page";
            _navigationService = navigationService;

            Task.Run(async () => await LoadProduct());
        }

        // Auto-search when SearchText changes
        partial void OnSearchTextChanged(string value)
        {
            SearchProduct(value);
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
                Price = 6.99m,
                StockQuantity = 100,
                ImageUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194801.jpg",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                Status = "Active"
            };

            var product2 = new ProductDto
            {
                Id = "2",
                Name = "Pepsi",
                Description = "Beverage Drink",
                Price = 7.00m,
                StockQuantity = 100,
                ImageUrl = "https://img.freepik.com/premium-vector/character-avatar-isolated_729149-194802.jpg",
                CreatedAt = DateTime.Now.AddDays(-10),
                UpdatedAt = DateTime.Now,
                Status = "Active"
            };

            MainThread.BeginInvokeOnMainThread(() =>
            {
                AllProducts.Clear();
                Products.Clear();

                AllProducts.Add(product1);
                AllProducts.Add(product2);

                Products.Add(product1);
                Products.Add(product2);

                IsRefreshing = false;
            });
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

        // Edit Mode Logic
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotEditing))]
        bool isEditing;

        public bool IsNotEditing => !IsEditing;

        [RelayCommand]
        void EnableEditMode()
        {
            if (!IsEditing) IsEditing = true;
        }

        [RelayCommand]
        async Task SaveProduct()
        {
            // Simulate Save
            IsEditing = false;
            // Here you would call API to update SelectedProduct
            await Application.Current.MainPage.DisplayAlert("Success", "Product Updated", "OK");
        }

        [RelayCommand]
        void CancelEdit()
        {
            IsEditing = false;
            // Ideally revert changes here if DTO was modified directly
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
