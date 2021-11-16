using System.Data.Common;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace MicrosoftAccessHelper.DB_Method_Classes
{
    public class DBWritingMethods : DBHelperMethods
    {
        public static async Task<string> WriteDatabase(params string[] vals) //If you are adding data to all columns in a given row
        {
            string input = $"INSERT INTO {Program.databaseStruct.GetTableName()} VALUES"; //Start of the command
            string output = "";

            int columnsLength = Program.databaseStruct.GetColumnNamesAsArr().Length;

            if (vals.Length != columnsLength) //Checks if the input has the same number of rows as the input
            {
                output += $"Error: The number of rows given \n({vals.Length}) is not the same as the amount of rows in the amount of columns in the table ({columnsLength})";
                return output;
            }

            for(int i = 0; i < vals.Length; i++)
            {
                if (i == 0)
                {
                    input += $"({vals[i]}, ";
                }
                else if (i != (vals.Length - 1)) 
                {
                    input += $" {vals[i]}, ";
                }
                else
                {
                    input += $"{vals[i]}";
                }
            }

            output = "Command: " + input; //If check passes, then the output contains the command text

            OleDbCommand command = CreateCommand(input);

            try
            {
                output += $"\nNumber of Rows Affected: {await command.ExecuteNonQueryAsync()}";
            }
            catch(DbException e)
            {
                output += "\nError: "+ e.ToString();
            }
            return output;
        }

        public static async Task<string> WriteDatabase(string[] columnNames, params string[] vals) //If you are adding data to specific columns in a given row
        {
            string input = $"INSERT INTO {Program.databaseStruct.GetTableName()}"; //Start of the command
            string valueString = "VALUES ";
            string output = "";

            int columnsLength = columnNames.Length;

            if (vals.Length != columnsLength) //Checks if the input has the same number of rows as the input
            {
                output += $"Error: The number of rows given \n({vals.Length}) is not the same as the amount of rows in the amount of columns in the table ({columnsLength})";
                return output;
            }

            for (int i = 0; i < vals.Length; i++)
            {
                if (i == 0)
                {
                    input += $"({columnNames[i]}, ";
                    valueString += $"({vals[i]}, ";
                }
                else if (i != (vals.Length - 1))
                {
                    input += $" {columnNames[i]}, ";
                    valueString += $" {vals[i]}, ";
                }
                else
                {
                    input += $"{columnNames[i]})";
                    valueString += $"{vals[i]});";
                }
            }

            input += valueString;

            output = "Command: " + input; //If check passes, then the output contains the command text

            OleDbCommand command = CreateCommand(input);

            try
            {
                output += $"\nNumber of Rows Affected: {await command.ExecuteNonQueryAsync()}";
            }
            catch (DbException e)
            {
                output += "\nError: " + e.ToString();
            }
            return output;
        }
    }
}
