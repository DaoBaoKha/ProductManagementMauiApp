using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class ProductManagePage : ContentPage
{
	public ProductManagePage(ProductManagePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}