using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic.Features
{
    public delegate string StringProcessor(string origin);
    public interface IStringArgs
    {
        string Details
        {
            get; set;
        }

        string ParseArgs();
    }

    public interface IStringAction
    {
        string Name {get; set;}
        StringProcessor Processor { get; set; }
        IStringArgs Args { get; set; }

        IStringAction Clone();

        void ShowEditDialog();
    }
}
