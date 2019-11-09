using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename_Basic.SettingDisplay;

namespace BatchRename_Basic.Features
{

    public delegate string StringOperation(string origin);
    public interface StringArgs
    {
        string Details
        {
            get;
        }

        string ParseArgs();
    }

    public interface StringAction
    {
        string Name {get;}

        StringOperation Operation { get; }
        StringArgs Args { get; set; }

        StringAction Clone();

        void ShowEditDialog();
    }




    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        private string _from= "";
        private string _to = "";

        public ReplaceArgs() { }

        public string ParseArgs()
        {
            return $"{From}/{To}";
        }
        public ReplaceArgs(string details)
        {
            // extract args
            string[] word = details.Split('/');
            From = word[1];
            To = word[2];
        }
        public string From
        {
            get => _from; set
            {
                _from = value;
                NotifyChange("From");
                NotifyChange("Details");
            }
        }

        public string To
        {
            get => _to; set
            {
                _to = value;
                NotifyChange("To");
                NotifyChange("Details");
            }
        }

        public string Details
        {
            get => $"Replace {From} with {To}";
        }     

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

    }


    //====================Replace Action Area ============================
    public class ReplaceAction : StringAction
    {

        public string Name => "Replace action";
        private string _replace(string origin)
        {

            var replaceArgs = Args as ReplaceArgs;
            var from = replaceArgs.From;
            var to = replaceArgs.To;
            if (from == null || from == null) return " ";
            string result = origin.Replace(from, to);

            return result;
        }
        public StringOperation Operation => _replace;
        public StringArgs Args { get; set; }
       

        public StringAction Clone()
        {
            return new ReplaceAction()
            {
                Args = new ReplaceArgs()
            };
        }
        public void ShowEditDialog()
        {
            var screen = new ReplaceSetting(Args as ReplaceArgs);

            if (screen.ShowDialog() == true)
            {
                var replaceArgs = Args as ReplaceArgs;
                replaceArgs.From = screen.From;
                replaceArgs.To = screen.To;

            }
        }

    }
    //====================End Action Area ============================


}
