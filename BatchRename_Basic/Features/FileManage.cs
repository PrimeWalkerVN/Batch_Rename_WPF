using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic.Features
{
    public class FileInfomation : INotifyPropertyChanged
    {
        private string fileName;
        private string newFileName;
        private string filePath;
        private string fileError;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyChange(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                NotifyChange("FileName");
            }
        }

        public string NewFileName
        {
            get => newFileName;
            set
            {
                newFileName = value;
                NotifyChange("NewFileName");
            }
        }

        public string Path
        {
            get => filePath;
            set
            {
                filePath = value;
                NotifyChange("Path");
            }
        }

        public string Error
        {
            get => fileError;
            set
            {
                fileError = value;
            }
        }

        public FileInfomation Clone()
        {
            FileInfomation f = new FileInfomation();
            f.fileName = this.fileName;
            f.newFileName = this.newFileName;
            f.filePath = this.filePath;
            f.fileError = this.fileError;

            return f;
        }

        public void UpdatePreview()
        {
            NotifyChange("newFileName");
        }
    }
}
