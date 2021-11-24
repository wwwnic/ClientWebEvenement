using System;
using System.Collections.Generic;

namespace ApplicationWebEvenements.Models
{
    public class Evenement
    {
        public int IdEvenement { get; }
        public string NomEvenement { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public int IdOrganisateur { get; set; }
        public string Description { get; set; }
    }
}
