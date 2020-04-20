
 -- This pattern script start an Aram Game.

---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script start an Aram Game."
--------

function Execute()

    api:log("Waiting for league client process...");
  
    api:waitProcessOpen(CLIENT_PROCESS_NAME);
    api:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
    

    api:log("Waiting for game to load.");

    api:bringProcessToFront(CLIENT_PROCESS_NAME);
    api:centerProcess(CLIENT_PROCESS_NAME)

    api:wait(2000); -- Wait for an UI element of the client to be displayed 

    api:log("Client Loaded.");

    api:leftClickImage("play.png");
    
    --api:leftClick(306,139); -- Click 'play' button

    api:wait(2000);
    api:leftClick(624,373); -- Click 'aram' button
    api:wait(2000);
    api:leftClick(832,949);  -- Click 'confirm' button

    api:wait(5000); -- Wait for aram background to be displayed (client can be laggy)

    api:leftClick(832,949);  -- Click 'Find match' button

    api:log("Finding match...");

    while api:isProcessOpen(GAME_PROCESS_NAME) == false do -- while match not founded, accept match
        api:leftClick(947,780);
        api:wait(3000);
    end

    api:executePattern("aram");
    

end


