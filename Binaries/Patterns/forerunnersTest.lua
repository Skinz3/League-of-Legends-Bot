
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "Forerunner's test image recognition and pattern structure."
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

	--Wait for the play button to be displayed on screen
	client:waitForPlayButton();
	
	win:log( "Play button detected" );
	
	
	--Detect current client status
	--status = win:getClientStatus();
	
	
	
	
end


