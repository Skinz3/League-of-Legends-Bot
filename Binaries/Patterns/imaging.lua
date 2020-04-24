
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script finds an image, and clicks it."
--------

function Execute()

    win:log("Waiting for test image...");
	win:waitForImage("play.png");
	win:log("Image found!");
	win:wait(2000);
	win:leftClickImage("play.png");
	win:log("Image clicked!");
	
end


