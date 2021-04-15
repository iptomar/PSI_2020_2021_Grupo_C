using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaAndaluz.Models
{
    /// <summary>
    /// Modelo das historias de luiza andaluz
    /// </summary>
    public class Historia
    {
        [Key]
        public String ID { get; set; }
        [Required]
        public String Titulo { get; set; }
        [Required]
        public String Descricao { get; set; }

        public bool Estado { get; set; }
        [Required]
        public String Nome { get; set; }

        public int Idade { get; set; }
        [Required]
        public String Email { get; set; }

        public DateTime Data { get; set; }

        public String Validador { get; set; }
        /// <summary>
        /// ligação à tabela Local
        /// </summary>
        [Required]
        [ForeignKey(nameof(Local))]
        public String LocalFK { get; set; }
        public Local Local { get; set; }

        public virtual ICollection<Conteudo> Conteudo { get; set; }
        public Historia()
        {
            Conteudo = new HashSet<Conteudo>();
        }

    }
}