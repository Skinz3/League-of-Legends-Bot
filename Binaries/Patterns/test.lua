
---------
CLIENT_PROCESS_NAME = "LeagueClientUX"
GAME_PROCESS_NAME = "League of Legends"
Description = "This is a test pattern."


--------

function Execute()
	
	while true do

		if img:textExists("League Of Legends","VICTORY") == true then
			win:log("VICTORY");
		end
	end
	
	
end


