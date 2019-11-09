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
    /// Interaction logic for RemoveSetting.xaml
    /// </summary>
    public partial class RemoveSetting : Window
    {
        public string RemoveString;
      
        public RemoveSetting(RemoveArgs args)
        {
            InitializeComponent();
            StringRemove.Text = args.StringRemove;
          
        }

       

        private void RemmoveOK_Click(object sender, RoutedEventArgs e)
        {
            string SpecialChars = @"""<>/\|:?!";
            for (int i = 0; i < SpecialChars.Length; i++)
            {
                if (StringRemove.Text.Contains(SpecialChars[i]))
                {
                    MessageBox.Show("Invalid!Cause your Name input contains special char,We'll set 'Default', try again....");
                    StringRemove.Text = "Default";
                   
                }

            }
            if (string.IsNullOrWhiteSpace(StringRemove.Text) || string.IsNullOrEmpty(StringRemove.Text))
            {
                MessageBox.Show("File name can't be empty or white spaces.");
                StringRemove.Text = "Default";
               
            }
            RemoveString = StringRemove.Text;
            this.DialogResult = true;
        }



        private void CancelRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
