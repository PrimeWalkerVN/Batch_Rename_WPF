using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic.Features
{
    public class Preset
    {
        public string PresetName { get; set; }
        public ObservableCollection<StringAction> ListPresetItem = null;
    }
}
