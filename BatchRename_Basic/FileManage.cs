using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename_Basic
{
    public class FileInformation : INotifyPropertyChanged
    {
        public string filePath { get; set; }
        public string fileName { get; set; }
        //Name without extension
        public string realName { get; set; }
        public string originalExtension { get; set; }
        public string newExt { get; set; }
        public string newName { get; set; }
        public string fileError { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdatePreview()
        {
            RaiseChangeEvent("newName");
            RaiseChangeEvent("newExt");
        }
        //public void UpdatePreview(string originName)
        //{
        //    this.filePath.Replace(originName, fileName);
        //    RaiseChangeEvent("filePath");
        //    RaiseChangeEvent("fileName");
        //    RaiseChangeEvent("fileExtension");
        //}

        void RaiseChangeEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FolderInformation : INotifyPropertyChanged
    {
        public string folderPath { get; set; }
        public string folderName { get; set; }
        public string newName { get; set; }
        public string folderError { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        void RaiseChangeEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public void UpdatePreview()
        {
            RaiseChangeEvent("newName");
        }
    }
}
