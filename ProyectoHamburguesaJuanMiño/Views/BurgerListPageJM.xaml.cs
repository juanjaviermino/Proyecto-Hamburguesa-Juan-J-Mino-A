namespace ProyectoHamburguesaJuanMiño.Views;

using Microsoft.Maui.Controls;
using ProyectoHamburguesaJuanMiño.Models;
using System.Collections.Generic;

public partial class BurgerListPageJM : ContentPage
{
    bool buscarEjecutado = false;

	public BurgerListPageJM()
	{
		InitializeComponent();
        List<BurgerJM> burger = App.Repositorio.GetAllBurgers();
        burgerListJM.ItemsSource = burger;
        if(!burger.Any())
        {
            mensajeJM.IsVisible = true;
        }
        else
        {
            mensajeJM.IsVisible = false;
        }
    }

    async void OnItemAdded(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BurgerItemPageJM));
    }

    public void buscarBurger(object sender, EventArgs e)
    {
        List<BurgerJM> burger = App.Repositorio.GetBurgersByName(buscadoJM.Text);
        burgerListJM.ItemsSource = burger;
        if (!burger.Any())
        {
            mensajeJM.IsVisible = true;
        }
        else
        {
            mensajeJM.IsVisible = false;
        }
        buscarEjecutado = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        List<BurgerJM> burger = App.Repositorio.GetAllBurgers();
        burgerListJM.ItemsSource = burger;
        if (!burger.Any())
        {
            mensajeJM.IsVisible = true;
        }
        else
        {
            mensajeJM.IsVisible = false;
        }
    }

    private void cambioTexto(object sender, TextChangedEventArgs e)
    {
        var newText = e.NewTextValue;
        if (string.IsNullOrWhiteSpace(newText))
        {
            botonBorrarJM.IsEnabled = false;
            botonBorrarJM.IsVisible = false;
            if(buscarEjecutado == true)
            {
                buscarEjecutado = false;
                List<BurgerJM> burger = App.Repositorio.GetAllBurgers();
                burgerListJM.ItemsSource = burger;
                if (!burger.Any())
                {
                    mensajeJM.IsVisible = true;
                }
                else
                {
                    mensajeJM.IsVisible = false;
                }
            }
        }
        else
        {
            botonBorrarJM.IsEnabled = true;
            botonBorrarJM.IsVisible = true;
            if (buscarEjecutado == true)
            {
                List<BurgerJM> burger = App.Repositorio.GetBurgersByName(buscadoJM.Text);
                burgerListJM.ItemsSource = burger;
                if (!burger.Any())
                {
                    mensajeJM.IsVisible = true;
                }
                else
                {
                    mensajeJM.IsVisible = false;
                }
            }
        }
    }


    private async void cambioSeleccion(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var hamburguesa = (Models.BurgerJM)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(BurgerItemPageJM)}?{nameof(BurgerItemPageJM.ItemId)}={hamburguesa.IdJM}");

            // Unselect the UI
            burgerListJM.SelectedItem = null;
        }
    }

    private void borrarEntryBuscar(object sender, EventArgs e)
    {
        buscadoJM.Text = "";
    }

    private async void PurgarLista(object sender, EventArgs e)
    {
        bool respuesta = await DisplayAlert("Alerta", "¿Estás seguro de que desea purgar la lista? \n" +
            "Esta acción borrará todos los items de la base de datos y restaurará los Ids", "No", "Sí");
        if (!respuesta)
        {
            App.Repositorio.purgarBurger();
            List<BurgerJM> burger = App.Repositorio.GetAllBurgers();
            burgerListJM.ItemsSource = burger;
            if (!burger.Any())
            {
                mensajeJM.IsVisible = true;
            }
            else
            {
                mensajeJM.IsVisible = false;
            }
        }
        else
        {
            return;
        }
    }

    private void onPressed(object sender, EventArgs e)
    {
        BotonAñadirJM.BackgroundColor = Color.FromArgb("#D52941");
    }

    private void onReleased(object sender, EventArgs e)
    {
        BotonAñadirJM.BackgroundColor = Color.FromArgb("#512bd4");
    }
}