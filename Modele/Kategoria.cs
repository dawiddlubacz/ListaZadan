using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZadan.Modele
{
    public class Kategoria
    {
        public int KategoriaId { get; set; }
        public string Nazwa { get; set; }
        public virtual ICollection<Zadanie> Zadania { get; set; }
    }
}
