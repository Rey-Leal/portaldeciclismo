using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalDeCiclismo.Models
{
    public class EtapaDeTreino
    {
        public int EtapaDeTreinoID { get; set; }
        public int TreinoID { get; set; }
        public string Descricao { get; set; }

        public virtual Treino Treino { get; set; }
    }
}