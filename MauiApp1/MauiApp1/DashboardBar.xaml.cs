using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class DashboardBar : ContentPage
{
	public DashboardBar(DashboardBarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}