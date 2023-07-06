using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalDeCiclismo.Models
{
    public enum Desempenho
    {
        A, B, C, D, E
    }

    public class Treino
    {
        public int TreinoID { get; set; }
        public int AtletaID { get; set; }
        public int CategoriaID { get; set; }
        public int TecnicoID { get; set; }

        public Desempenho? Desempenho { get; set; }
        public string Observacao { get; set; }

        public virtual Atleta Atleta { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Tecnico Tecnico { get; set; }
        public virtual ICollection<EtapaDeTreino> EtapasDeTreino { get; set; }
    }
}