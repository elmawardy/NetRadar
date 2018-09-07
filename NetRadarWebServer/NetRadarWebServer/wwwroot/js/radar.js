"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/RadarHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    msg = JSON.parse(msg);
    var index = 0;
    var found = false;
    app.$children[0].$refs.dataTable.users.forEach(element => {
       if (element.name == user){
        app.$set(app.$children[0].$refs.dataTable.users,index,{'name':user,'send':msg.Send,'recieve':msg.Recieve,'state':msg.State,'interface':msg.Interface,'time':new Date().getTime()})
        found = true;
       }
       index++;
    });
    if (found==false) // if user is not found in the user list add him/her
        app.$children[0].$refs.dataTable.users.push({'name':user,'send':msg.Send,'recieve':msg.Recieve,'state':msg.State,'interface':msg.Interface,'time':new Date().getTime()});
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});