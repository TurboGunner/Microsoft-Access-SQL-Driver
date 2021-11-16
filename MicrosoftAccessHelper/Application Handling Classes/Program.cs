using MicrosoftAccessHelper.OOP_Containers;
using System;
using System.Threading.Tasks; 

namespace MicrosoftAccessHelper
{
    public class Program
    {
        //Globals
        public static DatabaseStruct databaseStruct;
        public static IOutputClass output;

        public static string connectionString;

        public static async Task Main(string[] args)
        {
            //Declaring instances of globals
            output = new ConsoleOutput();
            databaseStruct = new DatabaseStruct();

            //Connection String
            output.GetConnectionString();

            Console.WriteLine("Enter in the name of the table name: ");
            string tableSelection = Console.ReadLine();

            output.OutputConnectionState();

            output.OutputColumnNames(tableSelection);
            await output.OutputRowData(tableSelection);

            string input = Console.ReadLine();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        public static void OnProcessExit(object sender, EventArgs e)
        {
            InitializerMethods.CloseDatabase(); //Default params (true, true)
        }

    }
}