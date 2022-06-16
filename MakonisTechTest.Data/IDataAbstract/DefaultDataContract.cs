
using MakonisTechTest.Common;
using MakonisTechTest.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MakonisTechTest.Data
{
    public abstract class DefaultDataContract : IDataContact
    {
        private string _fileLocation;
        private readonly object _lockObject = new object();

        public DefaultDataContract()
        {
        }

        public DefaultDataContract(IOptions<AppSettingsReader> options)
        {
            if (options is null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }

            _fileLocation = Path.Combine(Directory.GetCurrentDirectory(), options.Value.FilePath);
        }

        public async virtual Task<List<T>> Read<T>() where T : class
        {
            using (new TimeLocked(_lockObject).Lock(TimeSpan.FromSeconds(2)))
            {
                if (!File.Exists(_fileLocation))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_fileLocation));
                    File.Create(_fileLocation).Close();
                }

                string data = await File.ReadAllTextAsync(_fileLocation);
                if (!string.IsNullOrEmpty(data))
                {
                    return JsonConvert.DeserializeObject<List<T>>(data);
                }
                return null;
            }
        }

        public virtual async Task Write(string data)
        {
            using (new TimeLocked(_lockObject).Lock(TimeSpan.FromSeconds(2)))
            {
                await File.WriteAllTextAsync(_fileLocation, data);
            }
        }

        public abstract Task<bool> Remove(Guid id);
    }
}
