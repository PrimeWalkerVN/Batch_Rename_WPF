using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic.Features
{
    public delegate string StringProcessor(string origin);
    public interface StringArgs
    {
        string Details
        {
            get; set;
        }

        string ParseArgs();
    }

    public interface StringAction
    {
        string Name {get; set;}
        StringProcessor Processor { get; set; }
        StringArgs Args { get; set; }

        StringAction Clone();

        void ShowEditDialog();
    }
}
