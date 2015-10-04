using System;
using System.Collections.Generic;
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
using System.IO;
using Miseng.ViewModel.UICanvas;

namespace Miseng.View.Preview
{
    public partial class PreviewImage : UserControl
    {
        ControlViewModel ctrVM;
        public PreviewImage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
                ctrVM._previewImageV = this;
            }
        }

        private void NewProejctButtonClick(object sender, RoutedEventArgs e)
        {
            Window childWindow = new NewProjectWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

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

        private void TutorialButtonClick(object sender, RoutedEventArgs e)
        {
            Window childWindow = new TutorialWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            ctrVM._ucaVM.DomtreeVM.saveHtml();
        }

        private void RefreshButtonClick(object sender, RoutedEventArgs e)
        {
            ctrVM.FileTreeVM.UpDateFileExplorerViewModel();
        }
    }
}