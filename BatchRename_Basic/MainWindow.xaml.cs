using BatchRename_Basic.Features;
using BatchRename_Basic.SettingDisplay;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string ActivePresetFile = "";
        }

        // all global list
        ObservableCollection<StringAction> ListMethod = new ObservableCollection<StringAction>() {
            new ReplaceAction(),
            new ISBNAction(),
            new RemoveAction(),
            new CaseAction(),
            new NormalizeAction(),
            new ExtensionAction(),
            new GUIDAction()
        };

        ObservableCollection<FileInfomation> ListFile = new ObservableCollection<FileInfomation>();
        ObservableCollection<FileInfomation> ListFolder = new ObservableCollection<FileInfomation>();
        ObservableCollection<Preset> PresetList = new ObservableCollection<Preset>();

        public string ActivePresetFile { get; private set; }
        public string PresetName { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MethodsBox.ItemsSource = ListMethod;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ActionsListBox.Items.Clear();
            
            ListFile.Clear();
            FileTab.Items.Clear();
            ListFolder.Clear();
            FolderTab.Items.Clear();

        }

        //Start all method selected
        private void StartBatch_Click(object sender, RoutedEventArgs e)
        {
            // check if no method selected
            if(ActionsListBox.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("No method selected!");
                return;
            }

            if(ListFile.Count == 0 && ListFolder.Count == 0)
            {
                System.Windows.MessageBox.Show("No file (folder) selected!");
                return;
            }
            if (TabControlFile.IsSelected)
            {
                ProcessAllMethod_File();
            }
            else
            {
                ProcessAllMethod_Folder();
            }
        }

        private void ProcessAllMethod_File()
        {
            for (int i = 0; i < ListFile.Count; ++i)
            {
                FileInfomation tempFile = new FileInfomation
                {
                    FileName = ListFile[i].FileName,
                    Path = ListFile[i].Path
                };
                foreach (StringAction action in ActionsListBox.Items)
                {
                    tempFile.FileName = action.Operation.Invoke(tempFile.FileName);
                    ObservableCollection<FileInfomation> tempListFile = new ObservableCollection<FileInfomation>(ListFile);
                    tempListFile.Remove(tempListFile[i]);
                    foreach (FileInfomation file in tempListFile)
                    {
                        if (string.Compare(tempFile.FileName, file.FileName) == 0)
                        {
                            ListFile[i].Error = "File duplicate!";
                            ListFile[i].NewFileName = tempFile.FileName;
                        }
                    }
                    if(string.Compare(tempFile.FileName," ") == 0)
                    {
                        ListFile[i].Error = "Error name!";
                        break;
                    }
                }

                if(string.Compare(ListFile[i].Error, "File duplicate!") != 0)
                {
                    ListFile[i].NewFileName = tempFile.FileName;
                    ListFile[i].Error = "";
                    var temp = new FileInfo(ListFile[i].Path);
                    temp.MoveTo(Path.GetDirectoryName(ListFile[i].Path) + "\\" + tempFile.FileName);
                    FileTab.Items.Refresh();
                    
                //    File.Move(ListFile[i].Path, tempFile.FileName);
                }
            }
            System.Windows.MessageBox.Show("File Done!");
        }

        private void ProcessAllMethod_Folder()
        {
            for (int i = 0; i < ListFolder.Count; ++i)
            {
                FileInfomation tempFolder = new FileInfomation
                {
                    FileName = ListFolder[i].FileName,
                    Path = ListFolder[i].Path
                };
                foreach (StringAction action in ActionsListBox.Items)
                {
                    tempFolder.FileName = action.Operation.Invoke(tempFolder.FileName);
                    ObservableCollection<FileInfomation> tempListFolder = new ObservableCollection<FileInfomation>(ListFolder);
                    tempListFolder.Remove(tempListFolder[i]);
                    foreach (FileInfomation file in tempListFolder)
                    {
                        if (string.Compare(tempFolder.FileName, file.FileName) == 0)
                        {
                            ListFolder[i].Error = "Folder duplicate!";
                            ListFolder[i].NewFileName = tempFolder.FileName;
                        }
                    }
                    if (string.Compare(tempFolder.FileName, " ") == 0)
                    {
                        ListFolder[i].Error = "Error name!";
                        break;
                    }
                }

                if (string.Compare(ListFolder[i].Error, "File duplicate!") != 0)
                {
                    ListFolder[i].NewFileName = tempFolder.FileName;
                    ListFolder[i].Error = "";
                    var temp = new FileInfo(ListFolder[i].Path);
                    temp.MoveTo(Path.GetDirectoryName(ListFolder[i].Path) + "\\" + tempFolder.FileName);
                    FolderTab.Items.Refresh();

                    //    File.Move(ListFile[i].Path, tempFile.FileName);
                }
            }
            System.Windows.MessageBox.Show("Folder Done!");
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MethodsBox.SelectedItem == null)
            {

                System.Windows.MessageBox.Show("No method selected to add ?");
                return;
            }
            var methodSelected = MethodsBox.SelectedItem as StringAction;
            var instance = methodSelected.Clone();

            ActionsListBox.Items.Add(instance);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ActionsListBox.Items.Clear();
        }

        private void MoveTop_Click(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Please select one method!");
            }
            else
            {
                //int newPosition = ActionsListBox.SelectedIndex;
                //--newPosition;
                if (ActionsListBox.SelectedIndex == 0)
                    return;
                var item = ActionsListBox.SelectedItem;
                ActionsListBox.Items.Remove(item);
                ActionsListBox.Items.Insert(0, item);
                ActionsListBox.SelectedIndex = 0;
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            if(ActionsListBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Please select one method!");
            }
            else
            {
                int newPosition = ActionsListBox.SelectedIndex;
                --newPosition;
                if (newPosition < 0)
                    return;
                var item = ActionsListBox.SelectedItem;
                ActionsListBox.Items.Remove(item);
                ActionsListBox.Items.Insert(newPosition, item);
                ActionsListBox.SelectedIndex = newPosition;
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Please select one method!");
            }
            else
            {
                int newPosition = ActionsListBox.SelectedIndex;
                ++newPosition;
                if (newPosition >= ActionsListBox.Items.Count)
                    return;
                var item = ActionsListBox.SelectedItem;
                ActionsListBox.Items.Remove(item);
                ActionsListBox.Items.Insert(newPosition, item);
                ActionsListBox.SelectedIndex = newPosition;
            }
        }

        private void MoveBottom_Click(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Please select one method!");
            }
            else
            {
                //int newPosition = ActionsListBox.SelectedIndex;
                //--newPosition;
                if (ActionsListBox.SelectedIndex == ActionsListBox.Items.Count)
                    return;
                var item = ActionsListBox.SelectedItem;
                ActionsListBox.Items.Remove(item);
                ActionsListBox.Items.Insert(ActionsListBox.Items.Count, item);
                ActionsListBox.SelectedIndex = ActionsListBox.Items.Count - 1;
            }
        }

        private void LoadPreset_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog LoadFileDialog = new Microsoft.Win32.OpenFileDialog();
            LoadFileDialog.Filter = "All Files (*.txt)|*.txt";
            LoadFileDialog.Multiselect = true;


            if (LoadFileDialog.ShowDialog() == true)
            {

                ActionsListBox.Items.Clear();
                string selectecPresetFilePath = LoadFileDialog.FileName;

                ActivePresetFile = selectecPresetFilePath;

                using (StreamReader sr = new StreamReader(selectecPresetFilePath))
                {
                    string Line;

                    while ((Line = sr.ReadLine()) != null)
                    {
                        string presetname = Line;

                        ObservableCollection<StringAction> temp = new ObservableCollection<StringAction>();

                        while ((Line = sr.ReadLine()) != "--The end--")
                        {
                            if (Line.Contains("Replace"))
                            {
                                temp.Add(new ReplaceAction()
                                {
                                    Args = new ReplaceArgs(Line)
                                });
                            }
                            else
                            {
                                if (Line.Contains("Remove"))
                                {
                                    temp.Add(new RemoveAction()
                                    {
                                        Args = new RemoveArgs(Line)
                                    });
                                }
                                else
                                {
                                    if (Line.Contains("Extension"))
                                    {
                                        temp.Add(new ExtensionAction()
                                        {
                                            Args = new ExtensionArgs(Line)
                                        });
                                    }
                                    else
                                    {
                                        if (Line.Contains("Case"))
                                        {
                                            temp.Add(new CaseAction()
                                            {
                                                Args = new CaseArgs(Line)
                                            });
                                        }
                                        else
                                        {
                                            if (Line.Contains("Normalize"))
                                            {
                                                temp.Add(new NormalizeAction()
                                                {
                                                    Args = new NormalizeArgs()
                                                });
                                            }
                                            else
                                            {
                                                if (Line.Contains("ISBN"))
                                                {
                                                    temp.Add(new ISBNAction()
                                                    {
                                                        Args = new ISBNArgs()
                                                    });
                                                }
                                                else
                                                {
                                                    if (Line.Contains("GUID"))
                                                    {
                                                        temp.Add(new GUIDAction()
                                                        {
                                                            Args = new GUIDArgs()
                                                        });
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        PresetList.Add(new Preset()
                        {
                            PresetName = presetname,
                            ListPresetItem = temp
                        });
                    }
                }
            }

            PresetBox.ItemsSource = PresetList;
        }

        private void SavePreset_Click(object sender, RoutedEventArgs e)
        {
            if (ActionsListBox.Items.Count == 0)
            {
                System.Windows.MessageBox.Show("Method empty!");
                return;
            }

            var presetDialog = new SavePreset();
            if (presetDialog.ShowDialog() == true)
            {
                string PresetName = presetDialog.PresetName;

                if (!string.IsNullOrEmpty(ActivePresetFile))
                {
                    using (StreamWriter sw = File.AppendText(ActivePresetFile))
                    {
                        sw.WriteLine(PresetName);

                        foreach (StringAction action in ActionsListBox.Items)
                        {
                            string methodTemplate = $"{action.Name}/{action.Args.ParseArgs()}";
                            sw.WriteLine(methodTemplate);
                        }

                        sw.WriteLine("--The end--");
                    }
                    System.Windows.MessageBox.Show($"Preset saved in {ActivePresetFile}");
                }
                else
                {
                    // preset will save in the same .exe file
                    string presetFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string presetFilePath = presetFolderPath + @"\" + PresetName + ".txt";

                    if (!Directory.Exists(presetFolderPath)) Directory.CreateDirectory(presetFolderPath);
                    
                    if (!File.Exists(presetFilePath))
                    {
                        using (StreamWriter sw = File.CreateText(presetFilePath))
                        {
                            sw.WriteLine(PresetName);

                            foreach (StringAction action in ActionsListBox.Items)
                            {
                                string methodTemplate = $"{action.Name}/{action.Args.ParseArgs()}";
                                sw.WriteLine(methodTemplate);
                            }
                            sw.WriteLine("--The end--");
                        }
                        System.Windows.MessageBox.Show($"Preset saved in {presetFilePath}");
                    }
                    else
                    
                    {
                        using (StreamWriter sw = File.AppendText(presetFilePath))
                        {
                            sw.WriteLine(PresetName);

                            foreach (StringAction action in ActionsListBox.Items)
                            {
                                string methodTemplate = $"{action.Name}/{action.Args.ParseArgs()}";
                                sw.WriteLine(methodTemplate);
                            }

                            sw.WriteLine("--The end--");

                        }
                        System.Windows.MessageBox.Show($"Preset saved in {presetFilePath}");
                    }
                }
            }
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
             var method = ActionsListBox.SelectedItem as StringAction;
            method.ShowEditDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ActionsListBox.SelectedItem;
            ActionsListBox.Items.Remove(selectedItem);
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
            ListFile.Clear();
            FileTab.Items.Clear();
        }

        private void MoveFileTop_Click(object sender, RoutedEventArgs e)
        {
            if (FileTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                // if selected item is on top then return
                if (FileTab.SelectedIndex == 0) return;                     
                object selected = FileTab.SelectedItem;
                FileTab.Items.Remove(selected);
                FileTab.Items.Insert(0, selected);
                FileTab.SelectedIndex = 0;
                // else, insert on top List
            }
        }

        private void MoveFileUp_Click(object sender, RoutedEventArgs e)
        {
            if (FileTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                int newIndex = FileTab.SelectedIndex - 1;
                if (newIndex < 0) return;
                object selected = FileTab.SelectedItem;
                FileTab.Items.Remove(selected);
                FileTab.Items.Insert(newIndex, selected);
                FileTab.SelectedIndex = newIndex;
            }
        }

        private void MoveFileDown_Click(object sender, RoutedEventArgs e)
        {

            if (FileTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                int newIndex = FileTab.SelectedIndex + 1;
                if (newIndex >= FileTab.Items.Count) return;
                object selected = FileTab.SelectedItem;
                FileTab.Items.Remove(selected);
                FileTab.Items.Insert(newIndex, selected);
                FileTab.SelectedIndex = newIndex;
            }
        }

        private void MoveFileBottom_Click(object sender, RoutedEventArgs e)
        {
            if (FileTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                // if item is at the bottom
                if (FileTab.SelectedIndex == FileTab.Items.Count) return;
                // else, insert item at the bottom
                object selected = FileTab.SelectedItem;
                FileTab.Items.Remove(selected);
                FileTab.Items.Insert(FileTab.Items.Count, selected);
                FileTab.SelectedIndex = FileTab.Items.Count - 1;
            }
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
            FolderTab.Items.Clear();
            ListFolder.Clear();
        }

        private void MoveFolderTop_Click(object sender, RoutedEventArgs e)
        {
            if (FolderTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                // if selected item is on top then return
                if (FolderTab.SelectedIndex == 0) return;
                object selected = FolderTab.SelectedItem;
                FolderTab.Items.Remove(selected);
                FolderTab.Items.Insert(0, selected);
                FolderTab.SelectedIndex = 0;
                // else, insert on top List
            }
        }

        private void MoveFolderUp_Click(object sender, RoutedEventArgs e)
        {
            if (FolderTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                int newIndex = FolderTab.SelectedIndex - 1;
                if (newIndex < 0) return;
                object selected = FileTab.SelectedItem;
                FolderTab.Items.Remove(selected);
                FolderTab.Items.Insert(newIndex, selected);
                FolderTab.SelectedIndex = newIndex;
            }
        }

        private void MoveFolderDown_Click(object sender, RoutedEventArgs e)
        {
            if (FolderTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                int newIndex = FolderTab.SelectedIndex + 1;
                if (newIndex >= FolderTab.Items.Count) return;
                object selected = FolderTab.SelectedItem;
                FolderTab.Items.Remove(selected);
                FolderTab.Items.Insert(newIndex, selected);
                FolderTab.SelectedIndex = newIndex;
            }
        }

        private void MoveFolderBottom_Click(object sender, RoutedEventArgs e)
        {
            if (FolderTab.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("Don't found file, try selected!");
            }
            else
            {
                // if item is at the bottom
                if (FolderTab.SelectedIndex == FolderTab.Items.Count) return;
                // else, insert item at the bottom
                object selected = FolderTab.SelectedItem;
                FolderTab.Items.Remove(selected);
                FolderTab.Items.Insert(FolderTab.Items.Count, selected);
                FolderTab.SelectedIndex = FolderTab.Items.Count - 1;
            }
        }

        private void FolderTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnChangePreset(object sender, EventArgs e)
        {
            if (PresetBox.SelectedIndex == -1) return;

            ActionsListBox.Items.Clear();
            ActionsListBox.ItemsSource = null;
            object selected = PresetBox.SelectedItem;
            foreach (StringAction act in (selected as Preset).ListPresetItem)
            {
                ActionsListBox.Items.Add(act);
            }
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
