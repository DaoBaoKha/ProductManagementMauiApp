namespace MauiApp1.Controls;

public partial class ToastNotification : ContentView
{
    public ToastNotification()
    {
        InitializeComponent();
    }

    public async Task Show(string message)
    {
        // setup
        MessageLabel.Text = message;
        this.IsVisible = true;
        this.TranslationY = -100;
        this.Opacity = 1;

        // slide in animation
        await this.TranslateTo(0, 0, 600, Easing.SpringOut);

        await Task.Delay(2000);

        // slide out animation
        await Task.WhenAll(
            this.TranslateTo(0, -100, 400, Easing.CubicIn),
            this.FadeTo(0, 300)
        );

        // clean
        this.IsVisible = false;
        this.Opacity = 1; // reset opacity
    }
}