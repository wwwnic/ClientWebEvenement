
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ApplicationWebEvenements.Models
{
    public class Utilisateur
    {
        [JsonProperty("idUtilisateur")]
        public int IdUtilisateur { get; set; }


        [JsonProperty("nomUtilisateur")]
        public string NomUtilisateur { get; set; }


        [StringLength(24, MinimumLength = 4)]
        public string MotDePasse { get; set; }


        [JsonProperty("courriel")]
        public string Courriel { get; set; }


        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        [JsonProperty("dateCreation")]
        public string DateCreation { get; }
        public string LienImage { get; set; }
    }
}
