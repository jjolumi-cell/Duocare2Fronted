using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Duocare2.Models;
using Microsoft.Maui.Controls;

public class MonthlyCalendarViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private DateTime _fechaActual = DateTime.Now;
    private string _mesActual;

    public string MesActual
    {
        get => _mesActual;
        set { _mesActual = value; OnPropertyChanged(); }
    }

    public ObservableCollection<Day> DaysOfMonth { get; set; }

    public Command MesAnteriorCommand { get; }
    public Command MesSiguienteCommand { get; }
    public Command IrHoyCommand { get; }

    public MonthlyCalendarViewModel()
    {
        MesAnteriorCommand = new Command(() => CambiarMes(-1));
        MesSiguienteCommand = new Command(() => CambiarMes(1));
        IrHoyCommand = new Command(() => IrHoy());

        DaysOfMonth = new ObservableCollection<Day>();

        CargarMes();
    }

    private void CargarMes()
    {
        DaysOfMonth.Clear();

        // ✔ CORREGIDO: sin ToString("formato")
        MesActual = $"{_fechaActual:MMMM yyyy}".ToUpper();

        int diasEnMes = DateTime.DaysInMonth(_fechaActual.Year, _fechaActual.Month);

        int primerDiaSemana = (int)new DateTime(_fechaActual.Year, _fechaActual.Month, 1).DayOfWeek;

        for (int i = 0; i < primerDiaSemana; i++)
        {
            DaysOfMonth.Add(new Day
            {
                IsPlaceholder = true
            });
        }

        for (int i = 1; i <= diasEnMes; i++)
        {
            DaysOfMonth.Add(new Day
            {
                Number = i,
                Date = new DateTime(_fechaActual.Year, _fechaActual.Month, i)
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

    private void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
