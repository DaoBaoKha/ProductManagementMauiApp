using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class DemoPage : ContentPage
{
	public DemoPage(DemoPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
