using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Messages;
using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class AddUserPage : ContentPage
{
	public AddUserPage(AddUserPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		WeakReferenceMessenger.Default.Register<ShowToastMessage>(this, async (r, m) =>
		{
			await ToastNotificationControl.Show(m.Value);

		});
    }
}