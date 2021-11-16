using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftAccessHelper.OOP_Containers
{
    public interface IOutputClass
    {
        public abstract void GetConnectionString();
        public abstract void OutputColumnNames(string tableSelection);
        public abstract Task<bool> OutputRowData(string tableSelection);
        public abstract void OutputConnectionState();
        public abstract void OutputDBWriter();
        public abstract void OutputError(Exception e);
    }
}
