using Duocare2.ViewModels;

namespace Duocare2.Views;

public partial class FichaPage : ContentPage
{
    public FichaPage()
    {
        InitializeComponent();
        BindingContext = new FichaViewModel(); // IMPORTANTE
    }
}
