using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationWebEvenements.Models
{
    public class Evenement
    {
        [JsonProperty("idEvenement")]
        public int IdEvenement { get; set; }


        [JsonProperty("nomEvenement")]
        [Display(Name = "Nom evenement")]
        [Required(ErrorMessage = "Nom d'événement requis")]
        [StringLength(40, MinimumLength = 1)]
        public string NomEvenement { get; set; }


        [JsonProperty("location")]
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Localisation requise")]
        public string Location { get; set; }


        [JsonProperty("date")]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Date requise")]
        public string Date { get; set; }


        [JsonProperty("idOrganisateur")]
        public int IdOrganisateur { get; set; }


        [JsonProperty("description")]
        [Display(Name = "Description")]
        [StringLength(200)]
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
