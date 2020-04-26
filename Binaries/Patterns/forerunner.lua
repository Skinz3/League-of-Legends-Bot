
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This is a test pattern."


--------

function Execute()
	
	count = 0;
	
	while true do
		
		if count == 100 then
		
			img:findText();
			count = 0;
			
		end
	
		count = count + 1;
	
	end
	
end


