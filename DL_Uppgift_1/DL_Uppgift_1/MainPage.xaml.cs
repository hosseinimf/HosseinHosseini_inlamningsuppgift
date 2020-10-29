using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using static SharedLibrary.Models.Person; 
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using SharedLibrary.Models;
using SharedLibrary.Services;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DL_Uppgift_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SharedLibrary.Models.ViewModel viewModel { get; set; }

        TxtServices service = new TxtServices();
        CsvServices csvService = new CsvServices();
        JsonServices jsonService = new JsonServices();
        TxtServices txtService = new TxtServices();
        XmlServices xmlServices = new XmlServices();
        List<string> mainList = new List<string>();
        
        public MainPage()
        {
            this.InitializeComponent();
            viewModel = new ViewModel();            
        }

        //-------------------------------------------------------------------------------------Create and Write to files
        private async void btnCreateTxtFile_Click(object sender, RoutedEventArgs e)
        {
            await txtService.CreateTxtFileAsync();           
            await txtService.WriteToTxtFileAsync(new Person(tbFirstN.Text, tbLastN.Text, Convert.ToInt32(tbAge.Text), tbEmail.Text));
        }

        private async void btnCreateCsvFile_Click(object sender, RoutedEventArgs e)
        {
            await csvService.CreateCsvFileAsync();            
            await csvService.WriteToCsvFileAsync(new Person(tbFirstN.Text, tbLastN.Text, Convert.ToInt32(tbAge.Text), tbEmail.Text));            
        }

        private async void btnCreateJsonFile_Click(object sender, RoutedEventArgs e)
        {
            await JsonServices.CreateJsonFileAsync();
            await JsonServices.WriteToJsonFileAsync(new Person(tbFirstN.Text, tbLastN.Text, Convert.ToInt32(tbAge.Text), tbEmail.Text));
        }

        private async void btnCreateXmlFile_Click(object sender, RoutedEventArgs e)
        {
            await xmlServices.CreateXmlFileAsync();
            xmlServices.WriteToXmlFileAsync(new Person(tbFirstN.Text, tbLastN.Text, Convert.ToInt32(tbAge.Text), tbEmail.Text));
        }


        //-------------------------------------------------------------------------------------Read from Files

        private async void btnReadFromJsonFile_Click(object sender, RoutedEventArgs e)
        {
            await PopulateJsonPersonViewModel("jsonuppgift_1.json");
        }

        private async void btnReadFromCsvFile_Click(object sender, RoutedEventArgs e)
        {
            await PopulateCsvPersonViewModel("csvuppgift_1.csv");             
        }

        private async void btnReadFromTxtFile_Click(object sender, RoutedEventArgs e)
        {
            await PopulateTxtPersonViewModel("txtuppgift_1.txt");
        }

        private async void btnReadFromXmlFile_Click(object sender, RoutedEventArgs e)
        {
            await PopulateXmlPersonViewModel("xmluppgift_1.xml");
        }

        //-------------------------------------------------------------------------------------Populate info

        public async Task PopulateJsonPersonViewModel(string fileName)
        {
            var personsJson = JsonConvert.DeserializeObject<ObservableCollection<Person>>(await JsonServices.ReadFromJsonFileAsync(fileName));
            
            foreach (var person in personsJson)
            {                
                viewModel.persons.Add(person);
            }
        }

        public async  Task PopulateCsvPersonViewModel(string fileName)
        {
            var personsCsv = new ObservableCollection<Person>(await csvService.ReadFromCsvFileAsync(fileName) );
            
            foreach (var person in personsCsv)
            {
                viewModel.persons.Add(person);
            }            
        }

        public async Task PopulateTxtPersonViewModel(string fileName)
        {
            var personsTxt = new ObservableCollection<Person>(await txtService.ReadFromTxtFileAsync(fileName));

            foreach (var person in personsTxt)
            {
                viewModel.persons.Add(person);
            }
        }

        public async Task PopulateXmlPersonViewModel(string fileName)
        {
            var personsXml = new ObservableCollection<Person>(await xmlServices.ReadFromXmlFileAsync(fileName));

            foreach (var person in personsXml)
            {
                viewModel.persons.Add(person);
            }
        }


        //-------------------------------------------------------------------------------------Open files

        private async void btnOpenJsonFile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".json");

            StorageFile storageFile = await openPicker.PickSingleFileAsync();

            if (storageFile != null)
            {
                var stream = await storageFile.OpenAsync(FileAccessMode.Read);
                using (StreamReader reader = new StreamReader(stream.AsStream()))
                {
                    //lvShowItem.Items.Add(reader.ReadToEnd());
                    tbres.Text = reader.ReadToEnd();
                }
            }
        }       
    }
}
 