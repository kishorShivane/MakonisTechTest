using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakonisTechTest.Data
{
    public interface IDataContact
    {
        Task<List<T>> Read<T>() where T : class;
        Task Write(string data);
        Task<bool> Remove(Guid id);
    }
}
