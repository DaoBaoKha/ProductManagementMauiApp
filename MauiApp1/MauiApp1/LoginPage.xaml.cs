using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}