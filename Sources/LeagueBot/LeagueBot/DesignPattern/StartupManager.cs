using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LeagueBot.DesignPattern
{
    public enum StartupInvokePriority
    {
        Initial = 0,
        SecondPass = 1,
        ThirdPass = 2,
        FourthPass = 4,
        FifthPass = 5,
        SixthPath = 6,
        Last = 7,
    }

    public class StartupInvoke : Attribute
    {
        public StartupInvoke(string name, StartupInvokePriority type, bool exitOnTrow = true)
        {
            Type = type;
            Name = name;
            Hidden = false;
            ExitOnThrow = exitOnTrow;
        }

        public StartupInvoke(StartupInvokePriority type)
        {
            Hidden = true;
            Type = type;
            ExitOnThrow = true;
        }

        public bool ExitOnThrow { get; }

        public bool Hidden { get; }

        public string Name { get; }

        public StartupInvokePriority Type { get; }

        public override string ToString() => Name;
    }

    public class StartupManager
    {
        public static void Initialize(Assembly startupAssembly)
        {
            Logger.WriteColor1("** Initialisation **");

            var watch = Stopwatch.StartNew();

            foreach (var pass in Enum.GetValues(typeof(StartupInvokePriority)))
            {
                foreach (var item in startupAssembly.GetTypes())
                {
                    var methods = item.GetMethods().ToList().FindAll(x => x.GetCustomAttribute(typeof(StartupInvoke), false) != null);
                    var attributes = methods.ConvertAll<KeyValuePair<StartupInvoke, MethodInfo>>(x => new KeyValuePair<StartupInvoke, MethodInfo>(x.GetCustomAttribute(typeof(StartupInvoke), false) as StartupInvoke, x)).FindAll(x => x.Key.Type == (StartupInvokePriority)pass); ;

                    foreach (var data in attributes)
                    {
                        if (!data.Key.Hidden)
                            Logger.Write("(" + pass + ") Loading " + data.Key.Name + " ...", LogLevel.INFO);

                        if (!data.Value.IsStatic)
                            Logger.Write(data.Value.Name + " cannot be executed at startup. Invalid signature", LogLevel.WARNING);
                        else
                        {
                            try
                            {
                                data.Value.Invoke(null, new object[0]);
                            }
                            catch (Exception ex)
                            {
                                if (data.Key.ExitOnThrow)
                                {
                                    Logger.Write(ex.ToString(), LogLevel.ERROR_FATAL);
                                    return;
                                }
                                else
                                    Logger.Write("Unable to initialize " + data.Key.Name, LogLevel.WARNING);
                            }
                        }
                    }
                }
            }
            watch.Stop();
            Logger.WriteColor1("** Initialisation Complete (" + watch.Elapsed.Seconds + "s) **");
        }
    }
}