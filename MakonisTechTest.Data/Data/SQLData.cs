using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakonisTechTest.Data.Data
{
    /// <summary>
    /// Example class which can be used to read and write the data from SQL database
    /// </summary>
    public class SQLData : DefaultDataContract
    {
        public SQLData()
        {

        }

        public override async Task<List<T>> Read<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task Write(string data)
        {
            throw new NotImplementedException();
        }

    }
}
