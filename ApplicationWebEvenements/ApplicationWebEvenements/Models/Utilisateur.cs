using System;
using System.Collections.Generic;

#nullable disable

namespace ApplicationWebEvenements.Models
{
    public class Utilisateur
    {
        public int IdUtilisateur { get; }
        public string NomUtilisateur { get; set; }
        public string MotDePasse { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public string DateCreation { get; set; }
    }
}
