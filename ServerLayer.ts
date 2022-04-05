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
		
		WaitingFor(callback: (player: Object) => void): GameConnectionClient {
		    if (this.callbacks["WaitingFor"]) throw "WaitingFor is already bound!";
		    this.callbacks["WaitingFor"] = (player: Object) => callback(player);
		    return this;
		}
		
		WaitingToJoin(callback: (players: Object) => void): GameConnectionClient {
		    if (this.callbacks["WaitingToJoin"]) throw "WaitingToJoin is already bound!";
		    this.callbacks["WaitingToJoin"] = (players: Object) => callback(players);
		    return this;
		}
		
		GameState(callback: (state: Object) => void): GameConnectionClient {
		    if (this.callbacks["GameState"]) throw "GameState is already bound!";
		    this.callbacks["GameState"] = (state: Object) => callback(state);
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
		
		RoundFinished(callback: (roundInfo: Object) => void): GameConnectionClient {
		    if (this.callbacks["RoundFinished"]) throw "RoundFinished is already bound!";
		    this.callbacks["RoundFinished"] = (roundInfo: Object) => callback(roundInfo);
		    return this;
		}
		
		GameFinished(callback: (finalInfo: Object) => void): GameConnectionClient {
		    if (this.callbacks["GameFinished"]) throw "GameFinished is already bound!";
		    this.callbacks["GameFinished"] = (finalInfo: Object) => callback(finalInfo);
		    return this;
		}
		
		OperationError(callback: () => void): GameConnectionClient {
		    if (this.callbacks["OperationError"]) throw "OperationError is already bound!";
		    this.callbacks["OperationError"] = () => callback();
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
					transport: signalR.HttpTransportType.WebSockets
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
			this.connection.on("WaitingFor", (player: Object) => {
			    if (this.client.callbacks["WaitingFor"]) {
			        this.client.callbacks["WaitingFor"](player);
			    } else {
			        throw "WaitingFor implementation could not found!";
			    }
			});
			this.connection.on("WaitingToJoin", (players: Object) => {
			    if (this.client.callbacks["WaitingToJoin"]) {
			        this.client.callbacks["WaitingToJoin"](players);
			    } else {
			        throw "WaitingToJoin implementation could not found!";
			    }
			});
			this.connection.on("GameState", (state: Object) => {
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
			this.connection.on("RoundFinished", (roundInfo: Object) => {
			    if (this.client.callbacks["RoundFinished"]) {
			        this.client.callbacks["RoundFinished"](roundInfo);
			    } else {
			        throw "RoundFinished implementation could not found!";
			    }
			});
			this.connection.on("GameFinished", (finalInfo: Object) => {
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
	
	
			this.server.connection = this.connection;
		}
		
		server = {
			connection: <signalR.HubConnection>{},
	
			
			OnConnectedAsync: () => {
			    this.connection.send("OnConnectedAsync");
			},
			OnDisconnectedAsync: (exception: string) => {
			    this.connection.send("OnDisconnectedAsync", exception);
			},
			Join: () => {
			    this.connection.send("Join");
			},
			MakeMove: (x: number, y: number) => {
			    this.connection.send("MakeMove", x, y);
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
	
		}
	};

	class LobbyConnectionClient {
		callbacks: any = {};
	
		QueueData(callback: (queueDto: QueueDto[], number: number) => void): LobbyConnectionClient {
		    if (this.callbacks["QueueData"]) throw "QueueData is already bound!";
		    this.callbacks["QueueData"] = (queueDto: QueueDto[], number: number) => callback(queueDto, number);
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
		
		Started(callback: (msg: string) => void): LobbyConnectionClient {
		    if (this.callbacks["Started"]) throw "Started is already bound!";
		    this.callbacks["Started"] = (msg: string) => callback(msg);
		    return this;
		}
		
		Stopped(callback: (msg: string) => void): LobbyConnectionClient {
		    if (this.callbacks["Stopped"]) throw "Stopped is already bound!";
		    this.callbacks["Stopped"] = (msg: string) => callback(msg);
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
	
			this.connection.on("QueueData", (queueDto: QueueDto[], number: number) => {
			    if (this.client.callbacks["QueueData"]) {
			        this.client.callbacks["QueueData"](queueDto, number);
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
			this.connection.on("Started", (msg: string) => {
			    if (this.client.callbacks["Started"]) {
			        this.client.callbacks["Started"](msg);
			    } else {
			        throw "Started implementation could not found!";
			    }
			});
			this.connection.on("Stopped", (msg: string) => {
			    if (this.client.callbacks["Stopped"]) {
			        this.client.callbacks["Stopped"](msg);
			    } else {
			        throw "Stopped implementation could not found!";
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
	
			
			OnConnectedAsync: () => {
			    this.connection.send("OnConnectedAsync");
			},
			OnDisconnectedAsync: (exception: string) => {
			    this.connection.send("OnDisconnectedAsync", exception);
			},
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
			if (!this.client.callbacks["Started"]) throw new Error("Started not implemented");
			if (!this.client.callbacks["Stopped"]) throw new Error("Stopped not implemented");
			if (!this.client.callbacks["Players"]) throw new Error("Players not implemented");
			if (!this.client.callbacks["Disconnect"]) throw new Error("Disconnect not implemented");
	
		}
	};

    
	export interface QueueDto
	{
		ID: number;
		Name: string;
		PlayerCount: number;
		BoadrSize: number;
		Till: number;
	}
	export interface KeyCollection
	{
		Count: number;
	}
	export interface ValueCollection
	{
		Count: number;
	}
	export interface PlayerDto
	{
		ID: string;
		Name: string;
	}
	export interface DemoDto
	{
		Name: string;
		Age: number;
	}
}