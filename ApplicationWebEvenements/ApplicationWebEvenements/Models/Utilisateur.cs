
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
        [Required(ErrorMessage = "Nom utilisateur requis")]
        [Display(Name = "Nom utilisateur")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Le nom utilisateur doit être inclusivement entre 4 et 20 caractères")]
        [DataType(DataType.Text, ErrorMessage = "Nom utilisateur invalide")]
        [RegularExpression(@"^(?=[a-zA-Z0-9._]{0,21}$)(?!.*[_.]{2})[^_.].*[^_.]$", ErrorMessage = "Nom d'utilisateur interdit")]
        public string NomUtilisateur { get; set; }

        [JsonProperty("motDePasse")]
        [Required(ErrorMessage = "Mot de passe requis")]
        [Display(Name = "Mot de passe")]
        [StringLength(24, MinimumLength = 4, ErrorMessage = "Le mot de passe doit être inclusivement entre 4 et 24 caractères")]
        [DataType(DataType.Password, ErrorMessage = "Mot de passe non autorisé")]
        [RegularExpression(@"[a-zA-Z0-9\\!\\@\\#\\$\\*\\&\(\\$\\?\\%\\^\\'\\|\\)]{0,25}", ErrorMessage = "Mot de passe non autorisé : A-Z, 0-9 et certain symbole uniquement")]
        public string MotDePasse { get; set; }


        [JsonProperty("courriel")]
        [Required(ErrorMessage = "Courriel requis")]
        [Display(Name = "Courriel")]
        [StringLength(30, ErrorMessage = "Le courriel ne peut pas dépasser 30 caractères")]
        [EmailAddress(ErrorMessage = "Adresse courriel invalide")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Adresse courriel invalide")]
        public string Courriel { get; set; }


        [JsonProperty("telephone")]
        [Required(ErrorMessage = "Téléphone requis")]
        [Display(Name = "Téléphone")]
        [StringLength(12, ErrorMessage = "Le téléphone ne peut pas dépasser 12 chiffres")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Numéro de téléphone invalide")]
        public string Telephone { get; set; }

        [JsonProperty("dateCreation")]
        [Display(Name = "Date de création")]
        public string DateCreation { get; set; }


        public string GetTéléphoneFormaté()
        {
            if (Telephone.Length == 9)
            {
                var partie1 = Telephone.Substring(0, 3);
                var partie2 = Telephone.Substring(3, 3);
                var partie3 = Telephone.Substring(6, 4);
                return $"{partie1}-{partie2}-{partie3}";
            }
            else
            {
                return Telephone;
            }
        }

        public string GetDateFormatée()
        {
            var dateHeure = DateCreation.Split("T");
            return dateHeure[0];
        }

        public string LienImage { get; set; }
    }
}
