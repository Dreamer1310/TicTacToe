/// <reference path="../lib/angular/angular.d.ts" />
(function () {
    var lobbyConnection = new ServerLayer.LobbyConnection();
    var lobbyApp = angular.module("lobbyApp", []);
    lobbyApp.controller("lobbyController", function ($scope) {
        var button = $("#someID");
        $.extend($scope, {
            click: function () { return console.log(button); },
            seat: function (queueId) {
                lobbyConnection.server.Seat(queueId);
            },
            seatOut: function (queueId) {
                lobbyConnection.server.SeatOut(queueId);
            },
            testDto: function () {
                lobbyConnection.server.TestDto({ Name: "luka", Age: 21 });
            },
            testList: function () {
                lobbyConnection.server.TestList([{ Name: "luka", Age: 21 }, { Name: "ana", Age: 22 }, { Name: "temo", Age: 20 }]);
            },
            testDictionary: function () {
                lobbyConnection.server.TestDictionary({
                    "1": { ID: 1, Name: "3x3", PlayerCount: 1, BoadrSize: 3, Till: 1 },
                    "2": { ID: 2, Name: "5x5", PlayerCount: 1, BoadrSize: 5, Till: 3 }
                });
            }
        });
        lobbyConnection.client
            .CanSeat(function (canSeat) {
            console.log(canSeat);
        })
            .Disconnect(function () {
            console.log("Disconnect");
        })
            .QueueData(function (queues, a) {
            $scope.queues = queues;
            console.log(queues, a);
        })
            .Started(function (msg) {
            console.log(msg);
        })
            .StartGame(function (gameId) {
            console.log(gameId);
        })
            .Stopped(function (msg) {
            console.log(msg);
        })
            .YouLeftQueue(function () {
            console.log("You Left Queue");
        })
            .YouSetOnQueue(function (queueId) {
            console.log(queueId);
        })
            .Players(function (players) {
            console.log(players);
        });
        lobbyConnection.start().then(function () { console.log('არის კონტაქტი!'); });
    });
})();
//# sourceMappingURL=Lobby.js.map