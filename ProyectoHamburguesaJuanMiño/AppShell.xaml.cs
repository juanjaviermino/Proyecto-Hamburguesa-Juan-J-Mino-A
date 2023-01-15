namespace ProyectoHamburguesaJuanMiño;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Views.BurgerItemPageJM), typeof(Views.BurgerItemPageJM));
        Routing.RegisterRoute(nameof(Views.BurgerListPageJM), typeof(Views.BurgerListPageJM));
    }
}
