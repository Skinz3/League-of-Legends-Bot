using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeagueBot.IO
{
    public class CFGFile
    {
        private const string ELEMENT_REGEX = @"^(\w+)=(.+)$";

        private const string CATEGORY_REGEX = @"^\[(\w+)\]$";

        private string Filepath
        {
            get;
            set;
        }
        private Dictionary<string, Dictionary<string, string>> Content
        {
            get;
            set;
        }

        public CFGFile(string filePath)
        {
            this.Content = new Dictionary<string, Dictionary<string, string>>();

            this.Filepath = filePath;

            string[] lines = File.ReadAllLines(filePath);

            string currentCategory = string.Empty;

            foreach (var line in lines)
            {
                var match = Regex.Match(line, CATEGORY_REGEX);

                if (match.Success)
                {
                    currentCategory = match.Groups[1].Value;
                    Content.Add(currentCategory, new Dictionary<string, string>());
                }

                match = Regex.Match(line, ELEMENT_REGEX);

                if (match.Success)
                {
                    Content[currentCategory].Add(match.Groups[1].Value, match.Groups[2].Value);
                }
            }
        }

        public void Set(string category, string key, string value)
        {
            if (!Content.ContainsKey(category))
            {
                Content.Add(category, new Dictionary<string, string>());
            }

            if (!Content[category].ContainsKey(key))
            {
                Content[category].Add(key, value);
            }
            else
            {
                Content[category][key] = value;
            }
        }

        public void Save()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pair in Content)
            {
                sb.AppendLine("[" + pair.Key + "]");

                foreach (var element in pair.Value)
                {
                    sb.AppendLine(element.Key + "=" + element.Value);
                }
                sb.AppendLine();
            }

            File.WriteAllText(Filepath, sb.ToString());
        }
    }
}
