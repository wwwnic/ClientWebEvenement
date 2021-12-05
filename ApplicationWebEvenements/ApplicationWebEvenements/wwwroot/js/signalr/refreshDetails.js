"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/evenementsHub").build();

connection.on("rafraichirParticipants", function (message) {
    if (message != "") {
        var conteneur = document.getElementById("conteneurParticipants");
        conteneur.innerHTML = "";
        if (message == "Erreur de connexion") {
            document.getElementById("texteErreur").innerHTML = message;
        } else {
            var listeParticipants = JSON.parse(message);
            for (var u of listeParticipants) {
                conteneur.appendChild(genererParticipant(u));
            }
            document.getElementById("nbParticipants").innerHTML = 'Participants (' + listeParticipants.length + ')';
        }
    }
});

connection.on("rafraichirCommentaires", function (message) {
    var conteneur = document.getElementById("conteneurCommentaires");
    conteneur.innerHTML = "";
    if (message != "") {
        var listeCommentaires = JSON.parse(message);
        for (var c of listeCommentaires) {
            conteneur.appendChild(genererCommentaire(c));
        }
    } else {
        conteneur.innerHTML = '<h3>Aucun commentaire.</h3>'
    }
});

connection.on("erreurParticipation", function () {
    var erreur = document.getElementById("texteErreur");
    erreur.innerHTML = "Erreur de connexion";
});

connection.start().then(window.setInterval(function () {
    connection.invoke("RafraichirDetails").catch(function (err) {
        return console.error(err.toString());
    })
}, 1500)).catch(function (err) {
    return console.error(err.toString());
});

//Fonction pour générer une carte par événement
function genererParticipant(u) {
    var participant = document.createElement("li");
    participant.innerHTML = '<li><img width=50 height=50 src="' + u.LienImage + '" alt="Image" /><strong>' + u.nomUtilisateur + '</strong></li>';
    return participant;
}

function genererCommentaire(c) {
    // Création de l'élément carte
    var carte = document.createElement("div");
    carte.className = "card";

    // Création du header avec le nom
    var header = document.createElement("div");
    header.className = "card-header";
    header.innerHTML = '<h3><img width=50 height=50 src="' + c.Utilisateur.LienImage + '" alt="Image" />' + c.Utilisateur.nomUtilisateur + ' - ' + c.date + '</h3>';

    // Corps de la carte
    var body = document.createElement("div");
    body.className = "card-body";
    body.innerHTML = '<p>' + c.texte + '</p>';

    // Attachement des éléments de la carte à celle-ci
    carte.appendChild(header);
    carte.appendChild(body);

    return carte;
}

function participation(estParticipant) {
    connection.invoke("ModifierParticipation", estParticipant).catch(function (err) {
        return console.error(err.toString());
    });
    var btn = document.getElementById("btnParticipation");
    if (estParticipant) {
        btn.className = "btn btn-primary figma-mauve";
        btn.innerHTML = "Je participe";
        btn.onclick = function () { participation(false); };
    }
    else {
        btn.className = "btn btn-danger";
        btn.innerHTML = "Je ne participe pas";
        btn.onclick = function () { participation(true); };
    }
}