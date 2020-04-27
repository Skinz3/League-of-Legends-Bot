
---------
GAME_PROCESS_NAME = "League of Legends"
CLIENT_PROCESS_NAME = "LeagueClientUX"

Description = "This pattern play an Coop vs IA Game."

Side = false -- Blue = true, Red = false


CAST_SPELL_TARGET = {}

--------

function Execute()

    win:log("Waiting for league of legends process...");

    win:waitProcessOpen(GAME_PROCESS_NAME);
 
    win:waitUntilProcessBounds(GAME_PROCESS_NAME,1030,797)    

    win:wait(200);

    win:log("Waiting for game to load.");

    win:bringProcessToFront(GAME_PROCESS_NAME);
    win:centerProcess(GAME_PROCESS_NAME)

    WaitForGameToStart();

    win:log("We are in game !");

    win:bringProcessToFront(GAME_PROCESS_NAME);
    win:centerProcess(GAME_PROCESS_NAME)

    win:wait(1000);

    Side = getSide();

    if Side == true then
        CAST_SPELL_TARGET = {1084,398}
        win:log("We are blue side!");
    else
        CAST_SPELL_TARGET = {644,761}
        win:log("We are red side!");   
    end

    UpgradeSummonerSpell1();

    game:talk("Hello world");

    ToogleShop();
    BuyItem1();
    BuyItem2();
    ToogleShop();

    LockAlly3();


    while win:isProcessOpen(GAME_PROCESS_NAME) do

        win:bringProcessToFront(GAME_PROCESS_NAME);
        win:centerProcess(GAME_PROCESS_NAME)

        MoveToAlly();

        game:castSpell(1,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])
        
        win:wait(1000);

        MoveToAlly();

        game:castSpell(2,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])

        win:wait(1000);

        MoveToAlly();

        game:castSpell(3,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])

        win:wait(1000);

        MoveToAlly();

        game:castSpell(4,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])
      
    end

    win:log("Match ended.");

    win:waitProcessOpen(CLIENT_PROCESS_NAME);

    win:bringProcessToFront(CLIENT_PROCESS_NAME);
    win:centerProcess(CLIENT_PROCESS_NAME)

    win:wait(5000);

    win:leftClick(962,903); -- skip honor

    win:wait(2000);

    win:leftClick(716,947); -- close game recap

    win:wait(2000);

    win:executePattern("startCoop"); -- find new match.

end

function getSide()
    return win:getColor(1343,868) == "#2A768C";
end

function UpgradeSummonerSpell1()
    win:leftClick(826,833); 
    win:wait(200);
end

function UpgradeSummonerSpell2()
    win:leftClick(875,833);
    win:wait(200);
end

function UpgradeSummonerSpell3()
    win:leftClick(917,833); 
    win:wait(200);
end

function WaitForGameToStart()
    win:waitForColor(997,904,"#00D304");
end



function LockAlly3()
    win:keyUp("F3");
    win:wait(500);
    win:keyDown("F3");
    win:wait(500);
end



function MoveToAlly()
    win:rightClick(886,521);
    win:wait(200);
end


function ToogleShop()
    win:pressKey("P");
    win:wait(200);
end

function BuyItem1()
    win:rightClick(577,337);
    win:wait(200);
end

function BuyItem2()
    win:rightClick(782,336);
    win:wait(200);
end

