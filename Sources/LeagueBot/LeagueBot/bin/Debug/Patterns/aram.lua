
 -- This pattern script start an Aram Game.

---------
GAME_PROCESS_NAME = "League of Legends"
--------

function Execute()

    api:log("Waiting for league of legends process...");

    api:waitProcessOpen(GAME_PROCESS_NAME);
 
    api:waitUntilProcessBounds(GAME_PROCESS_NAME,1030,797)    

    api:log("Waiting for game to load.");

    api:bringProcessToFront(GAME_PROCESS_NAME);
    api:centerProcess(GAME_PROCESS_NAME)

    api:waitForColor(999,908,"#00D605");

    api:log("We are in game !");

    

    

end


