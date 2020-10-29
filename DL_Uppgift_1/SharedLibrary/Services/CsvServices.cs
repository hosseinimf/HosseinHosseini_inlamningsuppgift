using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedLibrary.Services
{
    public class CsvServices
    {
        public async Task CreateCsvFileAsync()
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            await storageFolder.CreateFileAsync("csvuppgift_1.csv", CreationCollisionOption.ReplaceExisting);
        }


        public async Task WriteToCsvFileAsync(Person person)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync("csvuppgift_1.csv");
            string listString = person.FirstName + ";" + person.LastName + ";" + person.Age + ";" + person.Email;
            await FileIO.WriteTextAsync(storageFile, listString);
        }


        public async Task <IEnumerable<Person>> ReadFromCsvFileAsync(string fileName)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync(fileName);

            var persons = new List<Person>();
            var lines = await FileIO.ReadLinesAsync(storageFile);

            foreach (var line in lines)
            {
                var data = line.Split(';');
                persons.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));
            }
            
            return persons;
        }
    }
}
