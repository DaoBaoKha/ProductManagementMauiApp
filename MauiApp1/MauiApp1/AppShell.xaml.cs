namespace MauiApp1
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Register detail page routes (pages pushed onto navigation stack)
            Routing.RegisterRoute(nameof(UserManagePage), typeof(UserManagePage));
            Routing.RegisterRoute(nameof(ProductManagePage), typeof(ProductManagePage));
            Routing.RegisterRoute(nameof(ProfileImagePage), typeof(ProfileImagePage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(DemoPage), typeof(DemoPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

        private async void OnLogoutClicked(object? sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                // Navigate back to login page (outside Shell)
                if (Application.Current is App app)
                {
                    app.NavigateToLogin();
                }
            }
        }
    }
}
