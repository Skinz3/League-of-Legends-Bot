using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using LeagueBot;
using LeagueBot.IO;
namespace LeagueBot.Game.Entities
{
    class Accounts
    {
        Dictionary<string, string> accountslist;
        public string login;
        public string password;
        public Accounts()
        {
            try
            {
                readFile(Directory.GetCurrentDirectory() + @"\accounts.txt");
                foreach (KeyValuePair<string, string> acc in accountslist)
                {
                    Console.WriteLine("Login: {0} and Password {1} added!", acc.Value, acc.Key);
                    this.login = acc.Value;
                    this.password = acc.Key;
                }
            }
            catch
            {
                Logger.Write("Accounts data could not be found. Are you missing accounts.txt?");
            }

        }

        private void readFile(string path)
        {
            accountslist = new Dictionary<string, string>();
            if (File.Exists(path))
            {
                string[] allacc = File.ReadAllLines(path);

                foreach (string acc in allacc)
                {

                    string[] x = acc.Split(':');
                    accountslist.Add(x[1], x[0]);
                }

            }
            else
            {
                Logger.Write("accounts.txt could not be found");
            }
        }
    }
}
