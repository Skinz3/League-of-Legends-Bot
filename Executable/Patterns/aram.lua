
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

    api:wait(2000);

    api:leftClick(1241,920); -- lock camera

    api:wait(200);

    api:leftClick(826,833); -- upgrade summoner Q
    
    api:wait(200);

    api:leftClick(875,833); -- upgrade summoner Z
    
    api:wait(200);

    api:leftClick(917,833); -- upgrade summoner E

    api:wait(200);

    api:leftClick(1138,917); -- open shop

    api:wait(200);

    api:rightClick(578,332) -- buy item

    api:wait(200);

    api:rightClick(1260,189); -- close shop

end


