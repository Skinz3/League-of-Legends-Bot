
---------
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern play an Aram Game."

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
        CAST_SPELL_TARGET = {1326,320}
        api:log("We are blue side!");
    else
        CAST_SPELL_TARGET = {644,761}
        api:log("We are red side!");   
    end

    LockCamera();
    
    UpgradeSummonerSpell1();
    UpgradeSummonerSpell2();
    UpgradeSummonerSpell3();

    OpenShop();
    BuyItem1();
    BuyItem2();
    
    api:talk("salam les roya");

    while api:isProcessOpen(GAME_PROCESS_NAME) do

        MoveToAlly2();

        CastSpell1();

        MoveToAlly2();

        api:wait(1000);
    end

    api:log("ended.");


end

function getSide()
    return api:getColor(1350,856) == "#2E7F98";
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

function CastSpell1(x,y)
    api:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    api:pressKey("D1");
    api:wait(200);
end

function MoveToAlly2()
    api:keyDown("F2");
    api:rightClick(886,521);
    api:wait(200);
end


function OpenShop()
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