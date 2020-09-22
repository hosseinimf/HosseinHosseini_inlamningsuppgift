using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_uppgift_2.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl2.xaml
    /// </summary>
    public partial class ContactControl2 : UserControl
    {
        public ContactControl2()
        {
            InitializeComponent();
        }

        public ImageSource ContactImage
        {
            get { return contactImage.Source; }
            set { contactImage.Source = value; }
        }

        public string ContactName
        {
            get { return contactName.Text; }
            set { contactName.Text = value; }
        }

        public string ContactRole
        {
            get { return contactRole.Text; }
            set { contactRole.Text = value; }
        }
    }
}
