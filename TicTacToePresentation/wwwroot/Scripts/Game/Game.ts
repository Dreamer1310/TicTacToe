(() => {
    var gameConnection: ServerLayer.GameConnection = new ServerLayer.GameConnection();

    var gameApp = angular.module("gameApp", []);

    gameApp.controller("gameController",
        ($scope: IGameScope) => {

            $.extend($scope, {
                makeMove: () => {
                    var x: number = +$('#xInput').val();
                    var y: number = +$('#yInput').val();
                    gameConnection.server.MakeMove(x, y);
                }
            })

            gameConnection.client
                .AskMove(() => {

                })
                .WaitingFor((player: ServerLayer.PlayerDto) => {
                    console.log(player);
                })
                .WaitingToJoin((players: ServerLayer.PlayerDto[]) => {
                    console.log(players);
                })
                .GameState((state: ServerLayer.StateDto) => {
                    console.log(state);
                })
                .GameStarted(() => {

                })
                .RoundStarted((roundId: number) => {
                    console.log(roundId);
                })
                .RoundFinished((roundInfo: ServerLayer.RoundFinishedDto) => {
                    console.log(roundInfo);
                })
                .GameFinished((finalInfo: ServerLayer.GameFinishedDto) => {
                    console.log(finalInfo);
                })
                .OperationError(() => {

                })
                .Disconnect(() => {

                })
                .PlayerMadeMove((move: ServerLayer.MoveDto) => {
                    console.log(move);
                });

            gameConnection.start().then(() => { gameConnection.server.Join() });
        }
    );
})();

interface IGameScope extends ng.IScope { }