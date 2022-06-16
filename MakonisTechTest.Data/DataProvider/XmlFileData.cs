using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakonisTechTest.Data
{
    public class XmlFileData : DefaultDataContract
    {
        public XmlFileData()
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
