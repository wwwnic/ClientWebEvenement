using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace ApplicationWebEvenements.Models
{
    public class Utilisateurevenement
    {
        [JsonProperty("idEvenement")]
        public int IdEvenement { get; set; }
        [JsonProperty("idUtilisateur")]
        public int IdUtilisateur { get; set; }
    }
}
