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
            // Resolve MainPage from DI container
            var mainPage = Services.GetRequiredService<MainPage>();
            
            var navigationPage = new NavigationPage(mainPage);
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