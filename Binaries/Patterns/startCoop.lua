
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This pattern script start an Coop against AI Game."

SELECTED_CHAMPION = "garen" 
MODE = "intro" -- Modes : (intermediate,intro)
--------

function Execute()

    bot:log("Waiting for league client process... Ensure League client window size is 1600x900");
  
    bot:waitProcessOpen(CLIENT_PROCESS_NAME);
    bot:bringProcessToFront(CLIENT_PROCESS_NAME);
    bot:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900);
    bot:centerProcess(CLIENT_PROCESS_NAME)

    bot:log("Client ready.");
    
    client:clickPlayButton(); -- Click 'play' button
    bot:wait(2000);
    client:clickCoopvsIAText(); -- Click 'coop vs ai' button
    bot:wait(2000);


    if MODE == "intermediate" then
        client:clickIntermediateText(); 
    elseif MODE == "intro" then
        client:clickIntroText();
    end

  

    bot:wait(2000);
    client:clickConfirmButton();  -- Click 'confirm' button

    bot:wait(5000); -- Wait for aram background to be displayed (client can be laggy)

    client:clickFindMatchButton();  -- Click 'Find match' button

    bot:log("Finding match...");

    while client:mustSelectChamp() == false do -- while match not founded, accept match
        client:acceptMatch();
        bot:wait(3000);
    end

    bot:log("Match founded");

    client:clickChampSearch(); -- Search Champ

    bot:inputWords(SELECTED_CHAMPION); -- Search garen
    
    client:selectFirstChampion();  -- Select first champ

    bot:wait(2000);

    client:lockChampion();  -- Click 'lock in'

    bot:executePattern("coop");
    

end


