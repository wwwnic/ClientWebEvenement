"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/evenementsHub").build();


connection.on("refreshEvenements", function (message) {
    //var elementTable = document.createElement("tr");
    document.getElementById("tableEvenements").innerHTML = message;
    //var listeEvenements = JSON.parse(message);
    //for (var e in listeEvenements) {
        //elementTable.innerHTML = e.nomEvenement;
        //document.getElementById("tableEvenements").appendChild(elementTable);
    //}
});



connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("refreshButton").addEventListener("click", function (event) {
    var message = "wow";
    connection.invoke("refreshEvenements",message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});