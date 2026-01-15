using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class AddUserPage : ContentPage
{
	public AddUserPage(AddUserPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}