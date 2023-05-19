using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaZadan.Modele
{
    public class Zadanie
    {
        public int ZadanieId { get; set; }
        public string Nazwa { get; set; }
        public int? KategoriaId { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}
