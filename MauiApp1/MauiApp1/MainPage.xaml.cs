using CommunityToolkit.Mvvm.Messaging;
using Grpc.Net.Client;
using GrpcService1;
using MauiApp1.Messages;
using MauiApp1.ViewModels;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {

        private bool _isAutoScrollEnabled = true;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            WeakReferenceMessenger.Default.Register<ShowToastMessage>(this, async (r, m) =>
            {
                await LoginToast.Show(m.Value);
            });
        }

        // only run when on main page
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // enable auto-scrolling when the page appears
            _isAutoScrollEnabled = true;

            /*
             * run each 3 seconds
             * dispatcher: provides a way to execute code on the UI thread
             */
            Dispatcher.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                // if user leave main page -> stop auto scroll
                if (!_isAutoScrollEnabled) return false;

                // auto scroll logic
                // tale view model to get number of banners
                var viewModel = BindingContext as MainPageViewModel;
                if (viewModel != null && viewModel.Banners.Count > 0)
                {
                    // calculate next position
                    var nextPosition = (MainBanner.Position + 1) % viewModel.Banners.Count;

                    MainBanner.Position = nextPosition;
                }

                return true; // retyrb trye to keep timer running
            });
        }

        // run when leave main page
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // disable auto-scrolling when the page disappears
            _isAutoScrollEnabled = false;
        }

        // ====================================================
        // TEST GRPC
        // ====================================================
        private async void OnTestGrpcClicked(object sender, EventArgs e)
        {
            // update status label
            GrpcStatusLabel.Text = "Connecting to 10.0.2.2:7032...";
            GrpcStatusLabel.TextColor = Colors.Orange;

            try
            {

                // use sockets handler instead of HttpClientHandler
                var handler = new SocketsHttpHandler();

                // DEMO ONLY: skip SSL certificate validation
                handler.SslOptions.RemoteCertificateValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true;

                // create channel with the handler
                var channel = GrpcChannel.ForAddress("https://10.0.2.2:7032", new GrpcChannelOptions
                {
                    HttpHandler = handler
                });

                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "Dao Bao Kha" });

                GrpcStatusLabel.Text = $"SUCCESS: {reply.Message}";
                GrpcStatusLabel.TextColor = Colors.Green;
            }
            catch (Exception ex)
            {
                GrpcStatusLabel.Text = $"ERROR: {ex.Message}";
                GrpcStatusLabel.TextColor = Colors.Red;
            }
        }
    }
}
