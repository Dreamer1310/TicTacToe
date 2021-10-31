(function () {
    var gameConnection = new ServerLayer.GameConnection();
    var gameApp = angular.module("gameApp", []);
    gameApp.controller("gameController", function ($scope) {
        $.extend($scope, {
            makeMove: function () {
                var x = +$('#xInput').val();
                var y = +$('#yInput').val();
                gameConnection.server.MakeMove(x, y);
            }
        });
        gameConnection.client
            .AskMove(function () {
        })
            .WaitingFor(function (player) {
            console.log(player);
        })
            .WaitingToJoin(function (players) {
            console.log(players);
        })
            .GameState(function (state) {
            console.log(state);
        })
            .GameStarted(function () {
        })
            .RoundStarted(function (roundId) {
            console.log(roundId);
        })
            .RoundFinished(function (roundInfo) {
            console.log(roundInfo);
        })
            .GameFinished(function (finalInfo) {
            console.log(finalInfo);
        })
            .OperationError(function () {
        })
            .Disconnect(function () {
        })
            .PlayerMadeMove(function (move) {
            console.log(move);
        });
        gameConnection.start().then(function () { gameConnection.server.Join(); });
    });
})();
//# sourceMappingURL=Game.js.map