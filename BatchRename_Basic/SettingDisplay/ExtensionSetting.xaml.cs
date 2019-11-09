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
    /// Interaction logic for ExtensionSetting.xaml
    /// </summary>
    public partial class ExtensionSetting : Window
    {
        public String NewExtension;

        public ExtensionSetting(ExtensionArgs args)
        {
            InitializeComponent();
            NewExtension = args.NewExtension;
        }

        private void ExOK_Click(object sender, RoutedEventArgs e)
        {
            string SpecialChars = @"""<>/\|:?!";
            foreach (char ch in SpecialChars)
            {
                if (ExtensionBox.Text.Contains(ch))
                {
                    MessageBox.Show("Invalid extension. Please try again. Auto set extension to 'Default'.");

                    ExtensionBox.Text = "Default";
                }
            }
            if (string.IsNullOrWhiteSpace(ExtensionBox.Text) || string.IsNullOrEmpty(ExtensionBox.Text))
            {
                MessageBox.Show("Extension can't be empty or white spaces.");

                ExtensionBox.Text = "Default";

            }
            NewExtension = ExtensionBox.Text;

            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
