using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SharedLibrary.Services
{
    public class JsonServices
    {
        public static async Task CreateJsonFileAsync()
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            await storageFolder.CreateFileAsync("jsonuppgift_1.json", CreationCollisionOption.ReplaceExisting);
        }


        public static async Task WriteToJsonFileAsync(Person person)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync("jsonuppgift_1.json");

            var list = new List<Person>();
            list.Add(person);
            var json = JsonConvert.SerializeObject(list);

            await FileIO.AppendTextAsync(storageFile, json);
        }


        public static async Task<string> ReadFromJsonFileAsync(string fileName)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync(fileName);

            return await FileIO.ReadTextAsync(storageFile);
        }
    }
}
