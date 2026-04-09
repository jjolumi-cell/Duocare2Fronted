using System;
using System.Windows.Input;
using Duocare2.Models;
using Microsoft.Maui.Controls;

namespace Duocare2.ViewModels
{
    public class TaskModalViewModel : BaseViewModel
    {
        public Day Day { get; }

        public string Title => Day.HasEvent ? $"Evento - Día {Day.Number}" : $"Tarea - Día {Day.Number}";
        public string DateText => Day.Date.ToString("dd/MM/yyyy");

        public string Description
        {
            get => Day.TaskDescription;
            set
            {
                Day.TaskDescription = value;
                Day.HasTask = !string.IsNullOrWhiteSpace(value);
                OnPropertyChanged();
            }
        }

        public TimeSpan? Hour
        {
            get => Day.TaskHour;
            set
            {
                Day.TaskHour = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }

        public TaskModalViewModel(Day day)
        {
            Day = day;

            SaveCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PopModalAsync();
            });

            CloseCommand = new Command(async () =>
            {
                await Shell.Current.Navigation.PopModalAsync();
            });
        }
    }
}
