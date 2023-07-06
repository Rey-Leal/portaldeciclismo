using System;
using System.Collections.Generic;

namespace PortalDeCiclismo.Models
{
    public enum Sexo
    {
        Masculino, Feminino
    }

    public class Atleta
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public int Peso { get; set; }
        public int Altura { get; set; }
        public Sexo? Sexo { get; set; }

        public virtual ICollection<Treino> Treinos { get; set; }
    }
}