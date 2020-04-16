
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

    api:waitForColor(997,904,"#00D304");

    api:log("We are in game !");

    api:wait(2000);

    api:leftClick(1241,920); -- lock camera

    api:wait(200);

    api:leftClick(826,833); -- upgrade summoner Q
    
    api:wait(200);

    api:leftClick(875,833); -- upgrade summoner Z
    
    api:wait(200);

    api:leftClick(917,833); -- upgrade summoner E

    api:wait(10000);

    api:rightClick(1351,860);


    while api:isProcessOpen(GAME_PROCESS_NAME) do
        api:rightClick(1351,860);
        api:wait(10000);
        api:rightClick(1372,837);
        api:wait(1000);
        api:moveMouse(1026,475);
        api:pressKey("D1");
        api:wait(1000);
        api:rightClick(1372,830);
        api:wait(1000);
        api:moveMouse(1026,475);
        api:pressKey("D2");
        api:moveMouse(1026,475);
        api:pressKey("D3");
        api:wait(500);
        api:rightClick(1351,860);
    end

    api:log("ended.");


end


