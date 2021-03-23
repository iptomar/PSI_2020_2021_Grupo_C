using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LuizaAndaluz.Models
{
    public class Local
    {
        [Key]
        public String ID { get; set; }

        [Required]
        public String Latitude { get; set; }

        [Required]
        public String Longitude { get; set; }

        public virtual ICollection<Historia> Historia { get; set; }

        public Local()
        {
            Historia = new HashSet<Historia>();
        }

    }
}