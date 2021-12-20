﻿using Services.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Entidades
{
    public class Developer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        [FirstCapitalLetterAttribute]
        public string Name { get; set; }
        public DateTimeOffset AddedDate { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]

        public int CostByDay { get; set; }
    }
}
