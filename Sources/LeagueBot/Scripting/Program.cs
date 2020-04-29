using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scripting
{
    class Program
    {
        static void Main(string[] args)
        {

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.OutputAssembly = string.Empty;
            parameters.IncludeDebugInformation = false;

            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);


            CompilerResults results = codeProvider.CompileAssemblyFromFile(parameters, "test.cs");

            if (results.Errors.Count > 0)
            {
                string errors = "Compilation failed:\n";
                foreach (CompilerError err in results.Errors)
                {
                    errors += err.ToString() + "\n";
                }
                Console.WriteLine(errors);
                Console.Read();
            }



            IScript o = (IScript)results.CompiledAssembly.CreateInstance("LeagueBot.TestScript");
            o.Execute();


            Console.Read();
        }
    }
}
