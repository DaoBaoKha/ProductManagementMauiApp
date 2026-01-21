using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (sender is Entry entry)
            {
                bool hasNumber = args.NewTextValue?.Any(char.IsDigit) ?? false;
                entry.TextColor = hasNumber ? Colors.Red : Colors.Black;
            }
        }
    }
}
