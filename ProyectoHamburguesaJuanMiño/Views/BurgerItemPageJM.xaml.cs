namespace ProyectoHamburguesaJuanMiño.Views;
using ProyectoHamburguesaJuanMiño.Models;
using ProyectoHamburguesaJuanMiño.Data;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class BurgerItemPageJM : ContentPage
{
    BurgerJM Item = new BurgerJM();
    BurgerJM aux = new BurgerJM();
    bool _flag;

    public int ItemId
    {
        set { cargarBurger(value); }
    }

    public BurgerItemPageJM()
	{
		InitializeComponent();
	}
    
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if(BindingContext == null)
        {
            Item.NombreJM = nombreJM.Text;
            Item.DescripcionJM = descripcionJM.Text;
            Item.ConQuesoExtraJM = _flag;
            int error = App.Repositorio.AddNewBurger(Item);
            if(error == 404)
            {
                await DisplayAlert("Alerta", "La hamburguesa no se pudo ingresar debido a que su nombre ya existe", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
            
        }
        else
        {
            App.Repositorio.actualizarBurger(aux.IdJM, aux.NombreJM, aux.DescripcionJM, aux.ConQuesoExtraJM);
            await Shell.Current.GoToAsync("..");
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _flag = e.Value;
    }

    private void cargarBurger(int id)
    {
        Models.BurgerJM burgerBuscada = new Models.BurgerJM();
        burgerBuscada = App.Repositorio.GetById(id);
        aux = burgerBuscada;
        BindingContext = burgerBuscada;
    }

    private async void eliminarBurgerBDD(object sender, EventArgs e)
    {
        App.Repositorio.eliminarBurger(aux.IdJM);
        await Shell.Current.GoToAsync("..");
    }
}