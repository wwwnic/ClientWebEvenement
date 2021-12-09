using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Models
{
    public class Recherche
    {
        public string Nom { get; set; }
        [DataType(DataType.Date)]
        public string Mois { get; set; }
        public string Location { get; set; }
        public string Organisateur { get; set; }
    }
}
