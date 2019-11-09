using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename_Basic.SettingDisplay;
using BatchRename_Basic.Features;

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
            get => $"Replace '{From}' with '{To}'";
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
            if (from == null || to == null) return " ";
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

    //====================ISBN Move Action Area =============================

    public class ISBNArgs : StringArgs, INotifyPropertyChanged
    {
        private string _optional;  // before/after

        public string Optional
        {
            get => _optional; set
            {
                _optional = value;
                NotifyChange("Option");
                NotifyChange("Details");
            }
        }
        public ISBNArgs() { }
        public ISBNArgs(string details)
        {
            string[] word = details.Split('/');
            Optional = word[1];
        }
        public string Details => $"Move ISBN {Optional} name";

       

        public string ParseArgs()
        {
            return Optional;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }
    }
    public class ISBNAction : StringAction
    {
        public string Name => "ISBN action";
        public StringOperation Operation => _option;
        public StringArgs Args { get; set; }
        

        private string _option(string origin)
        {

            var isbnArgs = Args as ISBNArgs;
            if (isbnArgs.Optional == "before")
            {
                string isbn = origin.Substring(0, 13);
                string name = origin.Substring(15);
                return $"{isbn} {name}";
            }
            else if (isbnArgs.Optional == "after")
            {
                string isbn = origin.Substring(0, 13);
                string name = origin.Substring(15);
                return $"{name} {isbn}";
            }

            return "error";
        }
        public StringAction Clone()
        {
            return new ISBNAction()
            {
                Args = new ISBNArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new ISBNOption(Args as ISBNArgs);
            if (screen.ShowDialog() == true)
            {
                var args = Args as ISBNArgs;
                args.Optional = screen.Optional;
            }
        }
    }

    //====================End ISBN Move Action Area==========================


    //====================Remove Pattern Action Area=========================

    public class RemoveArgs : StringArgs, INotifyPropertyChanged
    {
        private string _stringRemove;
        private string _to = "";

        public RemoveArgs() { }

        public string ParseArgs()
        {
            return $"{StringRemove}/{To}";
        }
        public RemoveArgs(string details)
        {
            // extract args
            string[] word = details.Split('/');
            StringRemove = word[1];
           
        }
        public string StringRemove
        {
            get => _stringRemove; set
            {
                _stringRemove = value;
                NotifyChange("StringRemove");
                NotifyChange("Details");
            }
        }

        public string To
        {
            get => _to;
        }

        public string Details
        {
            get => $"Remove '{StringRemove}'";
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

    public class RemoveAction : StringAction
    {

        public string Name => "Remove Pattern";
        private string _removeString(string origin)
        {

            var RemoveArgs = Args as RemoveArgs;
            var stringRemove = RemoveArgs.StringRemove;
            var to = RemoveArgs.To;
            if (stringRemove == null) return " ";
            string result = origin.Replace(stringRemove, to);

            return result;
        }
        public StringOperation Operation => _removeString;
        public StringArgs Args { get; set; }
       

        public StringAction Clone()
        {
            return new RemoveAction()
            {
                Args = new RemoveArgs()
            };
        }
        public void ShowEditDialog()
        {
            var screen = new RemoveSetting(Args as RemoveArgs);

            if (screen.ShowDialog() == true)
            {
                var removeArgs = Args as RemoveArgs;
                removeArgs.StringRemove = screen.RemoveString;
                
            }
        }

    }

    //====================End Remove Pattern Action Area=====================


}
