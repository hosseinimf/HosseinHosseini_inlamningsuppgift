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
    /// Interaction logic for ContactControl3.xaml
    /// </summary>
    public partial class ContactControl3 : UserControl
    {
        public ContactControl3()
        {
            InitializeComponent();
        }

        public string ContactName3
        {
            get { return contactName3.Text; }
            set { contactName3.Text = value; }
        }

        public string Description
        {
            get { return description.Text; }
            set { description.Text = value; }
        }
        public string MessageDate
        {
            get { return messageDate.Text; }
            set { messageDate.Text = value; }
        }
        public string MessageTime
        {
            get { return messageTime.Text; }
            set { messageTime.Text = value; }
        }
    }
}
