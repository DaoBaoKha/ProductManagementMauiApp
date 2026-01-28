using System.Text.RegularExpressions;

namespace MauiApp1.Behaviors
{
    /// <summary>
    /// Behavior to validate email format in Entry controls
    /// Changes text color to red if email is invalid
    /// </summary>
    public class EmailValidationBehavior : Behavior<Entry>
    {
        // Email regex pattern - basic validation
        private static readonly Regex EmailRegex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // BindableProperty với TwoWay mode
        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(
                nameof(IsValid),
                typeof(bool),
                typeof(EmailValidationBehavior),
                false,
                BindingMode.TwoWay); // ⚡ Explicitly set TwoWay

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
            
            // Initial validation on attach
            ValidateEntry(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                ValidateEntry(entry);
            }
        }

        private void ValidateEntry(Entry entry)
        {
            var text = entry.Text;

            // If empty, invalid
            if (string.IsNullOrWhiteSpace(text))
            {
                IsValid = false;
                entry.TextColor = Colors.Black;
                return;
            }

            // Validate email format
            bool isValid = EmailRegex.IsMatch(text);
            
            // Update IsValid property
            IsValid = isValid;

            // Set color based on validation
            entry.TextColor = isValid ? Colors.Black : Colors.Red;
        }
    }
}
