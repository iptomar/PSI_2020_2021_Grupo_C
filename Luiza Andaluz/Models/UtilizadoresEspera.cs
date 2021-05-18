
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaAndaluz.Models
{
    public class UtilizadoresEspera
    {
        [Key]
        public String ID { get; set; }

        public String Email { get; set; }
    }
}