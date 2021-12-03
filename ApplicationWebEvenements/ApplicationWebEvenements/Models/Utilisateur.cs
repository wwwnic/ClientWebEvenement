using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace ApplicationWebEvenements.Models
{
    public class Utilisateur
    {
        public Utilisateur()
        {
            IdUtilisateur = 0;
            NomUtilisateur = "";
            MotDePasse = "";
            Courriel = "";
            Telephone = "";
            DateCreation = "1900-12-02T18:12:00.094Z";
        }

        public Utilisateur(string nomUtilisateur, string motDePasse)
        {
            IdUtilisateur = 0;
            NomUtilisateur = nomUtilisateur;
            MotDePasse = motDePasse;
            Courriel = "";
            Telephone = "";
            DateCreation = "1900-12-02T18:12:00.094Z";
        }

        public Utilisateur(string nomUtilisateur, string motDePasse, string courriel, string telephone)
        {
            IdUtilisateur = 0;
            NomUtilisateur = nomUtilisateur;
            MotDePasse = motDePasse;
            Courriel = courriel;
            Telephone = telephone;
            DateCreation = "1900-12-02T18:12:00.094Z";
        }

        [JsonPropertyName("idUtilisateur")]
        public int IdUtilisateur { get; }
        [JsonPropertyName("nomUtilisateur")]
        public string NomUtilisateur { get; set; }
        [JsonPropertyName("motDePasse")]
        public string MotDePasse { get; set; }
        [JsonPropertyName("courriel")]
        public string Courriel { get; set; }
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; }
        [JsonPropertyName("dateCreation")]
        public string DateCreation { get; set; }
    } 
}
