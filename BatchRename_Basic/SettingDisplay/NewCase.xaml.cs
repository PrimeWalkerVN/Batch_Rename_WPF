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
    /// Interaction logic for NewCase.xaml
    /// </summary>
    public partial class NewCase : Window
    {

        public string CaseChoose;

        public NewCase(CaseArgs args)
        {
            InitializeComponent();

            CaseChoose = args.CaseType;
            if (CaseChoose == "UpperCase") UpperCase.IsChecked = true;
            else if (CaseChoose == "LowerCase") LowerCase.IsChecked = true;
            else TitleCase.IsChecked = true;
        }

        private void CaseSet_Click(object sender, RoutedEventArgs e)
        {
            if (UpperCase.IsChecked == true)
            {
                CaseChoose = "UpperCase";
                this.DialogResult = true;
            }
            else if (LowerCase.IsChecked == true)
            {
                CaseChoose= "LowerCase";
                this.DialogResult = true;
            }
            else if (TitleCase.IsChecked == true)
            {
                CaseChoose = "TitleCase";
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("No case option selected!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
