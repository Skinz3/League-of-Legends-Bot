
---------
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern play an Aram Game."
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
--[[
    api:bringProcessToFront(GAME_PROCESS_NAME);
    api:centerProcess(GAME_PROCESS_NAME)

    api:wait(2000);

    api:leftClick(1241,920); -- lock camera

    api:wait(200);

    api:leftClick(826,833); -- upgrade summoner Q
    
    api:wait(200);

    api:leftClick(875,833); -- upgrade summoner Z
    
    api:wait(200);

    api:leftClick(917,833); -- upgrade summoner E

    api:wait(10000);

    api:rightClick(1351,860); --]]


    while api:isProcessOpen(GAME_PROCESS_NAME) do
        api:keyDown("F2");

        api:rightClick(886,521);

        api:moveMouse(1026,475);
        api:pressKey("D1");
        api:wait(200);

        api:moveMouse(1026,475);
        api:pressKey("D2");
        api:wait(1000);

        api:rightClick(886,521);

        api:moveMouse(1026,475);
        api:pressKey("D3");
        api:wait(1000);

        api:rightClick(886,521);
        
        api:moveMouse(1026,475);
        api:pressKey("D4");
        api:wait(1000);
    end

    api:log("ended.");


end


