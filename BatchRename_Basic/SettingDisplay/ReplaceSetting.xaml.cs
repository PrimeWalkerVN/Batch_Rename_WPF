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
    /// Interaction logic for ReplaceSetting.xaml
    /// </summary>
    public partial class ReplaceSetting : Window
    {
        public string From;
        public string To;
        public ReplaceSetting(ReplaceArgs args)
        {
            InitializeComponent();
            FromTextBox.Text = args.From;
            ToTextBox.Text = args.To;
            
        }

        private void ReplaceOK_Click(object sender, RoutedEventArgs e)
        {
            string SpecialChars = @"""<>/\|:?!";
            for (int i = 0; i < SpecialChars.Length; i++)
            {
                if (FromTextBox.Text.Contains(SpecialChars[i]) || ToTextBox.Text.Contains(SpecialChars[i])) {
                    MessageBox.Show("Invalid!Cause your Name input contains special char,We'll set 'Default', try again....");
                    FromTextBox.Text = "Default";
                    ToTextBox.Text = "Default";
                }              
            }

            if (string.IsNullOrWhiteSpace(FromTextBox.Text) || string.IsNullOrEmpty(FromTextBox.Text)
                    || string.IsNullOrEmpty(ToTextBox.Text) || string.IsNullOrWhiteSpace(ToTextBox.Text))
            {
                MessageBox.Show("File name can't be empty or white spaces.");
                FromTextBox.Text = "Default";
                ToTextBox.Text = "Default";

            }
            From = FromTextBox.Text;
            To = ToTextBox.Text;
            this.DialogResult = true;
        }

        private void CancelReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
