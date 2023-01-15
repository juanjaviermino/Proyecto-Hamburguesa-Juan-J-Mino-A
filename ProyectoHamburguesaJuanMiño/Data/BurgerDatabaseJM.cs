using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoHamburguesaJuanMiño.Models;
using SQLite;

namespace ProyectoHamburguesaJuanMiño.Data
{
    public class BurgerDatabaseJM
    {
        string _dbPath;                                  //String que tendrá la conexión a la base de datos
        private SQLiteConnection conn;                   //Variable que tendrá la conexión

        public BurgerDatabaseJM(string DatabasePath)     //Constructor de la clase
        {
            _dbPath = DatabasePath;
        }

        private void Init()
        {
            if (conn != null)                            //Si la conexión ya existe, no se hace nada
                return;
            conn = new SQLiteConnection(_dbPath);        //Si no existe la conexión, la creamos
            conn.CreateTable<BurgerJM>();                //Creamos la tabla Burger, si ya está creada entonces solo se actualiza
        }

        public int AddNewBurger(BurgerJM burger)         //Crea la conexión, inserta un objeto nuevo, regresa las filas afectadas
        {
            Init();
            try
            {
                int result = conn.Insert(burger);
                return result;
            }
            catch(Exception e)
            {
                return 404;
            }
            
        }

        public List<BurgerJM> GetAllBurgers()            //Crea la conexión, recupera una lista de todas las filas, regresa la lista
        {
            Init();
            List<BurgerJM> burgers = conn.Table<BurgerJM>().ToList();
            return burgers;
        }

        public List<BurgerJM> GetBurgersByName(string contiene)
        {
            Init();
            var burger = from b in conn.Table<BurgerJM>()
                       where b.NombreJM.Contains(contiene)
                       select b;

            return burger.ToList();
        }

        public BurgerJM GetById(int id)
        {
            Init();
            var burger = from b in conn.Table<BurgerJM>()
                         where b.IdJM == id
                         select b;

            return burger.FirstOrDefault();
        }

        public void actualizarBurger(int id, string nom, string des, bool ques)
        {
            Init();
            var burgerNueva = conn.Table<BurgerJM>().Where(r => r.IdJM == id).FirstOrDefault();
            if (burgerNueva != null)
            {
                // Update the values
                burgerNueva.NombreJM = nom;
                burgerNueva.DescripcionJM = des;
                burgerNueva.ConQuesoExtraJM = ques;

                // Submit the changes to the database
                conn.Update(burgerNueva);
            }
        }

        public void eliminarBurger(int id)
        {
            var burgerEliminada = conn.Table<BurgerJM>().Where(r => r.IdJM == id).FirstOrDefault();
            if (burgerEliminada != null)
            {
                conn.Delete(burgerEliminada);
            }
        }

        public void purgarBurger()
        {
            conn.DeleteAll<BurgerJM>();
            conn.Execute("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'burger'");
        }
    }
}
