using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class ProductManagePage : ContentPage
{
	public ProductManagePage(ProductManagePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void OnDataGridTap(object sender, DevExpress.Maui.DataGrid.DataGridGestureEventArgs e)
    {
        if (e.Item != null && BindingContext is ProductManagePageViewModel viewModel)
        {
            viewModel.ShowProductDetailsCommand.Execute(e.Item);
        }
    }
}