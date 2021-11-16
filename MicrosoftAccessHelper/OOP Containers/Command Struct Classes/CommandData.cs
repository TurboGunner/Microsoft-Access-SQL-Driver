using System.Data.Common;

using MicrosoftAccessHelper.Operation_Method_Classes;

namespace MicrosoftAccessHelper.OOP_Containers
{
    public class CommandData
    {
        //Enums

        public enum OutputType { Reader, NonQuery }

        //Variables

        DbDataReader reader;
        CustomCommand command;
        OutputType type;

        //Constructors (Polymorphism)

        public CommandData(OutputType t, CustomCommand c)
        {
            type = t;
            command = c;
        }

        //Accessor Methods

        //Get Methods
        public DbDataReader GetReader() //Short-circuit eval
        {
            return reader;
        }

        //Set Methods
        public async void SetReader()
        {
            if (type.Equals(OutputType.Reader))
            {
                reader = await DBHelperMethods.CreateReader(command.GetCommand()); //Warning: runs synchronously
            }
        }
        public void SetReader(DbDataReader r)
        {
            reader = r;
        }

    }
}