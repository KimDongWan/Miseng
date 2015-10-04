using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Miseng.ViewModel;
using Miseng.View;
using Miseng.View.ExtendUIMaking;
using Miseng.ViewModel.UICanvas;
using System.IO;
using System.Xml.Linq;
using System.Xml;
namespace Miseng.View
{
    public partial class FileSystemTree : UserControl
    {
        #region // Private Variables
        private ControlViewModel myViewModel;
        private string copy_target_path;
        private bool is_cut;
        private string copy_target_name;
        private ImageInfo imageInfo;
        #endregion
        #region // .ctor
        public FileSystemTree()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoaded);
            copy_target_path = "";
            is_cut = false;
        }
        #endregion

        #region // Event Handlers
        private void ViewLoaded(object sender, RoutedEventArgs r)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                myViewModel = window.DataContext as ControlViewModel;
                (DirectoryTree.Items[0] as DirInfo).IsSelected = true;
                AddHotKeys();
            }

        }

        private void DirectoryTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            MessageBoxResult result1;
            myViewModel.FileTreeVM.CurrentTreeItem = DirectoryTree.SelectedItem as DirInfo;
            if (myViewModel.FileTreeVM.CurrentTreeItem.DirType == (int)ObjectType.File)
            {
                //파일 실행하는 부분
                
                if (myViewModel.FileTreeVM.CurrentTreeItem.Ext == ".html File")
                {
                    //동완이여기
                    string orderProjectPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject\\opendForder\\order.txt";
                    if (!File.Exists(orderProjectPath))
                    {
                        result1 = MessageBox.Show("order.txt가 존재하지 않습니다.", "오류");
                        return;

                    }
                    string reader = File.ReadAllText(orderProjectPath);
                    string result = myViewModel.current_projectName + "\r\n";
                    result += reader;
                    File.Delete(orderProjectPath);
                    FileStream str = File.Open(orderProjectPath, FileMode.OpenOrCreate);
                    byte[] StrByte = Encoding.UTF8.GetBytes(result);
                    str.Write(StrByte, 0, StrByte.Length);
                    str.Close();
                    //

                    Grid.SetZIndex(myViewModel._intro,0);
                    Grid.SetZIndex(myViewModel.fileTabV, 1);
                    string temp = (myViewModel.FileTreeVM.CurrentTreeItem.Path).ToString();
                    string[] words = temp.Split('\\');
                    int count = 0;
                    foreach (string word in words)
                    {
                        count++;
                    }
                    string temp2 = words[count - 1];
                    string[] words2 = temp2.Split('.');
                    myViewModel.CURRENT_SCENE = words2[0] + ".html";
                    myViewModel.current_Scene = words2[0];
                    myViewModel.current_projectName = words[count - 2];
                    myViewModel.projectAndfileName = words[count - 2] + "_" + words2[0];
                    myViewModel.SrcCodeVM.GetSrcOfPath(myViewModel.FileTreeVM.CurrentTreeItem.Path);                    
                    myViewModel.fileTabV.add_Tab_Item(myViewModel.FileTreeVM.CurrentTreeItem.Name.Replace(".html",""),myViewModel.FileTreeVM.CurrentTreeItem.Path);
                    myViewModel.fileTabV.file_path = myViewModel.FileTreeVM.CurrentTreeItem.Path;
                    
                }

            }
        }

        private void TreeView_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeNode = sender as TreeViewItem;
            if (currentTreeNode == null)
                return;

            if (currentTreeNode.ItemsSource == null)
                return;

            DirInfo parentDirectory = currentTreeNode.Header as DirInfo;
            if (parentDirectory == null)
                return;

            foreach (DirInfo d in currentTreeNode.ItemsSource)
            {
                //int objType = (int)ObjectType.File;
                //if (d.DirType == objType) System.Diagnostics.Process.Start(d.Path);
                if (myViewModel.CurrentDirectory.Path.Equals(d.Path))
                {
                    d.IsSelected = true;
                    d.IsExpanded = true;
                    break;
                }
            }
            e.Handled = true;
        }
        private void Loaded_TreeVIdw_Expand(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeNode = sender as TreeViewItem;
            if (currentTreeNode == null)
                return;

            if (currentTreeNode.ItemsSource == null)
                return;

            DirInfo parentDirectory = currentTreeNode.Header as DirInfo;
            if (parentDirectory == null)
                return;

            foreach (DirInfo d in currentTreeNode.ItemsSource)
            {
                //int objType = (int)ObjectType.File;
                //if (d.DirType == objType) System.Diagnostics.Process.Start(d.Path);
                if (myViewModel.CurrentDirectory.Path.Equals(d.Path))
                {

                    d.IsSelected = true;
                    d.IsExpanded = true;
                    break;
                }


                if (!String.IsNullOrEmpty(myViewModel.UpdateDirectory.Path))
                {
                    string[] pathArgs = myViewModel.UpdateDirectory.Path.Split(new String[] { "\\" }, StringSplitOptions.None);
                    string subPath = "";
                    foreach (string tempPath in pathArgs)
                    {
                        subPath += tempPath;
                        if (d.Path.Equals(subPath))
                        {
                            d.IsSelected = true;
                            d.IsExpanded = true;
                        }
                        subPath += "\\";
                    }
                }
            }

        }

        private void AddHotKeys()
        {
            try
            {
                RoutedCommand saveSettings = new RoutedCommand();
                saveSettings.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveSettings, cut_file_event_handler));

                saveSettings = new RoutedCommand();
                saveSettings.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveSettings, copy_file_event_handler));

                saveSettings = new RoutedCommand();
                saveSettings.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveSettings, paste_file_event_handler));

                saveSettings = new RoutedCommand();
                saveSettings.InputGestures.Add(new KeyGesture(Key.Delete));
                CommandBindings.Add(new CommandBinding(saveSettings, delete_file_event_handler));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void create_SrcFile_event_handler(object sender, RoutedEventArgs e)
        {
            Window childWindow = new newFileWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }
        private void create_Directory_event_handler(object sender, RoutedEventArgs e)
        {
            Window childWindow = new renameWindow(false);
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();

            //System.IO.Directory.CreateDirectory(myViewModel.CurrentDirectory+"\\"+System.IO.Path.GetRandomFileName());
        }
        private void rename_file_event_handler(object sender, RoutedEventArgs e)
        {
            Window childWindow = new renameWindow(true);
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }
        private void delete_file_event_handler(object sender, RoutedEventArgs e)
        {

            //핫키 등록
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(myViewModel.FileTreeVM.CurrentTreeItem.Path);
            System.IO.FileInfo fi = new System.IO.FileInfo(myViewModel.FileTreeVM.CurrentTreeItem.Path);


            if (dir.Exists)
            {
                dir.Delete(true);
            }
            if (fi.Exists)
                fi.Delete();
            myViewModel.Update_ExplorerViewModel();
        }
        private void copy_file_event_handler(object sender, RoutedEventArgs e)
        {
            //파일 복사
            //hotkey 만들고(Ctrl + C);

            copy_target_path = myViewModel.FileTreeVM.CurrentTreeItem.Path;
            copy_target_name = myViewModel.FileTreeVM.CurrentTreeItem.Name;
            is_cut = false;
        }
        private void cut_file_event_handler(object sender, RoutedEventArgs e)
        {
            //핫키 만들고(Ctrl + X);
            copy_target_path = myViewModel.FileTreeVM.CurrentTreeItem.Path;
            copy_target_name = myViewModel.FileTreeVM.CurrentTreeItem.Name;
            is_cut = true;

        }
        private void paste_file_event_handler(object sender, RoutedEventArgs e)
        {
            //핫키 만들고(Ctrl + V)
            if (string.IsNullOrEmpty(copy_target_path)) return;
            string destSrc = myViewModel.FileTreeVM.CurrentTreeItem.Path;

            //string destSrc = myViewModel.FileTreeVM.CurrentTreeItem.Name;
            //destSrc = myViewModel.FileTreeVM.CurrentTreeItem.Path.Remove(
            //myViewModel.FileTreeVM.CurrentTreeItem.Path.Length - myViewModel.FileTreeVM.CurrentTreeItem.Name.Length);
            destSrc = destSrc + "\\" + copy_target_name;

            if (myViewModel.FileTreeVM.CurrentTreeItem.DirType == (int)ObjectType.Directory || (myViewModel.FileTreeVM.CurrentTreeItem.DirType == (int)ObjectType.MyComputer))
                DirInfo.DirectoryCopy(copy_target_path, destSrc, true);
            else
                System.IO.File.Copy(copy_target_path, destSrc, true);

            if (is_cut)
            {
                new System.IO.FileInfo(copy_target_path).Delete();

            }
            is_cut = false;
            myViewModel.Update_ExplorerViewModel();


        }
        private void new_Project_event_handler(object sender, RoutedEventArgs e)
        {
            Window childWindow = new NewProjectWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }
        #endregion

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".*";
            dlg.Filter = "Text documents (.txt)|*.*";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filepath = dlg.FileName;
                string fileName = filepath.Split(new char[] {'\\'}).Last();
                imageInfo = new ImageInfo();
                imageInfo.ImageName = fileName;
                imageInfo.ImagePath = filepath;
                myViewModel._ucaVM.ImageCollection.Add(imageInfo);
                myViewModel.extendUIV.ImageCollection.ItemsSource = myViewModel._ucaVM.ImageCollection;

                if (!System.IO.File.Exists(myViewModel.CurrentDirectory.Path + "/" + fileName))
                    System.IO.File.Copy(filepath, myViewModel.CurrentDirectory.Path + "/" + fileName, false);

                myViewModel.FileTreeVM.UpDateFileExplorerViewModel();
            }
        }

        private void import_event_handler(object sender, RoutedEventArgs e)
        {
            string _targetPath="test";
            string _current_android_prj;
            System.Windows.Forms.FolderBrowserDialog Dialog = new System.Windows.Forms.FolderBrowserDialog();
            while (Dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                if((Dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)){
                    return ;
                }
                Dialog.Reset();               
            }

            _targetPath = Dialog.SelectedPath;
            Directory.CreateDirectory(_targetPath);
            _current_android_prj= myViewModel.CurrentDirectory.Path+"\\"+_targetPath.Split(new char[] {'\\'}).Last();
            DirInfo.DirectoryCopy(_targetPath, _current_android_prj, true);
            

            MessageBoxResult result1;
            string projectName = myViewModel.current_projectName;
            string meniPath = myViewModel.CurrentDirectory.Path + "\\" + _targetPath.Split(new char[] { '\\' }).Last() + "\\AndroidManifest.xml";
            if (!File.Exists(meniPath))
            {
                result1 = MessageBox.Show("menifest가 존재하지 않습니다.", "오류");
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(meniPath);
            XmlNode node = xmlDoc.DocumentElement;

            string applicationMenyPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject\\config\\applicationMeny.xml";
            XmlDocument applicationMenyDoc = new XmlDocument();
            applicationMenyDoc.Load(applicationMenyPath);
            XmlNode subNode = applicationMenyDoc.DocumentElement;
            XmlNode newNode = xmlDoc.ImportNode(subNode, true);
            int count = newNode.ChildNodes.Count;
            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name == "application")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        node1.AppendChild(newNode.ChildNodes[0]);
                    }
                    break;
                }
            }
            string userPermissionPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject\\config\\userPermission.xml";
            applicationMenyDoc = new XmlDocument();
            applicationMenyDoc.Load(userPermissionPath);
            subNode = applicationMenyDoc.DocumentElement;
            newNode = xmlDoc.ImportNode(subNode, true);

            for (int i = 0; i < 6; i++)
            {
                node.AppendChild(newNode.ChildNodes[0]);
            }

            xmlDoc.Save(meniPath);

            
            string basicMissengPrj = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject";
            string packageName = javaChange(_current_android_prj+"\\src");
            DirInfo.DirectoryCopy(basicMissengPrj + "\\androidSrc", _current_android_prj+"\\src", true);
            settingSAP(packageName, _current_android_prj+"\\src");
            addSourceToJava(_current_android_prj + "\\src\\", packageName);
            DirInfo.DirectoryCopy(basicMissengPrj + "\\androidLibs", _current_android_prj + "\\libs", true);
            DirInfo.DirectoryCopy(basicMissengPrj + "\\androidRes", _current_android_prj + "\\res", true);
            myViewModel.FileTreeVM.UpDateFileExplorerViewModel();
        }

        private void settingSAP(string _packageName, string envi)
        {
            MessageBoxResult result1;
            string path = envi + "\\com\\samsung\\android\\sdk\\accessory\\example\\helloaccessoryprovider\\HelloAccessoryProviderService.java";
            if (!File.Exists(path))
            {
                result1 = MessageBox.Show("HelloAccessoryProviderService.java가 존재하지 않습니다.", "오류");
                return;
            }

            string reader = File.ReadAllText(path);
            string result = reader.Replace("Miseng2cb5143be8d22c9a8562e6cc18f40009", _packageName);
            File.Delete(path);
            FileStream str = File.Open(path, FileMode.OpenOrCreate);
            byte[] StrByte = Encoding.UTF8.GetBytes(result);
            str.Write(StrByte, 0, StrByte.Length);
            str.Close();

        }

        private void addSourceToJava(string path, string _packageName)
        {
            MessageBoxResult result1;
            string[] arrPackageName = new string[20];
            arrPackageName = _packageName.Split('.');
            foreach (string paName in arrPackageName)
            {
                path += paName + "\\";
                
            }
            path += "UnityPlayerNativeActivity.java";

            if (!File.Exists(path))
            {
                result1 = MessageBox.Show("UnityPlayerNativeActivity.java가 존재하지 않습니다.", "오류");
                return;

            }
            string temp = null;
            string line;
            string addLine = null;
            bool flag = false;
            // string t = System.IO.File.ReadAllText(path);
            using (var reader = File.OpenText(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (addLine != null)
                    {
                        temp += addLine;
                        temp += "\r\n";
                        addLine = null;
                    }
                    if (!flag)
                    {
                        if (line.Contains("{"))
                        {
                            addLine = "public static String GearMsg=\"\";\r\n" +
                                "public static String PreGearMsg=\"\";\r\n" +
                                "public void SendToGearMSG(String notUsed) {\r\n" +
                                "if(GearMsg!=PreGearMsg)" +
                                "UnityPlayer.UnitySendMessage(\"MisengPlugin\", \"recieve_Data\", GearMsg);\r\n" +
                                "PreGearMsg = GearMsg;" +
                                "}\r\n" +
                                "public void RecieveFromUnityMSG(String msg){\r\n" +
                                "com.samsung.android.sdk.accessory.example.helloaccessoryprovider.HelloAccessoryProviderService.sendToMsg(msg);\r\n" +
                                "}\r\n";
                            flag = true;

                        }
                    }
                    temp += line;
                    temp += "\r\n";
                }
            }
          
            File.WriteAllText(path, temp);
        }

        private string javaChange(string path)
        {            
            string finalPath = null;
            finalPath = SearchTree(path);
            string[] finalArr = new string[20];
            finalArr = finalPath.Split('\\');
            bool flag = false;
            string completeStr = null;
            foreach (string arr in finalArr)
            {
                if (flag)
                {
                    completeStr += arr;
                    completeStr += '.';
                }
                if (arr == "src")
                {
                    flag = true;
                }
            }
            if (completeStr != null)
            {
                completeStr = completeStr.Remove(completeStr.Length - 1);
            }

            /*
            finalPath += "\\UnityPlayerNativeActivity.java";
            string line;
            if (!File.Exists(finalPath))
            {
                File.Create(path).Close();
            }
            using (var reader = File.OpenText(finalPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                }
            }
            */
            return completeStr;

        }

        public string SearchTree(string root)
        {
            string _finalPath = null;
            Stack<string> dirs = new Stack<string>(20);
            if (!Directory.Exists(root)) { return _finalPath; }
            dirs.Push(root);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e) { continue; } // 접근권한이 없으면 PASS
                catch (DirectoryNotFoundException e) { continue; } // 존재하지 않아도 PASS
                foreach (string str in subDirs)
                {
                    _finalPath = str;
                    dirs.Push(str);
                }
            }
            return _finalPath;
        }
    }
}
