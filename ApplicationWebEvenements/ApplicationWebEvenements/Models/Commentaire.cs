using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationWebEvenements.Models
{
    public class Commentaire
    {

        public int IdCommentaire { get; }
        public int IdEvenement { get; set; }
        public int IdUtilisateur { get; set; }
        public string Date { get; set; }
        public string Texte { get; set; }
    }
}
