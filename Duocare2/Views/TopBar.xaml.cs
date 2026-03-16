namespace Duocare2.Views;

public partial class TopBar : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(TopBar), default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Command GoBackCommand { get; }

    public TopBar()
    {
        InitializeComponent();
        GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        BindingContext = this;
    }
}
