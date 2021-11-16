using System.Threading.Tasks;

namespace MicrosoftAccessHelper
{
    public class ParsingMethods
    {
        public static string[] OutputToString(object[] input)
        {
            string[] output = new string[input.Length];

            Parallel.For(0, input.Length, i => {
                output[i] = input[i].ToString();
            });

            return output;
        }

        public static Task<string> OutputToTable(string[] input, string label = "") //Maybe add options to not have label/numbering later?
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += $"\n{label} {i}: {input[i]}";
            }
            return Task.Run(() => output);
        }

        public static Task<string> OutputToTable(string[] input, string[] labels, string formattingLabel = "") //Maybe add options to not have label/numbering later?
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                output += $"\n{formattingLabel} {labels[i]}: {input[i]}";
            }
            return Task.Run(() => output);
        }
    }
}
