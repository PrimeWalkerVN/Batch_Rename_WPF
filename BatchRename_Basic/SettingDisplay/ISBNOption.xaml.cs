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
using System.Windows.Shapes;
using BatchRename_Basic.Features;

namespace BatchRename_Basic.SettingDisplay
{
    /// <summary>
    /// Interaction logic for ISBNOption.xaml
    /// </summary>
    public partial class ISBNOption : Window
    {
        public string Optional; // User choose
        public ISBNOption(ISBNArgs Args)
        {
            InitializeComponent();
            Optional = Args.Optional;
        }

        private void ISBNOK_Click(object sender, RoutedEventArgs e)
        {
            if (BeforeRadio.IsChecked == true)
            {
                Optional = "Before";
                this.DialogResult = true;
            }
            else if (AfterRadio.IsChecked == true)
            {
                Optional = "After";
                this.DialogResult = true;
            }
            else {
                MessageBox.Show("No option selected, please choose position...");
            }
        }

        private void CancelISBNButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
