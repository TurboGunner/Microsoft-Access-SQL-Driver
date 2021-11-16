using System;

namespace MicrosoftAccessHelper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public class InitializerMethods
    {
        //Helper Methods for Initialization

        public static string GetConnectionString(string provider, string source)
        {
            string p;

            if (provider.Length != 0 && provider.Length > 0)
            {
                p = provider;
            }
            else
            {
                p = "Provider=Microsoft.ACE.OLEDB.16.0; Persist Security Info=True;"; //Default param for 64-bit
            }

            return p + source;
        }

        public static async void ChangeDatabase(string dbName, bool isAsync = true)
        {
            if (dbName.Length == 0)
            {
                return;
            }
            if (isAsync)
            {
                await Program.databaseStruct.GetConnection().ChangeDatabaseAsync(dbName);
            }
            else
            {
                Program.databaseStruct.GetConnection().ChangeDatabase(dbName);
            }
        }

        public static async void CloseDatabase(bool isAsync = true, bool dispose = true)
        {
            if (dispose && isAsync)
            {
                await Program.databaseStruct.GetConnection().DisposeAsync();
                GC.Collect();
            }
            else if (!dispose && isAsync)
            {
                await Program.databaseStruct.GetConnection().CloseAsync();
            }
            else if (dispose && isAsync)
            {
                Program.databaseStruct.GetConnection().Dispose();
                GC.Collect();
            }
            else
            {
                Program.databaseStruct.GetConnection().Close();
            }
        }
    }
}
