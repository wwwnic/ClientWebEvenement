using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationWebEvenements.Models
{
    public class Commentaire
    {
        [JsonProperty("idCommentaire")]
        public int IdCommentaire { get; }
        [JsonProperty("idEvenement")]
        public int IdEvenement { get; set; }
        [JsonProperty("idUtilisateur")]
        public int IdUtilisateur { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("texte")]
        public string Texte { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public string GetDateFormatée()
        {
            var dateHeure = Date.Split("T");
            return dateHeure[0] + " " + dateHeure[1];
        }

        public void SetDateFormatéePourJS()
        {
            var dateHeure = Date.Split("T");
            Date = dateHeure[0] + " " + dateHeure[1];
        }
    }
}
