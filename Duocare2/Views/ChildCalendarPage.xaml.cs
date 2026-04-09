using Duocare2.Models;
using Duocare2.ViewModels;

namespace Duocare2.Views;

public partial class ChildCalendarPage : ContentPage
{
    private Day selectedDay;

    public ChildCalendarPage()
    {
        InitializeComponent();
        BindingContext = new ChildCalendarViewModel();
    }

    // ============================
    //   TAP EN UN DÍA
    // ============================
    private async void OnDayTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Day day)
        {
            selectedDay = day;

            PopupTitulo.Text = $"Día {day.Number}";
            PopupFecha.Date = day.Date; // DateTime (no nullable)
            PopupHora.Time = day.TaskHour ?? new TimeSpan(12, 0, 0);
            PopupDescripcion.Text = day.TaskDescription ?? "";

            await MostrarPopup();
        }
    }

    // ============================
    //   MOSTRAR POPUP
    // ============================
    private async Task MostrarPopup()
    {
        PopupFondo.IsVisible = true;
        await PopupFondo.FadeTo(1, 180);

        PopupDia.IsVisible = true;
        PopupDia.Scale = 0;
        await PopupDia.ScaleTo(1, 220, Easing.CubicOut);
    }

    // ============================
    //   CERRAR POPUP
    // ============================
    private async void CerrarPopup(object sender, EventArgs e)
    {
        await CerrarPopupAnim();
    }

    private async Task CerrarPopupAnim()
    {
        await PopupDia.ScaleTo(0.8, 150, Easing.CubicIn);
        PopupDia.IsVisible = false;

        await PopupFondo.FadeTo(0, 150);
        PopupFondo.IsVisible = false;
    }

    // ============================
    //   GUARDAR
    // ============================
    private async void GuardarPopup(object sender, EventArgs e)
    {
        if (selectedDay == null)
            return;

        // CORRECCIÓN CS0266: DatePicker.Date es DateTime?
        selectedDay.Date = PopupFecha.Date.GetValueOrDefault();

        selectedDay.TaskHour = PopupHora.Time;
        selectedDay.TaskDescription = PopupDescripcion.Text;

        selectedDay.HasTask = true;
        selectedDay.HasEvent = false;

        await DisplayAlert("Guardado", "El día ha sido actualizado.", "OK");
        await CerrarPopupAnim();
    }

    // ============================
    //   ELIMINAR
    // ============================
    private async void EliminarPopup(object sender, EventArgs e)
    {
        if (selectedDay == null)
            return;

        selectedDay.HasTask = false;
        selectedDay.HasEvent = false;
        selectedDay.TaskDescription = "";
        selectedDay.TaskHour = new TimeSpan(12, 0, 0);

        await DisplayAlert("Eliminado", "El contenido del día ha sido eliminado.", "OK");
        await CerrarPopupAnim();
    }

    // ============================
    //   FAB (+)
    // ============================
    private async void OnFabClicked(object sender, EventArgs e)
    {
        if (selectedDay == null)
        {
            await DisplayAlert("Selecciona un día", "Primero toca un día del calendario.", "OK");
            return;
        }

        PopupTitulo.Text = $"Día {selectedDay.Number}";
        PopupFecha.Date = selectedDay.Date;
        PopupHora.Time = selectedDay.TaskHour ?? new TimeSpan(12, 0, 0);
        PopupDescripcion.Text = selectedDay.TaskDescription;

        await MostrarPopup();
    }
}
