
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This is a test pattern."


--------

function Execute()
	
	img:waitForText( "COLLECTION" );
	img:leftClickText( "COLLECTION" );
	
	img:waitForText( "PROFILE" );
	img:leftClickText( "PROFILE" );
	
	img:waitForText( "PLAY" );
	img:leftClickText( "PLAY" );
	
	img:waitForText( "TRAINING" );
	img:leftClickText( "TRAINING" );
	
	img:waitForText( "PVP" );
	img:leftClickText( "PVP" );
	
end


