using ProyectoHamburguesaJuanMiño.Data;

namespace ProyectoHamburguesaJuanMiño;

public partial class App : Application
{
    public static BurgerDatabaseJM Repositorio { get; private set; }

    public App(BurgerDatabaseJM repo)
	{
		InitializeComponent();

		MainPage = new AppShell();

        Repositorio = repo;
    }
}
