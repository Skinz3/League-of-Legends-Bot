
 -- This pattern script start an Aram Game.

---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script start an Aram Game."
--------

function Execute()

    bot:log("Waiting for league client process...Ensure League client window size is 1600x900");
  
    bot:waitProcessOpen(CLIENT_PROCESS_NAME);
    bot:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
    

    bot:log("Waiting for game to load.");

    bot:bringProcessToFront(CLIENT_PROCESS_NAME);
    bot:centerProcess(CLIENT_PROCESS_NAME)

    bot:wait(2000); -- Wait for an UI element of the client to be displayed 

    bot:log("Client Loaded.");

    client:clickPlayButton(); -- Click 'play' button
    bot:wait(2000);
    client:clickAramButton(); -- Click 'aram' button
    bot:wait(2000);
    client:clickConfirmButton();  -- Click 'confirm' button

    bot:wait(5000); -- Wait for aram background to be displayed (client can be laggy)

    client:clickFindMatchButton();  -- Click 'Find match' button

    bot:log("Finding match...");

    while bot:isProcessOpen(GAME_PROCESS_NAME) == false do -- while match not founded, accept match
        client:acceptMatch();
        bot:wait(4000);
    end

    
    bot:executePattern("aram"); 

end


