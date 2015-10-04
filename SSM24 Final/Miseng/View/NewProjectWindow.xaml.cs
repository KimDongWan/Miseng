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
using System.IO;
using Miseng.ViewModel;
using System.Xml.Linq;
using System.Xml;
using System.Runtime.InteropServices;
using System.Windows.Interop;
namespace Miseng.View
{
    /// <summary>
    /// NewProjectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        Miseng.ViewModel.UICanvas.ControlViewModel myViewModel;


        public NewProjectWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoaded);

        }
        private void ViewLoaded(object sender, RoutedEventArgs r)
        {
            myViewModel = this.DataContext as Miseng.ViewModel.UICanvas.ControlViewModel;
        }

        private void newProject_OK_Event_Handler(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            string projectName = _projectName.Text;
            string _newProjectPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace";
            string _hiddenProjectTarget = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject";
            string tempPath = _newProjectPath;
            _newProjectPath = _newProjectPath + "\\" + projectName;
            if (string.IsNullOrEmpty(projectName))
            {
                result = MessageBox.Show("이름이 비었습니다", "오류");
                return;
            }
            if (System.IO.Directory.Exists(_newProjectPath))
            {
                result = MessageBox.Show("이미 같은 프로젝트가 존재합니다.", "오류");
                return;
            }
            DirInfo.DirectoryCopy(_hiddenProjectTarget, _newProjectPath, true);
            DirectoryInfo di = Directory.CreateDirectory(_newProjectPath + "\\hiddenFile");  //or whatever 
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\androidLibs");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\androidRes");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\androidSrc");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\config");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\opendForder");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            di = new DirectoryInfo(_newProjectPath + "\\sapReference");
            di.Attributes = di.Attributes | FileAttributes.Hidden;
            Directory.Move(Path.Combine(_newProjectPath, "Tizen\\basicProject"), Path.Combine(_newProjectPath, "Tizen\\" + projectName));
            Directory.Move(Path.Combine(_newProjectPath, "Android\\basicProject"), Path.Combine(_newProjectPath, "Android\\" + projectName));

            projectNameChanged(projectName, _newProjectPath, tempPath);

            myViewModel.Update_ExplorerViewModel();
            this.Close();
        }
        protected XmlNode CreateNode(XmlDocument xmlDoc, string name, string innerXml)
        {
            XmlNode node = xmlDoc.CreateElement(string.Empty, name, string.Empty);
            node.InnerXml = innerXml;

            return node;
        }


        private void projectNameChanged(string _projectName, string __newProjectPath, string _tempPath)
        {
            
            string npath = __newProjectPath + "\\Tizen\\" + _projectName + "\\.project";
            string basicPath = _tempPath + "\\basicMissengProject\\config\\.project";
            string config = __newProjectPath + "\\Tizen\\" + _projectName + "\\config.xml";
            string basicConfig =  _tempPath + "\\basicMissengProject\\config\\config.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(basicPath);
            XmlNode node = xmlDoc.DocumentElement;
            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name == "name")
                {
                    node1.InnerText = _projectName;
                    break;
                }
            }
            xmlDoc.Save(npath);

            xmlDoc.Load(basicConfig);
            node = xmlDoc.DocumentElement;
            XmlAttributeCollection acxNode = node.Attributes;

            if (acxNode.GetNamedItem("id") != null)
            {
                acxNode.GetNamedItem("id").Value = _projectName;
            }

            foreach (XmlNode xn in node.ChildNodes) {
                if (xn.Name == "tizen:application")
                {
                    acxNode = xn.Attributes;
                    string tempId = acxNode.GetNamedItem("id").Value;
                    tempId = tempId.Split('.')[0];
                    if (acxNode.GetNamedItem("id") != null)
                    {
                        acxNode.GetNamedItem("id").Value = tempId + "." + _projectName;
                    }
                }
                else if (xn.Name == "name")
                {
                    xn.InnerText = _projectName;
                    break;
                }
            }
            xmlDoc.Save(config);         
        }


        private void newProject_CANCLE_Event_Handler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void OnDragMoveWindow(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void OnMinimizeWindow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
        }
        private void OnCloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //Attach this to the MouseDown event of your drag control to move the window in place of the title bar
        private void WindowDrag(object sender, MouseButtonEventArgs e) // MouseDown
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle,
                0xA1, (IntPtr)0x2, (IntPtr)0);
        }

        //Attach this to the PreviewMousLeftButtonDown event of the grip control in the lower right corner of the form to resize the window
        private void WindowResize(object sender, MouseButtonEventArgs e) //PreviewMousLeftButtonDown
        {
            HwndSource hwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            SendMessage(hwndSource.Handle, 0x112, (IntPtr)61448, IntPtr.Zero);
        }

    }
}
