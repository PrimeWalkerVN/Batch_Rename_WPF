using BatchRename_Basic.Features;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;



namespace BatchRename_Basic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // all global list
        ObservableCollection<IStringAction> ListMethod = new ObservableCollection<IStringAction>();
        ObservableCollection<FileInfomation> ListFile = new ObservableCollection<FileInfomation>();
        ObservableCollection<FileInfomation> ListFolder = new ObservableCollection<FileInfomation>();
        //preset here

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartBatch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveTop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveBottom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadPreset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFileBrowser_Click(object sender, RoutedEventArgs e)
        {

            var browserFile = new FolderBrowserDialog();
            DialogResult result = browserFile.ShowDialog();

            if(result == System.Windows.Forms.DialogResult.OK)
            {
                string[] listFile = Directory.GetFiles(browserFile.SelectedPath);
                foreach(var f in listFile)
                {
                    FileInfomation file = new FileInfomation()
                    {
                        FileName = Path.GetFileName(f),
                        Path = f
                    };
                    int count = ListFile.Count;
                    bool fileAdded = false;
                    if (count != 0)
                    {
                        bool fileExist = false;
                        for (int i = 0; i < count; ++i)
                        {
                            if (string.Compare(ListFile[i].FileName, file.FileName) == 0)
                            {
                                System.Windows.MessageBox.Show(ListFile[i].FileName + " is already selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                fileExist = true;
                            }                            
                        }
                        if(!fileExist)
                        {
                            if (!fileAdded)
                            {
                                FileTab.Items.Add(file);
                                ListFile.Add(file);
                                fileAdded = true;
                               
                            }
                        }
                    }
                    else
                    {
                        if (!fileAdded)
                        {
                            FileTab.Items.Add(file);
                            ListFile.Add(file);
                        }
                    }
                }
                OnPreviewChange();
            }




            //OpenFileDialog browserFileDialog = new OpenFileDialog();
            //browserFileDialog.Multiselect = true;
            //FileInfo fileinfo = new FileInfo("C:/");

            //string filePathSelected;
            //string[] arrFilePathSelected;

            //if (browserFileDialog.ShowDialog() == true)
            //{
            //    filePathSelected = browserFileDialog.FileNames.ToString();
            //    arrFilePathSelected = browserFileDialog.FileNames;
            //}
            //else
            //{
            //    filePathSelected = string.Empty;
            //    return;
            //}

            //if (arrFilePathSelected.Length >= 2)
            //{
            //    for (int i = 0; i < arrFilePathSelected.Length; ++i)
            //    {
            //        fileinfo = new FileInfo(arrFilePathSelected[i]);
            //        if (fileinfo.Exists)
            //        {
            //            if (ListFileSelected.Count() == 0)
            //            {
            //                ListFileSelected.Add(new FileInformation()
            //                {
            //                    fileName = fileinfo.Name,
            //                    newName = fileinfo.Name,
            //                    realName = fileinfo.Name.Replace(fileinfo.Extension.Length != 0 ? fileinfo.Extension : " ", ""),
            //                    filePath = arrFilePathSelected[i],
            //                    originalExtension = fileinfo.Extension,
            //                    newExt = fileinfo.Extension,
            //                    fileError = "Good"
            //                });
            //                OnFileListChange();
            //            }
            //            //else
            //        }
            //    }
            //}
        }

        private void OnPreviewChange()
        {
            for(int i = 0; i < ListFile.Count; ++i)
            {
                ListFile[i].UpdatePreview();
            }
            for (int i = 0; i < ListFolder.Count; ++i)
            {
                ListFolder[i].UpdatePreview();
            }
        }

        private void ClearFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFileTop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFileUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFileDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFileBottom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FileTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenFolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            var browserFolder = new FolderBrowserDialog();
            DialogResult result = browserFolder.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string[] listFolder = Directory.GetDirectories(browserFolder.SelectedPath);

                foreach (var d in listFolder)
                {
                    FileInfomation folder = new FileInfomation()
                    {
                        FileName = new DirectoryInfo(d).Name,
                        Path = d
                    };

                    int count = ListFolder.Count;
                    bool folderAdded = false;
                    if (count != 0)
                    {
                        bool folderExisted = false;
                        for (int i = 0; i < count; ++i)
                        {
                            if (string.Compare(ListFolder[i].FileName, folder.FileName) == 0)
                            {
                                System.Windows.MessageBox.Show(ListFolder[i].FileName + " is already selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                                folderExisted = true;
                            }
                        }
                        if (!folderExisted)
                        {
                            if (!folderAdded)
                            {
                                FolderTab.Items.Add(folder);
                                ListFolder.Add(folder);
                                folderAdded = true;

                            }
                        }
                    }
                    else
                    {
                        if (!folderAdded)
                        {
                            FolderTab.Items.Add(folder);
                            ListFolder.Add(folder);
                        }
                    }
                }
            }
            OnPreviewChange();
        }

        private void ClearFolder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFolderTop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFolderUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFolderDown_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveFolderBottom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FolderTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        //Method list file/folder changed event
        /*
        public void OnFileListChange()
        {
            try
            {
                if (ListMethod.Count == 0)
                {
                    return;
                }
                OnMethodListChanged();
            }
            catch (Exception e)
            {
                //handle exception here;
            }
        }

        public void OnFolderListChange()
        {
            try
            {
                if (ListMethod.Count == 0)
                {
                    return;
                }
                OnMethodListChanged();
            }
            catch (Exception e)
            {
                //handle exception here;
            }
        }

        private void OnMethodListChanged()
        {
            try
            {
                for (int i = 0; i < ListFileSelected.Count; i++)
                {
                    foreach (var action in ListMethod)
                    {
                        if (action.IsChecked == true)
                        {
                            if (action.isApplyToName == true)
                            {
                                ListFileSelected[i].realName = action.Process(ListFileSelected[i].realName);
                                ListFileSelected[i].newName = ListFileSelected[i].realName + ListFileSelected[i].originalExtension;
                            }
                            else if (action.isApplyToName == false)
                            {
                                ListFileSelected[i].newExt = action.Process(ListFileSelected[i].newExt);
                                ListFileSelected[i].newName = ListFileSelected[i].newName.Replace(ListFileSelected[i].originalExtension, selectedFileList[i].newExt);
                            }
                        }
                    }
                    ListFileSelected[i].UpdatePreview();
                }
                replaceDuplicateFileName();
                for (int i = 0; i < ListFolderSelected.Count; i++)
                {
                    foreach (var action in ListMethod)
                    {
                        if (action.IsChecked == true)
                        {
                            ListFolderSelected[i].newName = action.Process(ListFolderSelected[i].newName);
                        }
                    }
                    ListFolderSelected[i].UpdatePreview();
                }
                replaceDuplicatrFolderName();
            }
            catch (Exception ex)
            {
                for (int i = 0; i < ListFileSelected.Count; i++)
                {
                    ListFileSelected[i].fileError = ex.ToString();
                }
            }

            

        }

        //Handle duplicate
        public void replaceDuplicateFileName()
        {
            for (int i = 0; i < ListFileSelected.Count - 1; i++)
            {
                //FileName Extension
                string ext = ListFileSelected[i].newExt;
                int k = 0;

                for (int j = i + 1; j < ListFileSelected.Count; j++)
                {
                    if (String.Compare(ListFileSelected[i].newName, ListFileSelected[j].newName) == 0)
                    {
                        if (isAppendSuffix == true)
                        {
                            int num = 1;
                            var tempNewName = $"{ListFileSelected[i].realName} ({num}){ext}";
                            while (k < ListFileSelected.Count)
                            {
                                if (String.Compare(tempNewName, ListFileSelected[k].newName) == 0)
                                {
                                    num++;
                                    tempNewName = $"{ListFileSelected[i].realName} ({num}){ext}";
                                    k = 0;
                                }
                                else
                                {
                                    k++;
                                }
                            }
                            ListFileSelected[j].newName = tempNewName;
                            k = 0;
                        }
                        else
                        {
                            ListFileSelected[j].newName = ListFileSelected[j].fileName;
                        }
                        ListFileSelected[j].UpdatePreview();
                    }
                }
            }
        }

        public void replaceDuplicatrFolderName()
        {
            for (int i = 0; i < ListFolderSelected.Count - 1; i++)
            {
                int k = 0;

                for (int j = i + 1; j < ListFolderSelected.Count; j++)
                {
                    if (String.Compare(ListFolderSelected[i].newName, ListFolderSelected[j].newName) == 0)
                    {
                        if (isAppendSuffix == true)
                        {
                            int num = 1;

                            var tempNewName = $"{ListFolderSelected[i].newName} ({num})";
                            while (k < ListFolderSelected.Count)
                            {
                                if (String.Compare(tempNewName, ListFolderSelected[k].newName) == 0)
                                {
                                    num++;
                                    tempNewName = $"{ListFolderSelected[i].newName} ({num})";
                                    k = 0;
                                }
                                else
                                {
                                    k++;
                                }
                            }
                            ListFolderSelected[j].newName = tempNewName;
                            k = 0;
                        }
                        else
                        {
                            ListFolderSelected[j].newName = ListFolderSelected[j].folderName;
                        }
                        ListFolderSelected[j].UpdatePreview();
                    }
                }
            }
        }
        */
    }
}
