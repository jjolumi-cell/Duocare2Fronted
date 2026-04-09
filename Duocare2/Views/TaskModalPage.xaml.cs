using Duocare2.Models;
using Duocare2.ViewModels;

namespace Duocare2.Views
{
    public partial class TaskModalPage : ContentPage
    {
        public TaskModalPage(Day day)
        {
            InitializeComponent();
            BindingContext = new TaskModalViewModel(day);
        }
    }
}
