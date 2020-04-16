
 -- This pattern script start an Aram Game.

---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
--------

function Execute()

    api:log("Waiting for league client process...");

    api:waitProcessOpen(CLIENT_PROCESS_NAME);
 
    api:waitUntilProcessBounds(CLIENT_PROCESS_NAME,1600,900)    

    api:log("Waiting for league client to load.");

    api:bringProcessToFront(CLIENT_PROCESS_NAME);
    api:centerProcess(CLIENT_PROCESS_NAME)

    api:waitForColor(489,106,"#AE8636"); -- Wait for an UI element of the client to be displayed

    api:log("Client Loaded.");
    
    api:leftClick(306,139); -- Click 'play' button
    api:wait(1000);
    api:leftClick(624,373); -- Click 'aram' button
    api:wait(1000);
    api:leftClick(832,949);  -- Click 'confirm' button

    api:waitForColor(977,507,"#016570"); -- Wait for aram background to be displayed (client can be laggy)

    api:leftClick(832,949);  -- Click 'Find match' button


    api:log("Finding match...");

    while api:getColor(1353,164) ~= "#58431B" do -- while match not founded, accept match
        api:leftClick(947,780);
        api:wait(1000);
    end

    api:executePattern("aram.lua");
    

end


