module ServerLayer {
	class GameConnectionClient {
		callbacks: any = {};
	
		Disconnect(callback: () => void): GameConnectionClient {
		    if (this.callbacks["Disconnect"]) throw "Disconnect is already bound!";
		    this.callbacks["Disconnect"] = () => callback();
		    return this;
		}
		
		AskMove(callback: () => void): GameConnectionClient {
		    if (this.callbacks["AskMove"]) throw "AskMove is already bound!";
		    this.callbacks["AskMove"] = () => callback();
		    return this;
		}
		
		WaitingFor(callback: (player: PlayerDto) => void): GameConnectionClient {
		    if (this.callbacks["WaitingFor"]) throw "WaitingFor is already bound!";
		    this.callbacks["WaitingFor"] = (player: PlayerDto) => callback(player);
		    return this;
		}
		
		WaitingToJoin(callback: (players: PlayerDto[]) => void): GameConnectionClient {
		    if (this.callbacks["WaitingToJoin"]) throw "WaitingToJoin is already bound!";
		    this.callbacks["WaitingToJoin"] = (players: PlayerDto[]) => callback(players);
		    return this;
		}
		
		GameState(callback: (state: StateDto) => void): GameConnectionClient {
		    if (this.callbacks["GameState"]) throw "GameState is already bound!";
		    this.callbacks["GameState"] = (state: StateDto) => callback(state);
		    return this;
		}
		
		GameStarted(callback: () => void): GameConnectionClient {
		    if (this.callbacks["GameStarted"]) throw "GameStarted is already bound!";
		    this.callbacks["GameStarted"] = () => callback();
		    return this;
		}
		
		RoundStarted(callback: (roundId: number) => void): GameConnectionClient {
		    if (this.callbacks["RoundStarted"]) throw "RoundStarted is already bound!";
		    this.callbacks["RoundStarted"] = (roundId: number) => callback(roundId);
		    return this;
		}
		
		RoundFinished(callback: (roundInfo: RoundFinishedDto) => void): GameConnectionClient {
		    if (this.callbacks["RoundFinished"]) throw "RoundFinished is already bound!";
		    this.callbacks["RoundFinished"] = (roundInfo: RoundFinishedDto) => callback(roundInfo);
		    return this;
		}
		
		GameFinished(callback: (finalInfo: GameFinishedDto) => void): GameConnectionClient {
		    if (this.callbacks["GameFinished"]) throw "GameFinished is already bound!";
		    this.callbacks["GameFinished"] = (finalInfo: GameFinishedDto) => callback(finalInfo);
		    return this;
		}
		
		OperationError(callback: () => void): GameConnectionClient {
		    if (this.callbacks["OperationError"]) throw "OperationError is already bound!";
		    this.callbacks["OperationError"] = () => callback();
		    return this;
		}
		
		PlayerMadeMove(callback: (move: MoveDto) => void): GameConnectionClient {
		    if (this.callbacks["PlayerMadeMove"]) throw "PlayerMadeMove is already bound!";
		    this.callbacks["PlayerMadeMove"] = (move: MoveDto) => callback(move);
		    return this;
		}
		
		PlayerInfo(callback: (playerInfo: PlayerDto) => void): GameConnectionClient {
		    if (this.callbacks["PlayerInfo"]) throw "PlayerInfo is already bound!";
		    this.callbacks["PlayerInfo"] = (playerInfo: PlayerDto) => callback(playerInfo);
		    return this;
		}
		
	
	};
	
	export class GameConnection {
	    connection: signalR.HubConnection;
	    client = new GameConnectionClient();
	
	    constructor() {
			var address = (<any>window).signalrAddress;
	
			if (!address) {
				throw new Error("signalrAddress not found!");
			}
	
			this.connection = new signalR.HubConnectionBuilder()
				.withUrl(address + "game", {
					transport: signalR.HttpTransportType.WebSockets,
				})
				.build();
	
			this.connection.on("Disconnect", () => {
			    if (this.client.callbacks["Disconnect"]) {
			        this.client.callbacks["Disconnect"]();
			    } else {
			        throw "Disconnect implementation could not found!";
			    }
			});
			this.connection.on("AskMove", () => {
			    if (this.client.callbacks["AskMove"]) {
			        this.client.callbacks["AskMove"]();
			    } else {
			        throw "AskMove implementation could not found!";
			    }
			});
			this.connection.on("WaitingFor", (player: PlayerDto) => {
			    if (this.client.callbacks["WaitingFor"]) {
			        this.client.callbacks["WaitingFor"](player);
			    } else {
			        throw "WaitingFor implementation could not found!";
			    }
			});
			this.connection.on("WaitingToJoin", (players: PlayerDto[]) => {
			    if (this.client.callbacks["WaitingToJoin"]) {
			        this.client.callbacks["WaitingToJoin"](players);
			    } else {
			        throw "WaitingToJoin implementation could not found!";
			    }
			});
			this.connection.on("GameState", (state: StateDto) => {
			    if (this.client.callbacks["GameState"]) {
			        this.client.callbacks["GameState"](state);
			    } else {
			        throw "GameState implementation could not found!";
			    }
			});
			this.connection.on("GameStarted", () => {
			    if (this.client.callbacks["GameStarted"]) {
			        this.client.callbacks["GameStarted"]();
			    } else {
			        throw "GameStarted implementation could not found!";
			    }
			});
			this.connection.on("RoundStarted", (roundId: number) => {
			    if (this.client.callbacks["RoundStarted"]) {
			        this.client.callbacks["RoundStarted"](roundId);
			    } else {
			        throw "RoundStarted implementation could not found!";
			    }
			});
			this.connection.on("RoundFinished", (roundInfo: RoundFinishedDto) => {
			    if (this.client.callbacks["RoundFinished"]) {
			        this.client.callbacks["RoundFinished"](roundInfo);
			    } else {
			        throw "RoundFinished implementation could not found!";
			    }
			});
			this.connection.on("GameFinished", (finalInfo: GameFinishedDto) => {
			    if (this.client.callbacks["GameFinished"]) {
			        this.client.callbacks["GameFinished"](finalInfo);
			    } else {
			        throw "GameFinished implementation could not found!";
			    }
			});
			this.connection.on("OperationError", () => {
			    if (this.client.callbacks["OperationError"]) {
			        this.client.callbacks["OperationError"]();
			    } else {
			        throw "OperationError implementation could not found!";
			    }
			});
			this.connection.on("PlayerMadeMove", (move: MoveDto) => {
			    if (this.client.callbacks["PlayerMadeMove"]) {
			        this.client.callbacks["PlayerMadeMove"](move);
			    } else {
			        throw "PlayerMadeMove implementation could not found!";
			    }
			});
			this.connection.on("PlayerInfo", (playerInfo: PlayerDto) => {
			    if (this.client.callbacks["PlayerInfo"]) {
			        this.client.callbacks["PlayerInfo"](playerInfo);
			    } else {
			        throw "PlayerInfo implementation could not found!";
			    }
			});
	
	
			this.server.connection = this.connection;
		}
		
		server = {
			connection: <signalR.HubConnection>{},
	
			
			Join: () => {
			    this.connection.send("Join");
			},
			MakeMove: (point: PointDto) => {
			    this.connection.send("MakeMove", point);
			}
		}
	
		start() {
			this.checkClientBindings();
			return this.connection.start();
		};
	
		stop = () => {
			return this.connection.stop();
		};
	
		checkClientBindings() {
			if (!this.client.callbacks["Disconnect"]) throw new Error("Disconnect not implemented");
			if (!this.client.callbacks["AskMove"]) throw new Error("AskMove not implemented");
			if (!this.client.callbacks["WaitingFor"]) throw new Error("WaitingFor not implemented");
			if (!this.client.callbacks["WaitingToJoin"]) throw new Error("WaitingToJoin not implemented");
			if (!this.client.callbacks["GameState"]) throw new Error("GameState not implemented");
			if (!this.client.callbacks["GameStarted"]) throw new Error("GameStarted not implemented");
			if (!this.client.callbacks["RoundStarted"]) throw new Error("RoundStarted not implemented");
			if (!this.client.callbacks["RoundFinished"]) throw new Error("RoundFinished not implemented");
			if (!this.client.callbacks["GameFinished"]) throw new Error("GameFinished not implemented");
			if (!this.client.callbacks["OperationError"]) throw new Error("OperationError not implemented");
			if (!this.client.callbacks["PlayerMadeMove"]) throw new Error("PlayerMadeMove not implemented");
			if (!this.client.callbacks["PlayerInfo"]) throw new Error("PlayerInfo not implemented");
	
		}
	};

	class LobbyConnectionClient {
		callbacks: any = {};
	
		QueueData(callback: (queueDto: QueueDto[]) => void): LobbyConnectionClient {
		    if (this.callbacks["QueueData"]) throw "QueueData is already bound!";
		    this.callbacks["QueueData"] = (queueDto: QueueDto[]) => callback(queueDto);
		    return this;
		}
		
		YouLeftQueue(callback: () => void): LobbyConnectionClient {
		    if (this.callbacks["YouLeftQueue"]) throw "YouLeftQueue is already bound!";
		    this.callbacks["YouLeftQueue"] = () => callback();
		    return this;
		}
		
		YouSetOnQueue(callback: (queueId: number) => void): LobbyConnectionClient {
		    if (this.callbacks["YouSetOnQueue"]) throw "YouSetOnQueue is already bound!";
		    this.callbacks["YouSetOnQueue"] = (queueId: number) => callback(queueId);
		    return this;
		}
		
		CanSeat(callback: (canSeat: boolean) => void): LobbyConnectionClient {
		    if (this.callbacks["CanSeat"]) throw "CanSeat is already bound!";
		    this.callbacks["CanSeat"] = (canSeat: boolean) => callback(canSeat);
		    return this;
		}
		
		StartGame(callback: (gameId: number) => void): LobbyConnectionClient {
		    if (this.callbacks["StartGame"]) throw "StartGame is already bound!";
		    this.callbacks["StartGame"] = (gameId: number) => callback(gameId);
		    return this;
		}
		
		Players(callback: (players: { [Id: string]: PlayerDto}) => void): LobbyConnectionClient {
		    if (this.callbacks["Players"]) throw "Players is already bound!";
		    this.callbacks["Players"] = (players: { [Id: string]: PlayerDto}) => callback(players);
		    return this;
		}
		
		Disconnect(callback: () => void): LobbyConnectionClient {
		    if (this.callbacks["Disconnect"]) throw "Disconnect is already bound!";
		    this.callbacks["Disconnect"] = () => callback();
		    return this;
		}
		
	
	};
	
	export class LobbyConnection {
	    connection: signalR.HubConnection;
	    client = new LobbyConnectionClient();
	
	    constructor() {
			var address = (<any>window).signalrAddress;
	
			if (!address) {
				throw new Error("signalrAddress not found!");
			}
	
			this.connection = new signalR.HubConnectionBuilder()
				.withUrl(address + "lobby", {
					transport: signalR.HttpTransportType.WebSockets
				})
				.build();
	
			this.connection.on("QueueData", (queueDto: QueueDto[]) => {
			    if (this.client.callbacks["QueueData"]) {
			        this.client.callbacks["QueueData"](queueDto);
			    } else {
			        throw "QueueData implementation could not found!";
			    }
			});
			this.connection.on("YouLeftQueue", () => {
			    if (this.client.callbacks["YouLeftQueue"]) {
			        this.client.callbacks["YouLeftQueue"]();
			    } else {
			        throw "YouLeftQueue implementation could not found!";
			    }
			});
			this.connection.on("YouSetOnQueue", (queueId: number) => {
			    if (this.client.callbacks["YouSetOnQueue"]) {
			        this.client.callbacks["YouSetOnQueue"](queueId);
			    } else {
			        throw "YouSetOnQueue implementation could not found!";
			    }
			});
			this.connection.on("CanSeat", (canSeat: boolean) => {
			    if (this.client.callbacks["CanSeat"]) {
			        this.client.callbacks["CanSeat"](canSeat);
			    } else {
			        throw "CanSeat implementation could not found!";
			    }
			});
			this.connection.on("StartGame", (gameId: number) => {
			    if (this.client.callbacks["StartGame"]) {
			        this.client.callbacks["StartGame"](gameId);
			    } else {
			        throw "StartGame implementation could not found!";
			    }
			});
			this.connection.on("Players", (players: { [Id: string]: PlayerDto}) => {
			    if (this.client.callbacks["Players"]) {
			        this.client.callbacks["Players"](players);
			    } else {
			        throw "Players implementation could not found!";
			    }
			});
			this.connection.on("Disconnect", () => {
			    if (this.client.callbacks["Disconnect"]) {
			        this.client.callbacks["Disconnect"]();
			    } else {
			        throw "Disconnect implementation could not found!";
			    }
			});
	
	
			this.server.connection = this.connection;
		}
		
		server = {
			connection: <signalR.HubConnection>{},
	
			
			Seat: (queueId: number) => {
			    this.connection.send("Seat", queueId);
			},
			SeatOut: (queueId: number) => {
			    this.connection.send("SeatOut", queueId);
			},
			TestDto: (demo: DemoDto) => {
			    this.connection.send("TestDto", demo);
			},
			TestDictionary: (queues: { [Id: string]: QueueDto}) => {
			    this.connection.send("TestDictionary", queues);
			},
			TestList: (demos: DemoDto[]) => {
			    this.connection.send("TestList", demos);
			}
		}
	
		start() {
			this.checkClientBindings();
			return this.connection.start();
		};
	
		stop = () => {
			return this.connection.stop();
		};
	
		checkClientBindings() {
			if (!this.client.callbacks["QueueData"]) throw new Error("QueueData not implemented");
			if (!this.client.callbacks["YouLeftQueue"]) throw new Error("YouLeftQueue not implemented");
			if (!this.client.callbacks["YouSetOnQueue"]) throw new Error("YouSetOnQueue not implemented");
			if (!this.client.callbacks["CanSeat"]) throw new Error("CanSeat not implemented");
			if (!this.client.callbacks["StartGame"]) throw new Error("StartGame not implemented");
			if (!this.client.callbacks["Players"]) throw new Error("Players not implemented");
			if (!this.client.callbacks["Disconnect"]) throw new Error("Disconnect not implemented");
	
		}
	};

    
	export enum RoundStatus
	{
		NotFinished = 0,
		Finished = 1,
	}
	export enum GameFigures
	{
		None = 0,
		Cross = 1,
		Circle = 2,
	}
	export enum RoundFinishReasons
	{
		PlayerTimedOut = 0,
		PlayerWon = 1,
		Tie = 2,
	}
	export enum GameFinishReasons
	{
		TillPointsReach = 0,
		PlayerTimedOut = 1,
		PlayerResigned = 2,
	}
	export interface PlayerDto
	{
		ID: string;
		Name: string;
	}
	export interface StateDto
	{
		GridSize: number;
		CurrentPlayerId: string;
		Players: PlayerDto[];
		CurrentRound: RoundDto;
	}
	export interface RoundDto
	{
		ID: number;
		Status: RoundStatus;
		GameBoard: CellDto[];
	}
	export interface CellDto
	{
		Point: PointDto;
		GameFigure: GameFigures;
	}
	export interface PointDto
	{
		x: number;
		y: number;
	}
	export interface RoundFinishedDto
	{
		RoundFinishReason: RoundFinishReasons;
		WinnerID: string;
		Scores: { [Id: string]: number};
		WinningLine: PointDto[];
	}
	export interface KeyCollection
	{
		Count: number;
	}
	export interface ValueCollection
	{
		Count: number;
	}
	export interface GameFinishedDto
	{
		GameFinishReason: GameFinishReasons;
		WinnerID: string;
		Scores: { [Id: string]: number};
	}
	export interface MoveDto
	{
		Player: PlayerDto;
		Point: PointDto;
		Figure: GameFigures;
	}
	export interface QueueDto
	{
		ID: number;
		Name: string;
		PlayerCount: number;
		BoadrSize: number;
		Till: number;
	}
	export interface DemoDto
	{
		Name: string;
		Age: number;
	}
}