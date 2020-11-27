using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LeagueBot.IO
{
    public class CFGFile
    {
        private const string CATEGORY_REGEX = @"^\[(\w+)\]$";
        private const string ELEMENT_REGEX = @"^(\w+)=(.+)$";

        public CFGFile(string filePath)
        {
            Filepath = filePath;
            Content = new Dictionary<string, Dictionary<string, string>>();

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
                    Content[currentCategory].Add(match.Groups[1].Value, match.Groups[2].Value);
            }
        }

        private Dictionary<string, Dictionary<string, string>> Content { get; }

        private string Filepath { get; }

        public void Save()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var pair in Content)
            {
                sb.AppendLine("[" + pair.Key + "]");

                foreach (var element in pair.Value)
                    sb.AppendLine(element.Key + "=" + element.Value);

                sb.AppendLine();
            }

            File.WriteAllText(Filepath, sb.ToString());
        }

        public void Set(string category, string key, string value)
        {
            if (!Content.ContainsKey(category))
                Content.Add(category, new Dictionary<string, string>());

            if (!Content[category].ContainsKey(key))
                Content[category].Add(key, value);
            else
                Content[category][key] = value;
        }
    }
}