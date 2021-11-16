using System;
using System.Data;
using System.Threading.Tasks;

using MicrosoftAccessHelper.DB_Method_Classes;
using MicrosoftAccessHelper.OOP_Containers;

namespace MicrosoftAccessHelper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public class ConsoleOutput : IOutputClass
    {
        //Constructor
        public ConsoleOutput()
        {

        }

        public void GetConnectionString()
        {
            Console.WriteLine("Enter in the name of the database file: ");
            string databaseSelection = Console.ReadLine();
            Program.connectionString = InitializerMethods.GetConnectionString("", @$"Data Source=C:\{databaseSelection}.accdb"); //Insert Directory
        }

        public async void OutputColumnNames(string tableSelection)
        {
            string[] columnNames = await DBSchemaMethods.GetColumnNames(tableSelection);
            Program.databaseStruct.SetColumnNames(columnNames);

            Console.WriteLine(await ParsingMethods.OutputToTable(columnNames, "Column"));
        }

        public async Task<bool> OutputRowData(string tableSelection)
        {
            bool output = true;

            Console.WriteLine("Enter in the number of the row index: ");
            string rowSelection = Console.ReadLine();

            int rowIndex = 0;
            bool isParsed = int.TryParse(rowSelection, out rowIndex);
            if (!isParsed)
            {
                Console.WriteLine("Error: Parsing failed, defaults to Row Index Value 0");
                output = false;
            }

            string[] rowData = await DBAccessMethods.GetRowContent(tableSelection, rowIndex);
            Console.WriteLine(await ParsingMethods.OutputToTable(rowData, Program.databaseStruct.GetColumnNamesAsArr(), $"Index {rowIndex} at"));

            return output;
        }

        public async void OutputConnectionState()
        {
            Console.WriteLine($"Connection String: {Program.connectionString}");

            //Opens connection, var in class
            if (Program.databaseStruct.GetConnection().State == ConnectionState.Closed)
            {
                Program.databaseStruct.GetConnection().ConnectionString = Program.connectionString;
                await Program.databaseStruct.GetConnection().OpenAsync();
            }

            Console.WriteLine("\nConnection State: " + Program.databaseStruct.GetConnection().State);
        }

        public async void OutputDBWriter()
        {
            //Getting Rows
            Console.WriteLine("How many rows would you like to input? ");
            bool isSuccessful = int.TryParse(Console.ReadLine(), out int rows);

            Console.WriteLine("Enter in the starting index ");
            bool isSuccessfulIndex = int.TryParse(Console.ReadLine(), out int startIndex);
            string[] columnInput = new string[rows];

            if (!isSuccessful || rows < 1 || (startIndex + rows) > Program.databaseStruct.GetColumnNames().Count || !isSuccessfulIndex)
            {
                Console.WriteLine("Warning: invalid parse! ");
                return;
            }
            for(int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Enter in entry {startIndex + i}: ");
                columnInput[i] = Console.ReadLine();

            }
            await DBWritingMethods.WriteDatabase(columnInput);
        }

        public void OutputError(Exception e)
        {
            Console.WriteLine($"Error detected! Error:\n{e}");
        }

    }
}