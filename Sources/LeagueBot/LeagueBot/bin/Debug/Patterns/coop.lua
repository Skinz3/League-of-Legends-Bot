
---------
GAME_PROCESS_NAME = "League of Legends"
CLIENT_PROCESS_NAME = "LeagueClientUX"

Description = "This pattern play an Coop vs IA Game."

Side = false -- Blue = true, Red = false


CAST_SPELL_TARGET = {}

--------

function Execute()

    api:log("Waiting for league of legends process...");

    api:waitProcessOpen(GAME_PROCESS_NAME);
 
    api:waitUntilProcessBounds(GAME_PROCESS_NAME,1030,797)    

    api:wait(200);

    api:log("Waiting for game to load.");

    api:bringProcessToFront(GAME_PROCESS_NAME);
    api:centerProcess(GAME_PROCESS_NAME)

    WaitForGameToStart();

    api:log("We are in game !");

    api:bringProcessToFront(GAME_PROCESS_NAME);
    api:centerProcess(GAME_PROCESS_NAME)

    api:wait(1000);

    Side = getSide();

    if Side == true then
        CAST_SPELL_TARGET = {1084,398}
        api:log("We are blue side!");
    else
        CAST_SPELL_TARGET = {644,761}
        api:log("We are red side!");   
    end

    LockCamera();
    
    UpgradeSummonerSpell1();
    UpgradeSummonerSpell2();
    UpgradeSummonerSpell3();

    ToogleShop();
    BuyItem1();
    BuyItem2();
    ToogleShop();

    LockAlly2();
    
    -- api:talk("salam les roya");

    while api:isProcessOpen(GAME_PROCESS_NAME) do

        api:bringProcessToFront(GAME_PROCESS_NAME);
        api:centerProcess(GAME_PROCESS_NAME)
        
        MoveToAlly2();

        CastSpell1();

        api:wait(1000);

        MoveToAlly2();

        CastSpell2();

        api:wait(1000);

        MoveToAlly2();

        CastSpell3();

        api:wait(1000);

        MoveToAlly2();

        CastSpell4();
      
    end

    api:log("Match ended.");

    api:waitProcessOpen(CLIENT_PROCESS_NAME);

    api:bringProcessToFront(CLIENT_PROCESS_NAME);
    api:centerProcess(CLIENT_PROCESS_NAME)

    api:wait(5000);

    api:leftClick(962,903); -- skip honor

    api:wait(2000);

    api:leftClick(716,947); -- close game recap

    api:wait(2000);

    api:executePattern("startCoop"); -- find new match.

end

function getSide()
    return api:getColor(1343,868) == "#2A768C";
end

function UpgradeSummonerSpell1()
    api:leftClick(826,833); 
    api:wait(200);
end

function UpgradeSummonerSpell2()
    api:leftClick(875,833);
    api:wait(200);
end

function UpgradeSummonerSpell3()
    api:leftClick(917,833); 
    api:wait(200);
end

function WaitForGameToStart()
    api:waitForColor(997,904,"#00D304");
end

function LockCamera()
    api:leftClick(1241,920); 
    api:wait(200);
end

function LockAlly2()
    api:keyUp("F2");
    api:wait(200);
    api:keyDown("F2");
    api:wait(200);
end

function CastSpell1(x,y)
    api:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    api:pressKey("D1");
    api:wait(200);
end

function CastSpell2(x,y)
    api:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    api:pressKey("D2");
    api:wait(200);
end

function CastSpell3(x,y)
    api:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    api:pressKey("D3");
    api:wait(200);
end

function CastSpell4(x,y)
    api:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    api:pressKey("D4");
    api:wait(200);
end

function MoveToAlly2()
    api:rightClick(886,521);
    api:wait(200);
end


function ToogleShop()
    api:pressKey("P");
    api:wait(200);
end

function BuyItem1()
    api:rightClick(577,337);
    api:wait(200);
end

function BuyItem2()
    api:rightClick(782,336);
    api:wait(200);
end

function IsAlive()
    return api:getColor(765,904) == "#07140E";
end