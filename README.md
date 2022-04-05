Game Server For TicTacToe game (Advanced variation).

Brief:
	Most of us know TicTacToe is very easy and not challenging game.
	In this server there is two more features added.
	One - it can be played also on 5x5 sized board.
	Second - cross and circle also have different sized versions.
		This means if square is occupied with one piece, you still can capture
		that square by placing bigger sized figure, if you have any.
	
	This adds more complexity and challenge to the game.
	

Project Architecture:
	Project is divided into two different main modules. Server and Presentation.
	Server is fully functional game server. Presentation is just a show case of
	how to make client for such kind of server and how to communicate with it
	using SignalR Core library for sockets on both sides(server and presentation).
	Server is done in C# and presentation is done typescript(angular).
	