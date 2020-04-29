
---------
GAME_PROCESS_NAME = "League of Legends"
CLIENT_PROCESS_NAME = "LeagueClientUX"

Description = "This pattern play an Aram Game."

Side = false -- Blue = true, Red = false


CAST_SPELL_TARGET = {}

--------

function Execute()

    bot:log("Waiting for league of legends process...");

    bot:waitProcessOpen(GAME_PROCESS_NAME);
 
    bot:waitUntilProcessBounds(GAME_PROCESS_NAME,1030,797)    

    bot:wait(200);

    bot:log("Waiting for game to load.");

    bot:bringProcessToFront(GAME_PROCESS_NAME);
    bot:centerProcess(GAME_PROCESS_NAME)

    game:waitUntilGameStart();

    bot:log("We are in game !");

    bot:bringProcessToFront(GAME_PROCESS_NAME);
    bot:centerProcess(GAME_PROCESS_NAME)

    bot:wait(1000);

    Side = game:isBlueSide("HowlingAbyss");

    if Side == true then
        CAST_SPELL_TARGET = {1084,398}
        bot:log("We are blue side!");
    else
        CAST_SPELL_TARGET = {644,761}
        bot:log("We are red side!");   
    end

    game:upgradeSpell(1);

    --game:talk("Hi guys");

    game:toogleShop();

    game:buyItem(1);
    game:buyItem(2);

    game:toogleShop();

    game:lockAlly(3);

    while bot:isProcessOpen(GAME_PROCESS_NAME) do

        bot:bringProcessToFront(GAME_PROCESS_NAME);
        bot:centerProcess(GAME_PROCESS_NAME)

        game:moveCenterScreen();

        game:castSpell(1,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])
        
        bot:wait(1000);

        game:moveCenterScreen();

        game:castSpell(2,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])

        bot:wait(1000);

        game:moveCenterScreen();

        game:castSpell(3,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])

        bot:wait(1000);

        game:moveCenterScreen();

        game:castSpell(4,CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2])
      
    end

    bot:log("Match ended.");

    bot:waitProcessOpen(CLIENT_PROCESS_NAME);

    bot:bringProcessToFront(CLIENT_PROCESS_NAME);
    bot:centerProcess(CLIENT_PROCESS_NAME)

    bot:wait(5000);

    client:skipHonor();

    bot:wait(2000);

    client:closeGameRecap();

    bot:wait(2000);

    bot:executePattern("startAram"); -- find new match.


end

function getSide()
    return win:getColor(1350,856) == "#2E7F98";
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

function LockCamera()
    win:leftClick(1241,920); 
    win:wait(200);
end

function LockAlly2()
    win:keyUp("F2");
    win:wait(200);
    win:keyDown("F2");
    win:wait(200);
end

function CastSpell1(x,y)
    win:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    win:pressKey("D1");
    win:wait(200);
end

function CastSpell2(x,y)
    win:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    win:pressKey("D2");
    win:wait(200);
end

function CastSpell3(x,y)
    win:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    win:pressKey("D3");
    win:wait(200);
end

function CastSpell4(x,y)
    win:moveMouse(CAST_SPELL_TARGET[1],CAST_SPELL_TARGET[2]);
    win:pressKey("D4");
    win:wait(200);
end

function MoveToAlly2()
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

function IsAlive()
    return win:getColor(765,904) == "#07140E";
end