using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;


namespace MicrosoftAccessHelper
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]

    public class DatabaseStruct
    {
        //Variables
        string tableName;
        OleDbConnection connection;

        List<string> columnNames;
        DataTable sTable;
        DataTable table;

        //Constructor (Polymorphism)

        public DatabaseStruct()
        {
            columnNames = new List<string>();

            connection = new OleDbConnection();
        }
        public DatabaseStruct(string n)
        {
            columnNames = new List<string>();
            tableName = n;

            connection = new OleDbConnection();
        }

        public DatabaseStruct(DataTable s, string n)
        {
            columnNames = new List<string>();
            sTable = s;
            tableName = n;

            connection = new OleDbConnection();

        }
        public DatabaseStruct(List<string> c, DataTable s, string n)
        {
            columnNames = c;
            sTable = s;
            tableName = n;

            connection = new OleDbConnection();
        }

        //Accessor Methods

        //Get Methods
        public string[] GetColumnNamesAsArr()
        {
            return columnNames.ToArray();
        }

        public List<string> GetColumnNames()
        {
            return columnNames;
        }

        public DataTable GetDataTable()
        {
            return table;
        }

        public DataTable GetSchemaDataTable()
        {
            return sTable;
        }

        public string GetTableName()
        {
            return tableName;
        }

        public OleDbConnection GetConnection()
        {
            return connection;
        }

        //Set Methods

        public void SetColumnNames(List<string> c)
        {
            columnNames = c;
        }

        public void SetColumnNames(string[] c)
        {
            columnNames = c.ToList();
        }

        public void SetDataTable(DataTable t)
        {
            table = t;
        }

        public void SetSchemaDataTable(DataTable t)
        {
            sTable = t;
        }
    }
}