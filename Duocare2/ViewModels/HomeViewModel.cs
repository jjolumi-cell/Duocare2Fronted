using System.Collections.ObjectModel;

namespace Duocare2.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public string ChildName { get; set; }
    public string PetName { get; set; }

    public bool ShowChildCalendar { get; set; }
    public bool ShowPetCalendar { get; set; }

    public Command ToggleChildCalendarCommand { get; }
    public Command TogglePetCalendarCommand { get; }

    public ObservableCollection<string> ChildEvents { get; set; } = new();
    public ObservableCollection<string> PetEvents { get; set; } = new();

    public HomeViewModel()
    {
        ChildName = Preferences.Get("ChildName", "Not registered");
        PetName = Preferences.Get("PetName", "Not registered");

        ToggleChildCalendarCommand = new Command(() =>
        {
            ShowChildCalendar = !ShowChildCalendar;
            OnPropertyChanged(nameof(ShowChildCalendar));
        });

        TogglePetCalendarCommand = new Command(() =>
        {
            ShowPetCalendar = !ShowPetCalendar;
            OnPropertyChanged(nameof(ShowPetCalendar));
        });
    }
}
