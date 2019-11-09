using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchRename_Basic.SettingDisplay;
using BatchRename_Basic.Features;
using System.Text.RegularExpressions;

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
        public string Name => "ISBN Move";
        public StringOperation Operation => _option;
        public StringArgs Args { get; set; }
        

        private string _option(string origin)
        {

            var isbnArgs = Args as ISBNArgs;
            if (isbnArgs.Optional == "Before")
            {
                string isbn = origin.Substring(0, 13);
                string name = origin.Substring(15);
                return $"{isbn} {name}";
            }
            else if (isbnArgs.Optional == "After")
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

    //====================New Case Action Area===============================

    public class CaseArgs : StringArgs, INotifyPropertyChanged
    {

        private string _casetype; // upper case, lower case, title case

        public string ParseArgs()
        {
            return CaseType;
        }
        public CaseArgs() { }
        public CaseArgs(string details)
        {
            string[] word = details.Split('/');
            CaseType = word[1];
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Details => $"Change name to {CaseType} ";
        public int GetCaseType()
        {
            if (_casetype == "UpperCase") return 1;
            else if (_casetype == "LowerCase") return 2;
            return 3;
        }
        public void NotifyChange(string eventStr)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(eventStr));
            }
        }

        public string CaseType
        {
            get => _casetype; set
            {
                _casetype = value;
                NotifyChange("CaseType");
                NotifyChange("Details");
            }
        }

    }

    public class CaseAction : StringAction
    {
        public string Name => "New case";
        public StringArgs Args { get; set; }


        private string _changecase(string origin)
        {
            var caseArgs = Args as CaseArgs;

            int casetype = caseArgs.GetCaseType();
            if (casetype == 1)
            {
                return origin.ToUpper();
            }
            else if (casetype == 2)
            {
                return origin.ToLower();

            }
            else
            {
                Char FirstChar = char.ToUpper(origin[0]);
                String PartRemain = origin.Substring(1).ToLower();

                return FirstChar + PartRemain ;

            }
        }
        public StringOperation Operation => _changecase;

        public StringAction Clone()
        {
            return new CaseAction()
            {
                Args = new CaseArgs()
            };
        }

        public void ShowEditDialog()
        {
            var screen = new NewCase(Args as CaseArgs);
            if (screen.ShowDialog() == true)
            {
                var caseArgs = Args as CaseArgs;

                caseArgs.CaseType = screen.CaseChoose;
            }
        }
    }
    //====================End Case Action Area===============================


    //====================Full name normalize Area===========================
    public class NormalizeArgs : StringArgs
    {
        public string Details => $"Normalize name";

        public string ParseArgs() { return ""; }
    }
    public class NormalizeAction : StringAction
    {
        public string Name => "Normalize action";
        public StringArgs Args { get; set; }
        public StringOperation Operation => _normalize;
        private string _normalize(string origin)
        {

            //find and matches regular expression then trim them
            string trimmed = origin.Trim();
            trimmed = Regex.Replace(trimmed, @"[ \t]+", " ");
            

            //Split and Combine part 
            string res = "";
            string[] words = trimmed.Split(' ');
            foreach (var word in words)
            {
                res = res + char.ToUpper(word[0]) + word.Substring(1).ToLower() + " ";
            }

            //Remove dot out of string 
            int dot = res.LastIndexOf('.');
            if (res[dot - 1] == ' ') res=res.Remove(dot - 1, 1);

            return res;

        }
        public StringAction Clone()
        {
            return new NormalizeAction()
            {
                Args = new NormalizeArgs()
            };
        }
        public void ShowEditDialog()
        {
            return;
            // Because Dialog not used thus do not anything
        }
    }
    //====================End Full name normalize Area=======================

    //====================Extension Area==============================

    public class ExtensionArgs : StringArgs, INotifyPropertyChanged
    {
        private string _newextension;

        public string ParseArgs()
        {
            return NewExtension;
        }
        public ExtensionArgs() { }
        public ExtensionArgs(string details)
        {
            // extract args
            string[] words = details.Split('/');
            NewExtension = words[1];
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Details => $"Change extenstion to .{NewExtension}";

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }
        public string NewExtension
        {
            get => _newextension; set
            {
                _newextension = value;
                NotifyChange("New extension");
                NotifyChange("Details");

            }
        }
    }
    public class ExtensionAction : StringAction
    {
        public string Name => "Extension Change";
        private string _changeExtension(string origin)
        {
            var extArgs = Args as ExtensionArgs;   
            var newExt = extArgs.NewExtension;
            var foundPosition = origin.LastIndexOf(".");
            var beginning = origin.Substring(0, foundPosition);

            //Remove Special Char
            var charsToRemove = new string[] { "@", ",", ".", ";", "'" };
            foreach (var c in charsToRemove)
            {
                 newExt= newExt.Replace(c, string.Empty);
            }


            return $"{beginning}.{newExt}";
        }
        public StringOperation Operation { get => _changeExtension; }
        public StringArgs Args { get; set; }
        public StringAction Clone()
        {
            return new ExtensionAction()
            {
                Args = new ExtensionArgs()
            };
        }
        public void ShowEditDialog()
        {
            var screen = new ExtensionSetting(Args as ExtensionArgs);
            if (screen.ShowDialog() == true)
            {
                var extensionArgs = Args as ExtensionArgs;
                extensionArgs.NewExtension = screen.NewExtension;
            }
        }
    }
    //====================End extension Area==========================

    //====================GUID Area===================================

    public class GUIDArgs : StringArgs
    {
        public string Details => "GUID Generate";

        public string ParseArgs()
        {
            return "";
        }
    }
    public class GUIDAction : StringAction
    {
        public string Name => "GUID action";
        public StringArgs Args { get; set; }

        public StringOperation Operation => _generating;
        private string _generating(string origin)
        {
            var extPos = origin.LastIndexOf('.');
            var global = Guid.NewGuid();
            if (extPos == -1) return global.ToString();
            return $"{global.ToString()}.{origin.Substring(extPos + 1)}";
        }
        public StringAction Clone()
        {
            return new GUIDAction()
            {
                Args = new GUIDArgs()
            };
        }
        public void ShowEditDialog()
        {
            return;
            // nothing to do with this.
        }
    }
    //====================End GUID Area===============================

}
