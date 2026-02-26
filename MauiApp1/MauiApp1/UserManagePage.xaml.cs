using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class UserManagePage : ContentPage
{
	public UserManagePage(UserManagePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void OnDataGridTap(object sender, DevExpress.Maui.DataGrid.DataGridGestureEventArgs e)
    {
        if (e.Item != null && BindingContext is UserManagePageViewModel viewModel)
        {
            viewModel.ShowUserDetailsCommand.Execute(e.Item);
        }
    }
}