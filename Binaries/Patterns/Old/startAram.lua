
 -- This pattern script start an Aram Game.

---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script start an Aram Game."
--------

function Execute()

    win:log("Waiting for league client process...Ensure League client window size is 1600x900");
  
    win:waitProcessOpen(CLIENT_PROCESS_NAME);
    win:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
    

    win:log("Waiting for game to load.");

    win:bringProcessToFront(CLIENT_PROCESS_NAME);
    win:centerProcess(CLIENT_PROCESS_NAME)

    win:wait(2000); -- Wait for an UI element of the client to be displayed 

    win:log("Client Loaded.");

    win:leftClick(306,139); -- Click 'play' button
    win:wait(2000);
    win:leftClick(624,373); -- Click 'aram' button
    win:wait(2000);
    win:leftClick(832,949);  -- Click 'confirm' button

    win:wait(5000); -- Wait for aram background to be displayed (client can be laggy)

    win:leftClick(832,949);  -- Click 'Find match' button

    win:log("Finding match...");

    while win:isProcessOpen(GAME_PROCESS_NAME) == false do -- while match not founded, accept match
        win:leftClick(947,780);
        win:wait(3000);
    end

    win:executePattern("aram");
    

end


