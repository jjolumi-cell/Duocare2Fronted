using System;
using Microsoft.Maui.Controls;
using Duocare2.Models;
using Duocare2.ViewModels;

namespace Duocare2.Views;

// 🐾 SECCIÓN PERSONALIZADA (PET)
public partial class PetCalendarPage : ContentPage
{
    private Day _diaSeleccionado;

    public PetCalendarPage()
    {
        InitializeComponent();

        // 🔁 SECCIÓN DUPLICADA (CHILD & PET)
        BindingContext = new PetCalendarViewModel();
    }

    // ============================
    //   TAP EN UN DÍA DEL CALENDARIO
    // ============================
    // 🔁 SECCIÓN DUPLICADA (CHILD & PET)
    private async void OnDayTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is Day day && !day.IsPlaceholder)
        {
            _diaSeleccionado = day;

            PopupEventoTitulo.Text = $"Tarea - Día {day.Number}";
            FechaPicker.Date = day.Date;

            DescripcionEvento.Text = day.TaskDescription ?? "";

            await MostrarPopup();
        }
    }

    // ============================
    //   BOTÓN FLOTANTE +
    // ============================
    // 🔁 SECCIÓN DUPLICADA (CHILD & PET)
    private async void OnFabClicked(object sender, EventArgs e)
    {
        if (_diaSeleccionado == null)
        {
            await DisplayAlert("Error", "Selecciona un día primero.", "OK");
            return;
        }

        PopupEventoTitulo.Text = $"Tarea - Día {_diaSeleccionado.Number}";
        FechaPicker.Date = _diaSeleccionado.Date;

        DescripcionEvento.Text = _diaSeleccionado.TaskDescription ?? "";

        await MostrarPopup();
    }

    // ============================
    //   MOSTRAR POPUP (PET)
    // ============================
    // 🐾 SECCIÓN PERSONALIZADA (PET)
    private async Task MostrarPopup()
    {
        PopupFondo.IsVisible = true;
        await PopupFondo.FadeTo(1, 180);

        PopupEvento.IsVisible = true;
        PopupEvento.Scale = 0;
        await PopupEvento.ScaleTo(1, 220, Easing.CubicOut);
    }

    // ============================
    //   CERRAR POPUP (PET)
    // ============================
    private async void CerrarPopupEvento(object sender, EventArgs e)
    {
        await CerrarPopup();
    }

    private async Task CerrarPopup()
    {
        await PopupEvento.ScaleTo(0.8, 150, Easing.CubicIn);
        PopupEvento.IsVisible = false;

        await PopupFondo.FadeTo(0, 150);
        PopupFondo.IsVisible = false;
    }

    // ============================
    //           GUARDAR
    // ============================
    // 🔁 SECCIÓN DUPLICADA (CHILD & PET)
    private async void GuardarEvento(object sender, EventArgs e)
    {
        if (_diaSeleccionado == null)
            return;

        string fecha = $"{FechaPicker.Date:dd/MM/yyyy}";
        string descripcion = DescripcionEvento.Text ?? "";

        _diaSeleccionado.TaskDescription = descripcion;
        _diaSeleccionado.HasTask = true;

        await DisplayAlert("Guardado",
            $"Fecha: {fecha}\nDescripción: {descripcion}",
            "OK");

        await CerrarPopup();
    }
}
