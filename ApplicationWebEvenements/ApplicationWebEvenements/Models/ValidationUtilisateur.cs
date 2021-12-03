using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationWebEvenements.Models
{
    public class ValidationUtilisateur
    {

        [Required(ErrorMessage = "Nom utilisateur requis")]
        [Display(Name = "Nom utilisateur")]
        [StringLength(20, MinimumLength = 4)]
        [DataType(DataType.Text, ErrorMessage = "Nom utilisateur invalide")]
        public string NomUtilisateur { get; set; }


        [Required(ErrorMessage = "Mot de passe requis")]
        [Display(Name = "Mot de passe")]
        [StringLength(24, MinimumLength = 4)]
        [DataType(DataType.Password, ErrorMessage = "Mot de passe invalide")]
        public string MotDePasse { get; set; }


        [Required(ErrorMessage = "Courriel requis")]
        [Display(Name = "Courriel")]
        [StringLength(30)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel invalide")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Téléphone requis")]
        [Display(Name = "Téléphone")]
        [StringLength(12)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numéro de téléphone invalide")]

        public string Telephone { get; set; }
    }
}
