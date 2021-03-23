using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaAndaluz.Models
{
    public class Conteudo
    {
        [Key]
        public String ID { get; set; }

        public String Ficheiro { get; set; }

        public String Tipo { get; set; }

        [Required]
        [ForeignKey(nameof(Historia))]
        public String HistoriaFK { get; set; }
        public Historia Historia { get; set; }

    }
}