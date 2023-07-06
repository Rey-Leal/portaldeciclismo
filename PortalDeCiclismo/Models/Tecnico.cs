using System;
using System.Collections.Generic;

namespace PortalDeCiclismo.Models
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Treino> Treinos { get; set; }
    }
}