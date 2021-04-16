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


        [Required(ErrorMessage = "O Título é de preenchimento obrigatório.")]
        [Display(Name = "Título")]
        public String Titulo { get; set; }


        [Required(ErrorMessage = "A Descrição é de preenchimento obrigatório.")]
        [Display(Name = "Descrição")]
        public String Descricao { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "O Nome é de preenchimento obrigatório.")]
        [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
        [RegularExpression("[A-ZÂÓÍÉ][a-záéíóúàèìòùâêîôûãôûäëïöüçñ]+(( | d[oa](s)? | (d)?e |-|'| d')[A-ZÂÓÍÉ][a-záéíóúàèìòùâêîôûãôûäëïöüçñ]+){1,3}",
         ErrorMessage = "Só são aceites letras.<br />A primeira letra de cada nome é uma Maiúscula seguida de letras minúsculas.<br />Deve escrever entre 2 e 4 nomes.")]
        public String Nome { get; set; }


        [Required(ErrorMessage = "A Idade é de preenchimento obrigatório.")]
        [RegularExpression("[1-9][0-9]", ErrorMessage = "A idade deve ser entre 10 e 99 anos")]
        public int Idade { get; set; }


        [Required(ErrorMessage = "O Email é de preenchimento obrigatório.")]
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