using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;

namespace SharedLibrary.Services
{
    public class XmlServices
    {
        public async Task CreateXmlFileAsync()
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            await storageFolder.CreateFileAsync("xmluppgift_1.xml", CreationCollisionOption.ReplaceExisting);
        }


        public async void WriteToXmlFileAsync(Person person)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync("xmluppgift_1.xml");

            using (IRandomAccessStream streamWriter = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                Stream stream = streamWriter.AsStreamForWrite();

                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = (" "),
                    CloseOutput = true
                };

                using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
                {
                    xmlWriter.WriteStartElement($"person");

                    xmlWriter.WriteElementString($"FirstName", $"{ person.FirstName}");
                    xmlWriter.WriteElementString($"LastName", $"{ person.LastName}");
                    xmlWriter.WriteElementString($"Age", $"{ person.Age}");
                    xmlWriter.WriteElementString($"Email", $"{ person.Email}");
                    
                    xmlWriter.Flush();
                }
            }
        }


        public async Task<IEnumerable<Person>> ReadFromXmlFileAsync(string fileName)
        {
            StorageFolder storageFolder = KnownFolders.DocumentsLibrary;
            StorageFile storageFile = await storageFolder.GetFileAsync(fileName);
            
            var persons = new List<Person>();

            using (var stream = await storageFile.OpenStreamForReadAsync())
            {                
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(stream);

                XmlNodeList nodes = xdoc.GetElementsByTagName("person");

                foreach (XmlNode node in nodes)
                {
                    string firstName = node["FirstName"].InnerText;
                    string lastName = node["LastName"].InnerText;
                    int age = int.Parse(node["Age"].InnerText);
                    string email = node["Email"].InnerText;

                    persons.Add(new Person(firstName, lastName, age, email));               
                }
                return persons;
            }
            
        }          
    }
}
