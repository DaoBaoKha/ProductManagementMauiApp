using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class UserManagePage : ContentPage
{
	public UserManagePage(UserManagePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}