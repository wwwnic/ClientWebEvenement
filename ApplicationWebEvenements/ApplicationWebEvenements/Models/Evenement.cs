using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationWebEvenements.Models
{
    public class Evenement
    {
        [JsonPropertyName("idEvenement")]
        public int IdEvenement { get; set; }
        [JsonPropertyName("nomEvenement")]
        public string NomEvenement { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("idOrganisateur")]
        public int IdOrganisateur { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        public Utilisateur Organisateur { get; set; }
        public string LienImage { get; set; }
    }
}
