"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/evenementsHub").build();

connection.on("actionRafraichir", function (message) {
    if (message != "") {
        var conteneur = document.getElementById("conteneurEvens");
        conteneur.innerHTML = "";
        document.getElementById("loading").innerHTML = "";
        if (message == "Erreur de connexion") {
            conteneur.innerHTML = message;
        } else {
            var listeEvenement = JSON.parse(message);
            for (var e of listeEvenement) {
                conteneur.appendChild(genererCarteEvenement(e));
            }
        }
    }
});

connection.start().then(window.setInterval(function () {
    connection.invoke("RafraichirEvenementsRecents").catch(function (err) {
        return console.error(err.toString());
    })
}, 1500)).catch(function (err) {
    return console.error(err.toString());
});

//Fonction pour générer une carte par événement
function genererCarteEvenement(e) {

    // Création de l'élément carte avec son image
    var carte = document.createElement("div");
    carte.className = "card";
    carte.innerHTML += '<img class="card-img-top" src="' + e.LienImage + '" alt="Image">';

    // Création du header avec le nom
    var header = document.createElement("div");
    header.className = "card-header";
    header.innerHTML = "<h3>" + e.nomEvenement + "</h3>";

    // Corps de la carte
    var body = document.createElement("div");
    body.className = "card-body";

    // Liste des détails de l'événement
    var elements = document.createElement("ul");

    var location = document.createElement("li");
    location.textContent = `Location: ${e.location}`;
    elements.appendChild(location);

    var date = document.createElement("li");
    date.textContent = `Date: ${e.date}`;
    elements.appendChild(date);

    var organisateur = document.createElement("li");
    organisateur.textContent = `Organisé par: ${e.Organisateur.nomUtilisateur}`;
    elements.appendChild(organisateur);

    var description = document.createElement("li");
    description.textContent = `Description: ${e.description}`;
    elements.appendChild(description);

    var description2 = document.createElement("li");
    description2.textContent = `allo`;
    elements.appendChild(description2);

    // Attachement des éléments de la carte à celle-ci
    body.appendChild(elements);
    carte.appendChild(header);
    carte.appendChild(body);

    return carte;
}