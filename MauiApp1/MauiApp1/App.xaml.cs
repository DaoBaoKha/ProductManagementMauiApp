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
            // Start with login page (outside Shell) - Option A
            return new Window(CreateLoginPage());
        }

        /// <summary>
        /// Creates a NavigationPage with LoginPage as root
        /// </summary>
        private NavigationPage CreateLoginPage()
        {
            var loginPage = Services.GetRequiredService<LoginPage>();
            return new NavigationPage(loginPage)
            {
                BarBackgroundColor = Colors.LightBlue,
                BarTextColor = Colors.White
            };
        }

        /// <summary>
        /// Called after successful login - navigates to main app (Shell)
        /// </summary>
        public void NavigateToMainApp()
        {
            var shell = Services.GetRequiredService<AppShell>();
            if (Windows.Count > 0)
            {
                Windows[0].Page = shell;
            }
        }

        /// <summary>
        /// Called on logout - navigates back to login page
        /// </summary>
        public void NavigateToLogin()
        {
            if (Windows.Count > 0)
            {
                Windows[0].Page = CreateLoginPage();
            }
        }
    }
}