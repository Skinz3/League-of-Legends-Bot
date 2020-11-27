using LeagueBot.Api;
using LeagueBot.DesignPattern;
using LeagueBot.IO;
using LeagueBot.Utils;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LeagueBot.Patterns
{
    internal class PatternsManager
    {
        public const string EXTENSION = ".cs";
        public const string PATH = "Patterns\\";
        private static Dictionary<string, Type> Scripts = new Dictionary<string, Type>();

        public static bool Contains(string name) => Scripts.ContainsKey(name);

        public static void Execute(string name)
        {
            if (!Scripts.ContainsKey(name))
                Logger.Write("Unable to execute " + name + EXTENSION + ". Script not found.", LogLevel.WARNING);
            else
            {
                var script = (PatternScript)Activator.CreateInstance(Scripts[name]);
                script.Bot = new BotApi();
                script.Client = new ClientApi();
                script.Game = new GameApi();

                if (!script.ThrowException)
                {
                    try
                    {
                        Logger.Write("Pattern : " + name + " (safe)", LogLevel.IMPORTANT_INFO);
                        script.Execute();
                    }
                    catch (Exception ex)
                    {
                        LogFile.Log(name + " " + ex);
                        Logger.Write("Pattern : " + name + " stopped. Ending...", LogLevel.IMPORTANT_INFO);
                        script.OnEnd();
                    }
                }
                else
                {
                    Logger.Write("Pattern : " + name, LogLevel.IMPORTANT_INFO);
                    script.Execute();
                }
            }
        }

        [StartupInvoke("Patterns", StartupInvokePriority.SecondPass)]
        public static void Initialize()
        {
            var codeProvider = new CSharpCodeProvider();

            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                OutputAssembly = string.Empty,
                IncludeDebugInformation = false
            };

            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");

            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, PATH)).Where(x => Path.GetExtension(x) == EXTENSION).ToArray();

            CompilerResults results = codeProvider.CompileAssemblyFromFile(parameters, files);

            if (results.Errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError err in results.Errors)
                    sb.AppendLine(string.Format("{0}({1},{2}) : {3}", Path.GetFileName(err.FileName), err.Line, err.Column, err.ErrorText));

                Logger.Write(sb.ToString(), LogLevel.WARNING);
                Console.Read();
                Environment.Exit(0);
            }

            codeProvider.Dispose();

            foreach (var type in results.CompiledAssembly.GetTypes())
                Scripts.Add(type.Name, type);
        }

        public static string ToString()
        {
            var sb = new StringBuilder();
            foreach (var script in Scripts)
                sb.AppendLine("-" + script.Key);

            return sb.ToString();
        }
    }
}