using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Threading.Tasks;
using MicrosoftAccessHelper.OOP_Containers;
using static MicrosoftAccessHelper.OOP_Containers.CommandData;

namespace MicrosoftAccessHelper.Operation_Method_Classes
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public class CustomCommand : DBHelperMethods
    {
        //Variables
        string commandString;
        OleDbCommand command;

        //Constructors (Polymorphism)

        public CustomCommand(string s)
        {
            commandString = s;
            command = CreateCommand(commandString);
        }

        public CustomCommand(string s, OleDbCommand c)
        {
            commandString = s;
            command = c;
        }

        //Accessor Methods

        //Get Commands
        public string GetCommandString()
        {
            return commandString;
        }

        public OleDbCommand GetCommand()
        {
            return command;
        }

        //Set Methods
        public void SetCommandString(string s, bool recreateCommand = true)
        {
            commandString = s;
            if(!recreateCommand) //Short-circuit eval
            {
                return;
            }
            command = CreateCommand(commandString);
        }

        public void SetCommand(OleDbCommand c, bool recreateString = true)
        {
            command = c;
            if (!recreateString) //Short-circuit eval
            {
                return;
            }
            command.CommandText = commandString;
        }

        //Execution Methods

        public CommandData AssembleCommand(TableType dataType = TableType.Data) //Default to be data-oriented
        {
            CommandData output;

            if(dataType.Equals(TableType.Data) || dataType.Equals(TableType.Schema))
            {
                output = new CommandData(OutputType.Reader, this);
            }
            else
            {
                output = new CommandData(OutputType.NonQuery, this);
            }
            return output;
        }

        public async Task<DataTable> ExecuteCommand(TableType dataType, CommandData data, string tableName)
        {
            DataTable output = new DataTable();

            if(dataType.Equals(TableType.Data))
            {
                DbDataReader reader = data.GetReader();
                output.Load(reader, LoadOption.OverwriteChanges);
            }
            else if (dataType.Equals(TableType.Schema))
            {
                DbDataReader reader = data.GetReader();
                output = await reader.GetSchemaTableAsync();
            }
            return output;
        }

        //Overrides

        public override string ToString()
        {
            return $"Command: {commandString}";
        }

    }
}