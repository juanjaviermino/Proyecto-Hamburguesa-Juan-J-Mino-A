using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHamburguesaJuanMiño.Models
{
    [Table("burger")]
    public class BurgerJM
    {
        [PrimaryKey, AutoIncrement]
        public int IdJM { get; set; }

        [MaxLength(250), Unique]
        public string NombreJM { get; set; }

        public string DescripcionJM { get; set; }

        public bool ConQuesoExtraJM { get; set; }
    }
}
