using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Duocare2.ViewModels;

public class ParentNameMessage : ValueChangedMessage<string>
{
    public ParentNameMessage(string value) : base(value) { }
}
