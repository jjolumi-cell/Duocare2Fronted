using Duocare2.Models;
using Duocare2.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace Duocare2.ViewModels
{
    public class PetCalendarViewModel : BaseViewModel
    {
        private DateTime _fechaActual = DateTime.Now;
        private string _mesActual;

        public string MesActual
        {
            get => _mesActual;
            set => SetProperty(ref _mesActual, value);
        }

        public ObservableCollection<Day> DaysOfMonth { get; }

        public Command MesAnteriorCommand { get; }
        public Command MesSiguienteCommand { get; }
        public Command IrHoyCommand { get; }

        public Command<Day> OpenTaskModalCommand { get; }

        private Day _selectedDay;
        public Day SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (SetProperty(ref _selectedDay, value) && value != null)
                {
                    if (!value.IsPlaceholder)
                        OpenTaskModalCommand.Execute(value);
                }
            }
        }

        public PetCalendarViewModel()
        {
            DaysOfMonth = new ObservableCollection<Day>();

            MesAnteriorCommand = new Command(() => CambiarMes(-1));
            MesSiguienteCommand = new Command(() => CambiarMes(1));
            IrHoyCommand = new Command(IrHoy);

            OpenTaskModalCommand = new Command<Day>(OpenTaskModal);

            CargarMes();
        }

        private void CargarMes()
        {
            DaysOfMonth.Clear();

            MesActual = $"{_fechaActual:MMMM yyyy}".ToUpper();

            int diasEnMes = DateTime.DaysInMonth(_fechaActual.Year, _fechaActual.Month);
            int primerDiaSemana = (int)new DateTime(_fechaActual.Year, _fechaActual.Month, 1).DayOfWeek;

            for (int i = 0; i < primerDiaSemana; i++)
            {
                DaysOfMonth.Add(new Day
                {
                    IsPlaceholder = true,
                    TaskHour = null,
                    TaskDescription = "",
                    HasTask = false
                });
            }

            for (int i = 1; i <= diasEnMes; i++)
            {
                DaysOfMonth.Add(new Day
                {
                    Number = i,
                    Date = new DateTime(_fechaActual.Year, _fechaActual.Month, i),
                    TaskHour = null,
                    TaskDescription = "",
                    HasTask = false
                });
            }
        }

        private void CambiarMes(int offset)
        {
            _fechaActual = _fechaActual.AddMonths(offset);
            CargarMes();
        }

        private void IrHoy()
        {
            _fechaActual = DateTime.Now;
            CargarMes();
        }

        private async void OpenTaskModal(Day day)
        {
            await Shell.Current.Navigation.PushModalAsync(new TaskModalPage(day));
        }
    }
}
