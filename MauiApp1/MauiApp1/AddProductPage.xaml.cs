using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class AddProductPage : ContentPage
{
	public AddProductPage(AddProductPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}