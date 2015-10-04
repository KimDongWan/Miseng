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
using System.Windows.Shapes;
using Miseng.ViewModel;
using Miseng.Properties;
using Miseng.ViewModel.UICanvas;
using System.Runtime.InteropServices;
using System.Windows.Interop;
namespace Miseng.View
{
    /// <summary>
    /// newFileWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class newFileWindow : Window
    {
        private ControlViewModel myViewModel;
        private string path;
        private string targetName;
        private string fileType;

        enum FileType
        {
            html = 0,
            css = 1,
            js = 2
        }

        public newFileWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoaded);

        }

        private void ViewLoaded(object sender, RoutedEventArgs r)
        {
            myViewModel = this.DataContext as ControlViewModel;
        }

        private void click_OK_Btn_Handler(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            string basicSrc = "";
            path = myViewModel.FileTreeVM.CurrentTreeItem.Path;
            targetName = File_Name.Text;
            switch (SelectBox.SelectedIndex)
            {
                case (int)FileType.html: fileType = ".html"; basicSrc = Miseng.Properties.Resources.basicHtml; break;
                case (int)FileType.css: fileType = ".css"; basicSrc = Miseng.Properties.Resources.basicCSS; break;
                case (int)FileType.js: fileType = ".js"; basicSrc = Miseng.Properties.Resources.basicJS; break;
                default: fileType = ""; break;
            }

            if (string.IsNullOrEmpty(targetName))
            {
                result = MessageBox.Show("파일 이름이 비었습니다.", "오류");
                return;
            }
            if (string.IsNullOrEmpty(fileType))
            {
                result = MessageBox.Show("파일 타입을 선택하지 않았습니다.", "오류");
                return;
            }
            path += "\\" + targetName + fileType;
            if (System.IO.File.Exists(path))
            {
                result = MessageBox.Show("파일이 이미 존재합니다.", "오류");
                return;
            }

            System.IO.File.Create(path).Close();
            System.IO.File.WriteAllText(path, basicSrc);
            myViewModel.Update_ExplorerViewModel();
            this.Close();
        }
        private void click_CANCLE_Btn_Handler(object sender, RoutedEventArgs e)
        {
            this.Close();
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
