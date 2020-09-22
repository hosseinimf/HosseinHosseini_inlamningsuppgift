using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf_uppgift_2.Models;

namespace wpf_uppgift_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnMessage_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new MessageModel();
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ContactModel();
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
