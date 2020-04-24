
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
	
	--If we don't have the game confirm button
	if client:getConfirmButton() == false then
	
		--Wait for the play button to be displayed on screen
		client:waitForPlayButton();
		
		--Click the play button once it's found
		client:clickPlayButton();

		--Wait for the game confirm button
		client:waitForConfirmButton();
	
	end
	
	--Select the game mode ( "summoners rift", "aram", "one for all", "teamfight tactics" )
	client:clickGameMode( "one for all" );
	
	--Click the confirm button
	client:clickConfirmButton();
	
end


