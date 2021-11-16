using System;
using System.Data;
using System.Threading.Tasks;

namespace MicrosoftAccessHelper
{
    public class DBAccessMethods : DBHelperMethods
    {
        public static async Task<string[]> GetRowContent(string tableName, int index)
        {
            DataTable table = ReturnDataTable(tableName);

            string[] cellValue = new string[table.Columns.Count];

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Rows.Count <= 0)
                {
                    break;
                }
                try
                {
                    cellValue[i] = table.Rows[index][table.Columns[i]] + "";
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    if (i == 0) //If there is no ID value (or no value at all)
                    {
                        break;
                    }
                }
            }

            return await Task.Run(() => cellValue);
        }

        public static async Task<string> GetContent(string tableName, int index)
        {
            DataTable table = ReturnDataTable(tableName);

            string cellValue = "";

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Rows.Count <= 0)
                {
                    break;
                }
                try
                {
                    cellValue = table.Rows[index][table.Columns[i]] + "";
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                    if (i == 0) //If there is no ID value (or no value at all)
                    {
                        break;
                    }
                }
            }

            return await Task.Run(() => cellValue);
        }

    }
}
