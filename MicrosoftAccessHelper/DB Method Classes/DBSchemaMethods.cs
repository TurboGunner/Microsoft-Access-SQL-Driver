using System;
using System.Data;
using System.Threading.Tasks;

namespace MicrosoftAccessHelper
{
    public class DBSchemaMethods : DBHelperMethods
    {
        public static async Task<string[]> GetColumnSchema(string tableName)
        {
            DataTable table = ReturnDataTable(tableName, TableType.Schema);

            DataColumnCollection columns = table.Columns;

            string[] output = new string[columns.Count];

            //Column Collection to String Arr

            Parallel.For(0, table.Columns.Count, i => {
                output[i] = table.Columns[i].ColumnName;
            });

            return await Task.Run(() => output);
        }

        public static async Task<string[]> GetColumnNames(string tableName, string key = "ColumnName")
        {
            DataTable table = ReturnDataTable(tableName, TableType.Schema);

            DataColumn columnNames = table.Columns[key];

            string[] output = new string[table.Columns.Count];

            //Column Collection to String Arr

                Parallel.For(0, table.Columns.Count, i =>
                {
                    DataRow row;
                    try
                    {
                        row = table.Rows[i];
                        output[i] = row[columnNames].ToString();
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Program.output.OutputError(e);//temp
                    }
                });



            return await Task.Run(() => output);
        }

    }
}
