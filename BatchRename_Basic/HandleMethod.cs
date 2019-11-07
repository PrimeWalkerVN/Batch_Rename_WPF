using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic
{
    public delegate void UpdateEventHandler(object sender, UpdateEvent e);
    public class UpdateEvent : EventArgs
    {
    }

    public interface IMethodArgs
    {
        string Type { get; }
    }

    public interface IMethodAction
    {
        event UpdateEventHandler newNameEvent;
        string MethodName { get; set; }
        bool IsChecked { get; set; }

        List<string> ApplyTo { get; }
        bool isApplyToName { get; set; }
        IMethodArgs methodArgs { get; set; }
        string Description { get; }
        string Process(string origin);
        void ShowUpdateDetailWindow();
    }
}
