using System.Collections.ObjectModel;
using Duocare2.Models;

namespace Duocare2.ViewModels;

public class ChildCalendarViewModel : BaseViewModel
{
    public ObservableCollection<Day> DaysOfMonth { get; set; }

    private DateTime _fechaActual;

    public string MesActual => _fechaActual.ToString("MMMM yyyy").ToUpper();

    public ChildCalendarViewModel()
    {
        DaysOfMonth = new ObservableCollection<Day>();
        _fechaActual = DateTime.Now;
        GenerarCalendario(_fechaActual);
    }

    // ============================
    //   GENERAR CALENDARIO
    // ============================
    private void GenerarCalendario(DateTime fecha)
    {
        DaysOfMonth.Clear();

        var primerDia = new DateTime(fecha.Year, fecha.Month, 1);
        int offset = (int)primerDia.DayOfWeek;

        // Placeholder antes del día 1
        for (int i = 0; i < offset; i++)
            DaysOfMonth.Add(new Day { IsPlaceholder = true });

        int diasMes = DateTime.DaysInMonth(fecha.Year, fecha.Month);

        // Días reales
        for (int dia = 1; dia <= diasMes; dia++)
        {
            DaysOfMonth.Add(new Day
            {
                Number = dia,
                Date = new DateTime(fecha.Year, fecha.Month, dia),
                HasTask = false,
                HasEvent = false,
                TaskDescription = "",
                TaskHour = new TimeSpan(12, 0, 0)
            });
        }

        OnPropertyChanged(nameof(MesActual));
    }

    // ============================
    //   COMANDOS DE NAVEGACIÓN
    // ============================
    public Command MesAnteriorCommand => new Command(() =>
    {
        _fechaActual = _fechaActual.AddMonths(-1);
        GenerarCalendario(_fechaActual);
    });

    public Command MesSiguienteCommand => new Command(() =>
    {
        _fechaActual = _fechaActual.AddMonths(1);
        GenerarCalendario(_fechaActual);
    });

    public Command IrHoyCommand => new Command(() =>
    {
        _fechaActual = DateTime.Now;
        GenerarCalendario(_fechaActual);
    });
}
