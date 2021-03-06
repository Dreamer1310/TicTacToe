var ServerLayer;
(function (ServerLayer) {
    var GameConnectionClient = /** @class */ (function () {
        function GameConnectionClient() {
            this.callbacks = {};
        }
        GameConnectionClient.prototype.Disconnect = function (callback) {
            if (this.callbacks["Disconnect"])
                throw "Disconnect is already bound!";
            this.callbacks["Disconnect"] = function () { return callback(); };
            return this;
        };
        GameConnectionClient.prototype.AskMove = function (callback) {
            if (this.callbacks["AskMove"])
                throw "AskMove is already bound!";
            this.callbacks["AskMove"] = function () { return callback(); };
            return this;
        };
        GameConnectionClient.prototype.WaitingFor = function (callback) {
            if (this.callbacks["WaitingFor"])
                throw "WaitingFor is already bound!";
            this.callbacks["WaitingFor"] = function (player) { return callback(player); };
            return this;
        };
        GameConnectionClient.prototype.WaitingToJoin = function (callback) {
            if (this.callbacks["WaitingToJoin"])
                throw "WaitingToJoin is already bound!";
            this.callbacks["WaitingToJoin"] = function (players) { return callback(players); };
            return this;
        };
        GameConnectionClient.prototype.GameState = function (callback) {
            if (this.callbacks["GameState"])
                throw "GameState is already bound!";
            this.callbacks["GameState"] = function (state) { return callback(state); };
            return this;
        };
        GameConnectionClient.prototype.GameStarted = function (callback) {
            if (this.callbacks["GameStarted"])
                throw "GameStarted is already bound!";
            this.callbacks["GameStarted"] = function () { return callback(); };
            return this;
        };
        GameConnectionClient.prototype.RoundStarted = function (callback) {
            if (this.callbacks["RoundStarted"])
                throw "RoundStarted is already bound!";
            this.callbacks["RoundStarted"] = function (roundId) { return callback(roundId); };
            return this;
        };
        GameConnectionClient.prototype.RoundFinished = function (callback) {
            if (this.callbacks["RoundFinished"])
                throw "RoundFinished is already bound!";
            this.callbacks["RoundFinished"] = function (roundInfo) { return callback(roundInfo); };
            return this;
        };
        GameConnectionClient.prototype.GameFinished = function (callback) {
            if (this.callbacks["GameFinished"])
                throw "GameFinished is already bound!";
            this.callbacks["GameFinished"] = function (finalInfo) { return callback(finalInfo); };
            return this;
        };
        GameConnectionClient.prototype.OperationError = function (callback) {
            if (this.callbacks["OperationError"])
                throw "OperationError is already bound!";
            this.callbacks["OperationError"] = function () { return callback(); };
            return this;
        };
        GameConnectionClient.prototype.PlayerMadeMove = function (callback) {
            if (this.callbacks["PlayerMadeMove"])
                throw "PlayerMadeMove is already bound!";
            this.callbacks["PlayerMadeMove"] = function (move) { return callback(move); };
            return this;
        };
        GameConnectionClient.prototype.PlayerInfo = function (callback) {
            if (this.callbacks["PlayerInfo"])
                throw "PlayerInfo is already bound!";
            this.callbacks["PlayerInfo"] = function (playerInfo) { return callback(playerInfo); };
            return this;
        };
        return GameConnectionClient;
    }());
    ;
    var GameConnection = /** @class */ (function () {
        function GameConnection() {
            var _this = this;
            this.client = new GameConnectionClient();
            this.server = {
                connection: {},
                Join: function () {
                    _this.connection.send("Join");
                },
                MakeMove: function (point) {
                    _this.connection.send("MakeMove", point);
                }
            };
            this.stop = function () {
                return _this.connection.stop();
            };
            var address = window.signalrAddress;
            if (!address) {
                throw new Error("signalrAddress not found!");
            }
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(address + "game", {
                transport: signalR.HttpTransportType.WebSockets,
            })
                .build();
            this.connection.on("Disconnect", function () {
                if (_this.client.callbacks["Disconnect"]) {
                    _this.client.callbacks["Disconnect"]();
                }
                else {
                    throw "Disconnect implementation could not found!";
                }
            });
            this.connection.on("AskMove", function () {
                if (_this.client.callbacks["AskMove"]) {
                    _this.client.callbacks["AskMove"]();
                }
                else {
                    throw "AskMove implementation could not found!";
                }
            });
            this.connection.on("WaitingFor", function (player) {
                if (_this.client.callbacks["WaitingFor"]) {
                    _this.client.callbacks["WaitingFor"](player);
                }
                else {
                    throw "WaitingFor implementation could not found!";
                }
            });
            this.connection.on("WaitingToJoin", function (players) {
                if (_this.client.callbacks["WaitingToJoin"]) {
                    _this.client.callbacks["WaitingToJoin"](players);
                }
                else {
                    throw "WaitingToJoin implementation could not found!";
                }
            });
            this.connection.on("GameState", function (state) {
                if (_this.client.callbacks["GameState"]) {
                    _this.client.callbacks["GameState"](state);
                }
                else {
                    throw "GameState implementation could not found!";
                }
            });
            this.connection.on("GameStarted", function () {
                if (_this.client.callbacks["GameStarted"]) {
                    _this.client.callbacks["GameStarted"]();
                }
                else {
                    throw "GameStarted implementation could not found!";
                }
            });
            this.connection.on("RoundStarted", function (roundId) {
                if (_this.client.callbacks["RoundStarted"]) {
                    _this.client.callbacks["RoundStarted"](roundId);
                }
                else {
                    throw "RoundStarted implementation could not found!";
                }
            });
            this.connection.on("RoundFinished", function (roundInfo) {
                if (_this.client.callbacks["RoundFinished"]) {
                    _this.client.callbacks["RoundFinished"](roundInfo);
                }
                else {
                    throw "RoundFinished implementation could not found!";
                }
            });
            this.connection.on("GameFinished", function (finalInfo) {
                if (_this.client.callbacks["GameFinished"]) {
                    _this.client.callbacks["GameFinished"](finalInfo);
                }
                else {
                    throw "GameFinished implementation could not found!";
                }
            });
            this.connection.on("OperationError", function () {
                if (_this.client.callbacks["OperationError"]) {
                    _this.client.callbacks["OperationError"]();
                }
                else {
                    throw "OperationError implementation could not found!";
                }
            });
            this.connection.on("PlayerMadeMove", function (move) {
                if (_this.client.callbacks["PlayerMadeMove"]) {
                    _this.client.callbacks["PlayerMadeMove"](move);
                }
                else {
                    throw "PlayerMadeMove implementation could not found!";
                }
            });
            this.connection.on("PlayerInfo", function (playerInfo) {
                if (_this.client.callbacks["PlayerInfo"]) {
                    _this.client.callbacks["PlayerInfo"](playerInfo);
                }
                else {
                    throw "PlayerInfo implementation could not found!";
                }
            });
            this.server.connection = this.connection;
        }
        GameConnection.prototype.start = function () {
            this.checkClientBindings();
            return this.connection.start();
        };
        ;
        GameConnection.prototype.checkClientBindings = function () {
            if (!this.client.callbacks["Disconnect"])
                throw new Error("Disconnect not implemented");
            if (!this.client.callbacks["AskMove"])
                throw new Error("AskMove not implemented");
            if (!this.client.callbacks["WaitingFor"])
                throw new Error("WaitingFor not implemented");
            if (!this.client.callbacks["WaitingToJoin"])
                throw new Error("WaitingToJoin not implemented");
            if (!this.client.callbacks["GameState"])
                throw new Error("GameState not implemented");
            if (!this.client.callbacks["GameStarted"])
                throw new Error("GameStarted not implemented");
            if (!this.client.callbacks["RoundStarted"])
                throw new Error("RoundStarted not implemented");
            if (!this.client.callbacks["RoundFinished"])
                throw new Error("RoundFinished not implemented");
            if (!this.client.callbacks["GameFinished"])
                throw new Error("GameFinished not implemented");
            if (!this.client.callbacks["OperationError"])
                throw new Error("OperationError not implemented");
            if (!this.client.callbacks["PlayerMadeMove"])
                throw new Error("PlayerMadeMove not implemented");
            if (!this.client.callbacks["PlayerInfo"])
                throw new Error("PlayerInfo not implemented");
        };
        return GameConnection;
    }());
    ServerLayer.GameConnection = GameConnection;
    ;
    var LobbyConnectionClient = /** @class */ (function () {
        function LobbyConnectionClient() {
            this.callbacks = {};
        }
        LobbyConnectionClient.prototype.QueueData = function (callback) {
            if (this.callbacks["QueueData"])
                throw "QueueData is already bound!";
            this.callbacks["QueueData"] = function (queueDto) { return callback(queueDto); };
            return this;
        };
        LobbyConnectionClient.prototype.YouLeftQueue = function (callback) {
            if (this.callbacks["YouLeftQueue"])
                throw "YouLeftQueue is already bound!";
            this.callbacks["YouLeftQueue"] = function () { return callback(); };
            return this;
        };
        LobbyConnectionClient.prototype.YouSetOnQueue = function (callback) {
            if (this.callbacks["YouSetOnQueue"])
                throw "YouSetOnQueue is already bound!";
            this.callbacks["YouSetOnQueue"] = function (queueId) { return callback(queueId); };
            return this;
        };
        LobbyConnectionClient.prototype.CanSeat = function (callback) {
            if (this.callbacks["CanSeat"])
                throw "CanSeat is already bound!";
            this.callbacks["CanSeat"] = function (canSeat) { return callback(canSeat); };
            return this;
        };
        LobbyConnectionClient.prototype.StartGame = function (callback) {
            if (this.callbacks["StartGame"])
                throw "StartGame is already bound!";
            this.callbacks["StartGame"] = function (gameId) { return callback(gameId); };
            return this;
        };
        LobbyConnectionClient.prototype.Players = function (callback) {
            if (this.callbacks["Players"])
                throw "Players is already bound!";
            this.callbacks["Players"] = function (players) { return callback(players); };
            return this;
        };
        LobbyConnectionClient.prototype.Disconnect = function (callback) {
            if (this.callbacks["Disconnect"])
                throw "Disconnect is already bound!";
            this.callbacks["Disconnect"] = function () { return callback(); };
            return this;
        };
        return LobbyConnectionClient;
    }());
    ;
    var LobbyConnection = /** @class */ (function () {
        function LobbyConnection() {
            var _this = this;
            this.client = new LobbyConnectionClient();
            this.server = {
                connection: {},
                Seat: function (queueId) {
                    _this.connection.send("Seat", queueId);
                },
                SeatOut: function (queueId) {
                    _this.connection.send("SeatOut", queueId);
                },
                TestDto: function (demo) {
                    _this.connection.send("TestDto", demo);
                },
                TestDictionary: function (queues) {
                    _this.connection.send("TestDictionary", queues);
                },
                TestList: function (demos) {
                    _this.connection.send("TestList", demos);
                }
            };
            this.stop = function () {
                return _this.connection.stop();
            };
            var address = window.signalrAddress;
            if (!address) {
                throw new Error("signalrAddress not found!");
            }
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(address + "lobby", {
                transport: signalR.HttpTransportType.WebSockets
            })
                .build();
            this.connection.on("QueueData", function (queueDto) {
                if (_this.client.callbacks["QueueData"]) {
                    _this.client.callbacks["QueueData"](queueDto);
                }
                else {
                    throw "QueueData implementation could not found!";
                }
            });
            this.connection.on("YouLeftQueue", function () {
                if (_this.client.callbacks["YouLeftQueue"]) {
                    _this.client.callbacks["YouLeftQueue"]();
                }
                else {
                    throw "YouLeftQueue implementation could not found!";
                }
            });
            this.connection.on("YouSetOnQueue", function (queueId) {
                if (_this.client.callbacks["YouSetOnQueue"]) {
                    _this.client.callbacks["YouSetOnQueue"](queueId);
                }
                else {
                    throw "YouSetOnQueue implementation could not found!";
                }
            });
            this.connection.on("CanSeat", function (canSeat) {
                if (_this.client.callbacks["CanSeat"]) {
                    _this.client.callbacks["CanSeat"](canSeat);
                }
                else {
                    throw "CanSeat implementation could not found!";
                }
            });
            this.connection.on("StartGame", function (gameId) {
                if (_this.client.callbacks["StartGame"]) {
                    _this.client.callbacks["StartGame"](gameId);
                }
                else {
                    throw "StartGame implementation could not found!";
                }
            });
            this.connection.on("Players", function (players) {
                if (_this.client.callbacks["Players"]) {
                    _this.client.callbacks["Players"](players);
                }
                else {
                    throw "Players implementation could not found!";
                }
            });
            this.connection.on("Disconnect", function () {
                if (_this.client.callbacks["Disconnect"]) {
                    _this.client.callbacks["Disconnect"]();
                }
                else {
                    throw "Disconnect implementation could not found!";
                }
            });
            this.server.connection = this.connection;
        }
        LobbyConnection.prototype.start = function () {
            this.checkClientBindings();
            return this.connection.start();
        };
        ;
        LobbyConnection.prototype.checkClientBindings = function () {
            if (!this.client.callbacks["QueueData"])
                throw new Error("QueueData not implemented");
            if (!this.client.callbacks["YouLeftQueue"])
                throw new Error("YouLeftQueue not implemented");
            if (!this.client.callbacks["YouSetOnQueue"])
                throw new Error("YouSetOnQueue not implemented");
            if (!this.client.callbacks["CanSeat"])
                throw new Error("CanSeat not implemented");
            if (!this.client.callbacks["StartGame"])
                throw new Error("StartGame not implemented");
            if (!this.client.callbacks["Players"])
                throw new Error("Players not implemented");
            if (!this.client.callbacks["Disconnect"])
                throw new Error("Disconnect not implemented");
        };
        return LobbyConnection;
    }());
    ServerLayer.LobbyConnection = LobbyConnection;
    ;
    var RoundStatus;
    (function (RoundStatus) {
        RoundStatus[RoundStatus["NotFinished"] = 0] = "NotFinished";
        RoundStatus[RoundStatus["Finished"] = 1] = "Finished";
    })(RoundStatus = ServerLayer.RoundStatus || (ServerLayer.RoundStatus = {}));
    var GameFigures;
    (function (GameFigures) {
        GameFigures[GameFigures["None"] = 0] = "None";
        GameFigures[GameFigures["Cross"] = 1] = "Cross";
        GameFigures[GameFigures["Circle"] = 2] = "Circle";
    })(GameFigures = ServerLayer.GameFigures || (ServerLayer.GameFigures = {}));
    var RoundFinishReasons;
    (function (RoundFinishReasons) {
        RoundFinishReasons[RoundFinishReasons["PlayerTimedOut"] = 0] = "PlayerTimedOut";
        RoundFinishReasons[RoundFinishReasons["PlayerWon"] = 1] = "PlayerWon";
        RoundFinishReasons[RoundFinishReasons["Tie"] = 2] = "Tie";
    })(RoundFinishReasons = ServerLayer.RoundFinishReasons || (ServerLayer.RoundFinishReasons = {}));
    var GameFinishReasons;
    (function (GameFinishReasons) {
        GameFinishReasons[GameFinishReasons["TillPointsReach"] = 0] = "TillPointsReach";
        GameFinishReasons[GameFinishReasons["PlayerTimedOut"] = 1] = "PlayerTimedOut";
        GameFinishReasons[GameFinishReasons["PlayerResigned"] = 2] = "PlayerResigned";
    })(GameFinishReasons = ServerLayer.GameFinishReasons || (ServerLayer.GameFinishReasons = {}));
})(ServerLayer || (ServerLayer = {}));
//# sourceMappingURL=ServerLayer.js.map