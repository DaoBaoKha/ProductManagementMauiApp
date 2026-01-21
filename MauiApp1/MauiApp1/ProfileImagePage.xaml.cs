using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class ProfileImagePage : ContentPage
{
	public ProfileImagePage(ProfileImagePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
