using LeagueBot.Api;
using LeagueBot.DesignPattern;
using LeagueBot.IO;
using LeagueBot.Utils;
using LeagueBot.Windows;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeagueBot.Patterns
{
    class PatternsManager
    {
        public const string PATH = "Patterns\\";

        public const string EXTENSION = ".cs";

        static Dictionary<string, Type> Scripts = new Dictionary<string, Type>();

        [StartupInvoke("Patterns", StartupInvokePriority.SecondPass)]
        public static void Initialize()
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.OutputAssembly = string.Empty;
            parameters.IncludeDebugInformation = false;

            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            parameters.ReferencedAssemblies.Add("System.Drawing.dll");

            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, PATH)).Where(x => Path.GetExtension(x) == EXTENSION).ToArray();

            CompilerResults results = codeProvider.CompileAssemblyFromFile(parameters, files);

            if (results.Errors.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (CompilerError err in results.Errors)
                {
                    sb.AppendLine(string.Format("{0}({1},{2}) : {3}", Path.GetFileName(err.FileName), err.Line, err.Column, err.ErrorText));
                }
                Logger.Write(sb.ToString(), MessageState.WARNING);
                Console.Read();
                Environment.Exit(0);
            }

            codeProvider.Dispose();

            foreach (var type in results.CompiledAssembly.GetTypes())
            {
                Scripts.Add(type.Name, type);
            }

        }
        public static bool Contains(string name)
        {
            return Scripts.ContainsKey(name);
        }
        public static void Execute(string name)
        {
            if (!Scripts.ContainsKey(name))
            {
                Logger.Write("Unable to execute " + name + EXTENSION + ". Script not found.", MessageState.WARNING);
            }
            else
            {
                PatternScript script = (PatternScript)Activator.CreateInstance(Scripts[name]);
                script.bot = new BotApi();
                script.client = new ClientApi();
                script.game = new GameApi();


                if (!script.ThrowException)
                {
                    try
                    {
                        Logger.Write("Pattern : " + name + " (safe)", MessageState.IMPORTANT_INFO);
                        script.Execute();
                    }
                    catch (Exception ex)
                    {
                        LogFile.Log(name + " " + ex);
                        Logger.Write("Pattern : " + name + " stopped. Ending...", MessageState.IMPORTANT_INFO);
                        script.End();
                    }
                }
                else
                {
                    Logger.Write("Pattern : " + name, MessageState.IMPORTANT_INFO);
                    script.Execute();
                }
            }
        }
        public static string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var script in Scripts)
            {
                sb.AppendLine("-" + script.Key);
            }

            return sb.ToString();
        }
    }
}
