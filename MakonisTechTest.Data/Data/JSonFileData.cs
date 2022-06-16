using MakonisTechTest.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MakonisTechTest.Data
{
    /// <summary>
    /// File data class which uses the default behaviour from Abstract class to read and write the data from Json file 
    /// </summary>   
    public class JSonFileData : DefaultDataContract
    {

        public JSonFileData(IOptions<AppSettingsReader> options) : base(options)
        {
            if (options is null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }
        }

        public override async Task<bool> Remove(Guid id)
        {
            bool status = false;

            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            List<PersonViewModel> listOfPerson = await Read<PersonViewModel>();

            if (listOfPerson == null && !listOfPerson.Any()) return false;

            PersonViewModel personToDelete = listOfPerson.FirstOrDefault(x => x.Id == id);

            if (personToDelete is null) return false;

            status = listOfPerson.Remove(personToDelete);

            if (!status) return false;

            await Write(JsonConvert.SerializeObject(listOfPerson));

            return true;
        }
    }
}

