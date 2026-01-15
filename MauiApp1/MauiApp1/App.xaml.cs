namespace MauiApp1
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;
        }

        public IServiceProvider Services { get; }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // start with login page
            var loginPage = Services.GetRequiredService<LoginPage>();
            
            var navigationPage = new NavigationPage(loginPage);
            navigationPage.BarBackgroundColor = Colors.LightBlue;
            navigationPage.BarTextColor = Colors.White;

            // Resolve DashboardBar from DI container
            var dashboardBar = Services.GetRequiredService<DashboardBar>();
            
            var flyoutPage = new FlyoutPage
            {
                Flyout = dashboardBar,
                Detail = navigationPage
            };

            return new Window(flyoutPage);
        }
    }
}