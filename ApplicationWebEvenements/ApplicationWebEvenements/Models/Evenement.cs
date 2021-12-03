using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApplicationWebEvenements.Models
{
    public class Evenement
    {
        [JsonProperty("idEvenement")]
        public int IdEvenement { get; set; }
        [JsonProperty("nomEvenement")]
        public string NomEvenement { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("date")]
        public string Date {  get; set; }
        [JsonProperty("idOrganisateur")]
        public int IdOrganisateur { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        public Utilisateur Organisateur { get; set; }
        public string LienImage { get; set; }

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
