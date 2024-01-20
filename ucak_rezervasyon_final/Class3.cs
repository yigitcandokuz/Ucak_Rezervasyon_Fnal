using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ucak_rezervasyon_final
{
   public class Ucus
    {
        public Lokasyon KalkisYeri { get; set; }
        public Lokasyon VarisYeri { get; set; }
        public DateTime Saat { get; set; }
        public Ucak UcakBilgisi { get; set; }
        public int RezervasyonSayisi { get; set; }
    }

}
