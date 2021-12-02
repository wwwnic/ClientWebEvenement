
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApplicationWebEvenements.Models
{
    public class Utilisateur
    {
        [JsonPropertyName("idUtilisateur")]
        public int IdUtilisateur { get; set; }
        [JsonPropertyName("nomUtilisateur")]
        public string NomUtilisateur { get; set; }
        [JsonPropertyName("motDePasse")]
        public string MotDePasse { get; set; }
        [JsonPropertyName("courriel")]
        public string Courriel { get; set; }
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; }
        [JsonPropertyName("dateCreation")]
        public string DateCreation { get; }
        public string LienImage { get; set; }
    }
}
