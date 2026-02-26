using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class UserAnalyticsPage : ContentPage
{
	public UserAnalyticsPage(UserAnalyticsPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        // GIẢI PHÁP:
        // 1. {VP$0} : Format giá trị VP thành số nguyên (0), KHÔNG dùng % trong format để tránh bị nhân 100.
        // 2. %      : Tự viết dấu % ở ngoài như một chữ cái bình thường.

        SeriesLabel.TextPattern = "{V} ({VP$0}%)";
    }
}
