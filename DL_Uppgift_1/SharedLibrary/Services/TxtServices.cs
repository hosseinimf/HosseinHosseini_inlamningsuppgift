using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedLibrary.Services
{
    public class TxtServices
    {
        public async Task CreateTxtFileAsync()
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            await storageFolder.CreateFileAsync("txtuppgift_1.txt", CreationCollisionOption.ReplaceExisting);
        }


        public async Task WriteToTxtFileAsync(Person person)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile file = await storageFolder.GetFileAsync("txtuppgift_1.txt");
 
            string listString = person.FirstName + " " + person.LastName + " " + person.Age + " " + person.Email;
            await FileIO.WriteTextAsync(file, listString);
        }


        public async Task<IEnumerable<Person>> ReadFromTxtFileAsync(string fileName)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync(fileName);

            var persons = new List<Person>();
            var lines = await FileIO.ReadLinesAsync(storageFile);

            foreach (var line in lines)
            {
                var data = line.Split(' ');
                persons.Add(new Person(data[0], data[1], Convert.ToInt32(data[2]), data[3]));
            }
            return persons;
        }
    }
}
