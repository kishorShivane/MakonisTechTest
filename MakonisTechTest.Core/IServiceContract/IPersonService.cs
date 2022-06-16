using MakonisTechTest.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakonisTechTest.Core
{
    public interface IPersonService
    {
        Task AddPerson(PersonViewModel person);

        Task<bool> DeletePerson(Guid id);

        Task<List<PersonViewModel>> GetPersonList();
    }
}
