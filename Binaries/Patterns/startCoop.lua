
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script start an Coop against AI Game."
--------

function Execute()

    win:log("Waiting for league client process... Ensure League client window size is 1600x900");
  
    win:waitProcessOpen(CLIENT_PROCESS_NAME);
    win:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
    

    win:log("Waiting for game to load.");

    win:bringProcessToFront(CLIENT_PROCESS_NAME);
    win:centerProcess(CLIENT_PROCESS_NAME)

    win:wait(2000); -- Wait for an UI element of the client to be displayed 

    win:log("Client Loaded.");
    
    win:leftClick(306,139); -- Click 'play' button
    win:wait(2000);
    win:leftClick(336,213); -- Click 'coop vs ai' button
    win:wait(2000);
    win:leftClick(755,790); -- Click 'intermediate' button
    win:wait(2000);
    win:leftClick(832,949);  -- Click 'confirm' button

    win:wait(5000); -- Wait for aram background to be displayed (client can be laggy)

    win:leftClick(832,949);  -- Click 'Find match' button

    win:log("Finding match...");

    while not(win:isProcessOpen(GAME_PROCESS_NAME)) do -- Until process open, it's always possible that we fall back to match found screen
        while IsMatchFoundScreen() do
            win:log("Match founded");
            AcceptMatch()
        end
        win:wait(4000);
    end

    win:executePattern("coop");
end

function IsMatchFoundScreen()
    return win:getColor(932,580) == "#F2E5D1"
end

function SelectChampion()
    win:leftClick(645,275);  -- Select first champ
    win:wait(1000);

    win:leftClick(959,831);  -- Click 'lock in'
    win:log("Waiting for league of legends process...");
end

function AcceptMatch()
    while win:getColor(1661,941) ~= "#CFBC91" do -- while not in champ selection screen, try accept match and wait
        win:leftClick(947,780);
        win:wait(2000);
    end

    win:log("Selecting Champion"); -- in champ selection screen
    SelectChampion()
end


