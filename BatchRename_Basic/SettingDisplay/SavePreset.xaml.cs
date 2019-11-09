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

namespace BatchRename_Basic.SettingDisplay
{
    /// <summary>
    /// Interaction logic for SavePreset.xaml
    /// </summary>
    public partial class SavePreset : Window
    {
        public string PresetName { get; set; }
        public SavePreset()
        {
            InitializeComponent();
        }

        private void OnSave_Click(object sender, RoutedEventArgs e)
        {
            PresetName = SavePresetTextBox.Text;
            this.DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
