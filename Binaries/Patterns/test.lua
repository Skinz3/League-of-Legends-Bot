
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern starts a ."


PLAY_BUTTON = {"Client\\playNormal.png","Client\\playHover.png"}

COOP_BUTTON = {"Client\\coopNormal.png","Client\\coopHover.png"}

INTERMEDIATE_BUTTON = {"Client\\intermediateNormal.png","Client\\intermediateHover.png"}

CONFIRM_BUTTON = {"Client\\confirmNormal.png","Client\\confirmHover.png"}

FINDMATCH_BUTTON = {"Client\\findNormal.png","Client\\findHover.png"}

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

	win:log("Creating lobby...");
	
    --Click Play button
 	img:waitForButton(PLAY_BUTTON[1],PLAY_BUTTON[2]);
	img:leftClickButton(PLAY_BUTTON[1],PLAY_BUTTON[2]);

	--Click Coop button
	img:waitForButton(COOP_BUTTON[1],COOP_BUTTON[2]);
	img:leftClickButton(COOP_BUTTON[1],COOP_BUTTON[2]);
	

	--Click Intermediate button
	img:waitForButton(INTERMEDIATE_BUTTON[1],INTERMEDIATE_BUTTON[2]);
	img:leftClickButton(INTERMEDIATE_BUTTON[1],INTERMEDIATE_BUTTON[2]);

	
	img:waitForButton(CONFIRM_BUTTON[1],CONFIRM_BUTTON[2]);
	img:leftClickButton(CONFIRM_BUTTON[1],CONFIRM_BUTTON[2]);

	img:waitForButton(FINDMATCH_BUTTON[1],FINDMATCH_BUTTON[2]);
	img:leftClickButton(FINDMATCH_BUTTON[1],FINDMATCH_BUTTON[2]);
	
	
end


