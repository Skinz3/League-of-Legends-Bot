using LeagueBot.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public StartupInvokePriority Type
        {
            get; set;
        }

        public bool Hided
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
        public bool ExitOnThrow
        {
            get;
            set;
        }

        public StartupInvoke(string name, StartupInvokePriority type, bool exitOnTrow = true)
        {
            this.Type = type;
            this.Name = name;
            this.Hided = false;
            this.ExitOnThrow = exitOnTrow;
        }
        public StartupInvoke(StartupInvokePriority type)
        {
            this.Hided = true;
            this.Type = type;
            this.ExitOnThrow = true;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class StartupManager
    {
        public static void Initialize(Assembly startupAssembly)
        {
            Logger.WriteColor1("** Initialisation **");

            Stopwatch watch = Stopwatch.StartNew();

            foreach (var pass in Enum.GetValues(typeof(StartupInvokePriority)))
            {
                foreach (var item in startupAssembly.GetTypes())
                {
                    var methods = item.GetMethods().ToList().FindAll(x => x.GetCustomAttribute(typeof(StartupInvoke), false) != null);
                    var attributes = methods.ConvertAll<KeyValuePair<StartupInvoke, MethodInfo>>(x => new KeyValuePair<StartupInvoke, MethodInfo>(x.GetCustomAttribute(typeof(StartupInvoke), false) as StartupInvoke, x)).FindAll(x => x.Key.Type == (StartupInvokePriority)pass); ;

                    foreach (var data in attributes)
                    {
                        if (!data.Key.Hided)
                        {
                            Logger.Write("(" + pass + ") Loading " + data.Key.Name + " ...", MessageState.INFO);
                        }

                        if (data.Value.IsStatic)
                        {
                            try
                            {
                                data.Value.Invoke(null, new object[0]);
                            }
                            catch (Exception ex)
                            {
                                if (data.Key.ExitOnThrow)
                                {
                                    Logger.Write(ex.ToString(), MessageState.ERROR_FATAL);
                                    return;
                                }
                                else
                                {
                                    Logger.Write("Unable to initialize " + data.Key.Name, MessageState.WARNING);
                                    continue;
                                }
                            }

                        }
                        else
                        {
                            Logger.Write(data.Value.Name + " cannot be executed at startup. Invalid signature", MessageState.WARNING);
                            continue;
                        }


                    }


                }
            }
            watch.Stop();
            Logger.WriteColor1("** Initialisation Complete (" + watch.Elapsed.Seconds + "s) **");
        }
    }
}
