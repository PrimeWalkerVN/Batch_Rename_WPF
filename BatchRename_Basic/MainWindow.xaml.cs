using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


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

        BindingList<FileInformation> ListFileSelected = new BindingList<FileInformation>();
        BindingList<FolderInformation> ListFolderSelected = new BindingList<FolderInformation>();
        BindingList<IMethodAction> ListMethod = new BindingList<IMethodAction>();

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
            OpenFileDialog browserFileDialog = new OpenFileDialog();
            browserFileDialog.Multiselect = true;
            FileInfo fileinfo = new FileInfo("C:/");

            string filePathSelected;
            string[] arrFilePathSelected;

            if (browserFileDialog.ShowDialog() == true)
            {
                filePathSelected = browserFileDialog.FileNames.ToString();
                arrFilePathSelected = browserFileDialog.FileNames;
            }
            else
            {
                filePathSelected = string.Empty;
                return;
            }

            if (arrFilePathSelected.Length >= 2)
            {
                for (int i = 0; i < arrFilePathSelected.Length; ++i)
                {
                    fileinfo = new FileInfo(arrFilePathSelected[i]);
                    if (fileinfo.Exists)
                    {
                        if (ListFileSelected.Count() == 0)
                        {
                            ListFileSelected.Add(new FileInformation()
                            {
                                fileName = fileinfo.Name,
                                newName = fileinfo.Name,
                                realName = fileinfo.Name.Replace(fileinfo.Extension.Length != 0 ? fileinfo.Extension : " ", ""),
                                filePath = arrFilePathSelected[i],
                                originalExtension = fileinfo.Extension,
                                newExt = fileinfo.Extension,
                                fileError = "OK"
                            });
                            OnFileListChange();
                        }
                        //else
                    }
                }
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
    }
}
