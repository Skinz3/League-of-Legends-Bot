using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.IO
{
    public class FileIO
    {
        private string dir;

        public FileIO(string dir)
        {
            this.dir = dir;
        }

        public string[] getChamps()
        {
            return File.ReadAllLines(dir);
        }

        public string[] getAccounts()
        {
            return File.ReadAllLines(dir);
        }

    }
}
