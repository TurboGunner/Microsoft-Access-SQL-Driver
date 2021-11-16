using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;
using System.Data.Common;

namespace MicrosoftAccessHelper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public class DBHelperMethods
    {
        //Enums

        public enum TableType { Data, Schema, Writing };

        //Methods

        public static OleDbCommand CreateCommand(string content)
        {
            return new OleDbCommand(content, Program.databaseStruct.GetConnection());
        }

        public static async Task<DbDataReader> CreateReader(OleDbCommand command, CommandBehavior behavior = CommandBehavior.Default)
        {
            return await command.ExecuteReaderAsync(behavior);
        }

        public static async void UpdateDataTables(string tableName)
        {
            OleDbCommand command = CreateCommand($"select * from {tableName}");
            DbDataReader reader = await CreateReader(command, CommandBehavior.Default);

            //Get Schema-Table
            Program.databaseStruct.SetSchemaDataTable(await reader.GetSchemaTableAsync());

            DataTable table = new DataTable();

            table.Load(reader, LoadOption.OverwriteChanges);
            Program.databaseStruct.SetDataTable(table);
        }
        public static DataTable ReturnDataTable(string tableName, TableType type = TableType.Data, bool updateTables = true)
        {
            DataTable output;

            if (updateTables)
            {
                UpdateDataTables(tableName);
            }

            if (type.Equals(TableType.Data))
            {
                output = Program.databaseStruct.GetDataTable();
            }
            else
            {
                output = Program.databaseStruct.GetSchemaDataTable();
            }

            return output;
        }

    }
}