using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccessLibrary.Data
{
    public class SettingsContext
    {
        private static Settings _settings { get; set; }

        public async static void GetSettingsInformation()
        {
            _settings = await ReadFromJsonFileAsync("settings.json");
        }



        public static IEnumerable<string> GetStatus()
        {
            var list = new List<string>();


            foreach (var status in _settings.Status)
            {
                list.Add(status);
            }

            return list;
        }

        public static int GetMaxItemsCount()
        {
            return _settings.MaxItemsCount;
        }


        public static async Task LoadCreateJsonFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            await storageFolder.CreateFileAsync("settings.json", CreationCollisionOption.ReplaceExisting);

            await SettingsContext.WriteToJsonFileAsync(new Settings("new", "active", "closed", 5));

            _settings = await ReadFromJsonFileAsync("settings.json");
        }

        public static async Task CreateJsonFileAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            await storageFolder.CreateFileAsync("settings.json", CreationCollisionOption.ReplaceExisting);
        }


        public static async Task WriteToJsonFileAsync(Settings settings)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync("settings.json");

            var json = JsonConvert.SerializeObject(settings);

            await FileIO.WriteTextAsync(storageFile, json);
        }


        public static async Task<Settings> ReadFromJsonFileAsync(string fileName)
        {
            List<string> list = new List<string>();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await storageFolder.GetFileAsync(fileName);

            var Json = JsonConvert.DeserializeObject<Settings>(await FileIO.ReadTextAsync(storageFile));

            return Json;
        }
    }





    public class Settings
    {
        public string[] Status = new string[3];
        public int MaxItemsCount { get; set; }


        public Settings(string str1, string str2, string str3, int maxNumber)
        {
            Status[0] = str1;
            Status[1] = str2;
            Status[2] = str3;

            MaxItemsCount = maxNumber;
        }

        public Settings()
        {

        }

        public Settings(string str1, string str2, string str3)
        {
            Status[0] = str1;
            Status[1] = str2;
            Status[2] = str3;
        }
    }
}
