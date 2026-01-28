using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MauiApp1.Messages
{
    public class ShowToastMessage : ValueChangedMessage<string>
    {
        public ShowToastMessage(string value) : base(value)
        {
        }
    }
}
