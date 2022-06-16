using MakonisTechTest.Data;
using MakonisTechTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MakonisTechTest.Core
{
    public class PersonService : IPersonService
    {
        private readonly IDataContact _dataProvider;
        public PersonService(IDataContact dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public async Task AddPerson(PersonViewModel person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var persons = await GetPersonList();
            if (persons == null)
                persons = new List<PersonViewModel>();

            persons.Add(person);
            await _dataProvider.Write(JsonConvert.SerializeObject(persons));
        }

        public async Task<bool> DeletePerson(Guid id)
        {
            return await _dataProvider.Remove(id);
        }

        public async Task<List<PersonViewModel>> GetPersonList()
        {
            return await _dataProvider.Read<PersonViewModel>();
        }


    }
}
