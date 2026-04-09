using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Duocare2.ViewModels;

public class ChildNameMessage : ValueChangedMessage<string>
{
    public ChildNameMessage(string value) : base(value) { }
}
