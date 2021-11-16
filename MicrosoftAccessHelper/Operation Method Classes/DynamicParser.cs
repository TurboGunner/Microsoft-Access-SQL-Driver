using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicrosoftAccessHelper.Operation_Method_Classes
{
    public class DynamicParser
    {
        //Variables
        public static string input = "";
        public static List<string> imports = new List<string> {"System"}; //Automatically inputs system

        public static void AddImports(params string[] add)
        {
            for(int i = 0; i < add.Length; i++)
            {
                for(int j = 0; j < imports.Count; j++) 
                {
                    if (imports[j].Equals(add[i])) //Checks for duplicate imports
                    {
                        imports.Add(add[i]);
                    }
                }
            }
        }

        public static async Task<string> Execute(string code) //Short-circuit eval
        {
            if (imports.Count > 1)
            {
                return await CSharpScript.EvaluateAsync<string>(code, ScriptOptions.Default.WithImports(imports.ToArray()));
            }
            return await CSharpScript.EvaluateAsync<string>(code, ScriptOptions.Default); //If there are no imports
        }

        public static async Task<string> RunCode(string code, string type = "string", string prefix = "value")
        {
            string inputString = $"{type} {prefix} = {code}";
            return await Execute(inputString);
        }
    }
}