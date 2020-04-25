
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern starts a ."


PLAY_NORMAL = "Client\\playNormal.png"
PLAY_HOVER = "Client\\playHover.png"
--------

function Execute()
	
	--Output
	win:log( "Getting things ready..." );
	
	--Wait until the client is open
	win:waitProcessOpen( CLIENT_PROCESS_NAME );
	
	--Wait until the process is in position
    win:waitUntilProcessBounds( CLIENT_PROCESS_NAME, 1600, 900 );
    
	--Bring process to front
    win:bringProcessToFront( CLIENT_PROCESS_NAME );
	
	--Center the process
	win:centerProcess( CLIENT_PROCESS_NAME );
	
	win:log("Clicking play button...");

	img:waitForButton(PLAY_NORMAL,PLAY_HOVER);

	img:leftClickButton(PLAY_NORMAL,PLAY_HOVER);

	win:log("ended");
	
	
end


