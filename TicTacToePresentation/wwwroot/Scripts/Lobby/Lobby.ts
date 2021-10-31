/// <reference path="../lib/angular/angular.d.ts" />

(() => {

    var lobbyConnection: ServerLayer.LobbyConnection = new ServerLayer.LobbyConnection();

    var lobbyApp = angular.module("lobbyApp", []);

    lobbyApp.controller("lobbyController",
        ($scope: ILobbyScope) => {
            var button = $("#someID");
            $scope.queues = [];

            $.extend($scope, {
                click: () => console.log(button),

                seat: (queueId) => {
                    lobbyConnection.server.Seat(queueId);
                },
                seatOut: (queueId) => {
                    lobbyConnection.server.SeatOut(queueId);
                },
                testDto: () => {
                    lobbyConnection.server.TestDto(<ServerLayer.DemoDto>{Name: "luka", Age: 21});
                },
                testList: () => {
                    lobbyConnection.server.TestList([<ServerLayer.DemoDto>{ Name: "luka", Age: 21 }, <ServerLayer.DemoDto>{ Name: "ana", Age: 22 }, <ServerLayer.DemoDto>{ Name: "temo", Age: 20 }]);
                },
                testDictionary: () => {
                    lobbyConnection.server.TestDictionary({
                        "1": <ServerLayer.QueueDto>{ ID: 1, Name: "3x3", PlayerCount: 1, BoadrSize: 3, Till: 1 },
                        "2": <ServerLayer.QueueDto>{ ID: 2, Name: "5x5", PlayerCount: 1, BoadrSize: 5, Till: 3 }
                    });
                }
            });

            lobbyConnection.client
                .CanSeat((canSeat: boolean) => {
                    console.log(canSeat);
                })
                .Disconnect(() => {
                    console.log("Disconnect")
                })
                .QueueData((queues: ServerLayer.QueueDto[]) => {
                    $scope.queues = queues;
                    $scope.$apply();
                    console.log($scope.queues);
                })
                .Started((msg: string) => {
                    console.log(msg);
                })
                .StartGame((gameId: number) => {
                    window.location.href = window.location.origin + (<any>window).gameUrl;

                    console.log(window.location.origin);
                })
                .Stopped((msg: string) => {
                    console.log(msg);
                })
                .YouLeftQueue(() => {
                    console.log("You Left Queue");
                })
                .YouSetOnQueue((queueId: number) => {
                    console.log(queueId);
                })
                .Players((players: { [Id: string]: ServerLayer.PlayerDto }) => {
                    console.log(players);
                });

            lobbyConnection.start().then(() => { console.log('არის კონტაქტი!') });
        });
})();

interface ILobbyScope extends ng.IScope {}