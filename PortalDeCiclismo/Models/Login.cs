using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalDeCiclismo.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Digite seu login")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Usuario")]
        [StringLength(30)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Digite sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [StringLength(10)]
        public string Senha { get; set; }

    }

    [Table("Acesso")]
    public class Acesso
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public int Tipo { get; set; }
        public int ID { get; set; }
    }
}