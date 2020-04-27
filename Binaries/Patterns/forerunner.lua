
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This is a test pattern."


--------

function Execute()
	
	win:waitProcessOpen(CLIENT_PROCESS_NAME);
    win:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
 
    win:bringProcessToFront(CLIENT_PROCESS_NAME);
    win:centerProcess(CLIENT_PROCESS_NAME)
	
	img:waitForText( "COLLECTION" );
	img:leftClickText( "COLLECTION" );
	
	img:waitForText( "PLAY" );
	img:leftClickText( "PLAY" );
	
	img:waitForText( "TRAINING" );
	img:leftClickText( "TRAINING" );
	
	img:waitForText( "PVP" );
	img:leftClickText( "PVP" );
	
	img:waitForText( "HOME" );
	img:leftClickText( "HOME" );
	
	img:waitForText( "PATCH NOTES" );
	img:leftClickText( "PATCH NOTES" );
	
	img:waitForText( "TFT" );
	img:leftClickText( "TFT" );
	
	img:waitForText( "PATCH NOTES" );
	img:leftClickText( "PATCH NOTES" );
	
	img:waitForText( "PROFILE" );
	img:leftClickText( "PROFILE" );
	
	img:waitForText( "MATCH HISTORY" );
	img:leftClickText( "MATCH HISTORY" );
	
end


