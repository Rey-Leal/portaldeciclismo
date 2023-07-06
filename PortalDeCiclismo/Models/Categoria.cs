using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalDeCiclismo.Models
{
    public class Categoria
    {        
        public int CategoriaID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Treino> Treinos { get; set; }
    }
}