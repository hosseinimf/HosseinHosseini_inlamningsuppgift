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
using wpf_uppgift_2.Models;

namespace wpf_uppgift_2.Views
{
    /// <summary>
    /// Interaction logic for MessageViewModel.xaml
    /// </summary>
    public partial class MessageViewModel : UserControl
    {
        public MessageViewModel()
        {
            InitializeComponent();
        }

        private void btnmessage1_MouseEnter(object sender, MouseEventArgs e)
        {
            from.Text = "From Hossein Hosseini";
            to.Text = "To Amirali Hosseini";
            text.Text = "En man som heter Ove är en roman som skriven av svensk författare Fredrik Backmans.\n" +
                " Romanen handlar om Oves liv båda samtiden och tillbakablickar av stora händelser " +
                "\nsom påverkade hans liv och humor. Han har upplevt mycket sorg. Han är 59 år gammal, har en gammal Saab och bor i ett radhus.\n" +
                " Som karaktär beskrivs han seriös, bitter, ärlig och aggressiv och Han är stenhård när det gäller regler\n till exempel runt om kring huset. ";            
        }

       
        private void btnMessage2_MouseEnter(object sender, MouseEventArgs e)
        {
            from.Text = "From Peter Andersson";
            to.Text = "To Hossein Hosseini";
            text.Text = "Ove och Sonja blev kär i varandra efter första träff och därefter fick de\n" +
                " träffa mer och efter ett tag de gifte sig. Sonja blev rullstolsbunden på grund av \n" +
                "en bussolycka men tröts att detta fick hon jobb som lärare med hjälp av Ove. Ove och\n" +
                " Sonja bodde länge tillsammans och hade perfekt liv i radhuset. ";
        }
    }
}
