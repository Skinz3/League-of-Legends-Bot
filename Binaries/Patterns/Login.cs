
using System;
using System.Collections.Generic;
using System.Drawing;
using LeagueBot.Patterns;
using LeagueBot.ApiHelpers;
using System.Diagnostics;

namespace LeagueBot
{
    public class Login : PatternScript
    {

        public override void Execute()
        {
			
			bot.StartProcess();
			bot.log("Waiting for league of legends service process (15 seconds delay for weak PC/VMs)");
			bot.wait(6000);
            bot.log("Checking isLogged...");
			bot.wait(1000);
            bot.bringProcessToFront(RIOT_PROCESS_NAME);
            bot.bringProcessToFront(CLIENT_PROCESS_NAME);
            bot.centerProcess(RIOT_PROCESS_NAME);
            bot.centerProcess(CLIENT_PROCESS_NAME);
			
			if(client.isLogged()){
				bot.log("Your are Logged");
				bot.wait(10000);
				bot.executePattern("StartCoop");
			}
			bot.log("Accounts not logged.");
			bot.log("Waiting for riot client process...");
            bot.waitProcessOpen(RIOT_PROCESS_NAME);
            bot.bringProcessToFront(RIOT_PROCESS_NAME);
            bot.centerProcess(RIOT_PROCESS_NAME);
            client.GetLoginData();
			bot.log("Logging in");
            bot.wait(1000);
			if(client.WrongCredentials() == true){
				bot.log("Cant login. Maybe wrong password?");
				bot.wait(500);
				bot.KillProcess(RIOT_PROCESS_NAME);
				bot.wait(2000);
				bot.executePattern("Login");
			}else{
				bot.wait(10000);
				bot.executePattern("StartCoop");
			}
			
        }
    }
}
