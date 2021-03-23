
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaAndaluz.Models
{
    public class Utilizador
    {
        [Key]
        public String ID { get; set; }

        public DateTime Nascimento { get; set; }

        public String Aut { get; set; }

        public virtual ICollection<Historia> Historia { get; set; }

        public Utilizador()
        {
            Historia = new HashSet<Historia>();
        }
    }
}