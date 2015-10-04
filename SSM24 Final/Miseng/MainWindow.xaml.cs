using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Miseng.Model;
using Miseng.ViewModel.UIMaking;
using Miseng.ViewModel;
using System.Threading;
using Miseng.ViewModel.UICanvas;
using GalaSoft.MvvmLight;
using Miseng.View.FileTab;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Miseng.View;
namespace Miseng
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ControlViewModel controlVM;        
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            // 로고시간 길게
            //Thread.Sleep(1000);
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoaded);
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void ViewLoaded(object sender, RoutedEventArgs e)
        {            
            controlVM = new ControlViewModel();
            controlVM.fileTabV = _fileTabView;
            controlVM.tabControlsV = tabV;
            controlVM._intro = _intro;
            controlVM.writeFileStory();
           
            this.DataContext = controlVM;
            AddHotKeys();
        }
        private void AddHotKeys()
        {
            try
            {
                RoutedCommand saveSettings = new RoutedCommand();
                saveSettings.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveSettings, FileSave_event_handler));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void FileSave_event_handler(object sender, ExecutedRoutedEventArgs e)
        {
            controlVM._ucaVM.DomtreeVM.saveHtml();
            controlVM.SrcCodeVM.GetSrcOfPath(Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + controlVM.current_projectName + "\\Tizen\\" + controlVM.current_projectName + "\\" + controlVM.current_Scene + ".html");
            controlVM.JSFileSave(controlVM.fileTabV.scriptV.getJSCode());
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
        public void changeTabControl(){
        
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

        private void NewFileMenuClick_Click(object sender, RoutedEventArgs e)
        {
            Window childWindow = new NewProjectWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }

        private void OpenFileMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseFileMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CutMenu_Click(object sender, RoutedEventArgs e)
        {
            controlVM._ucaVM.uicanV.cutLeftEvent();
        }

        private void CopyMenu_Click(object sender, RoutedEventArgs e)
        {
            controlVM._ucaVM.uicanV.CopyEvent();
        }

        private void PasteMenu_Click(object sender, RoutedEventArgs e)
        {
            controlVM._ucaVM.uicanV.CopyEvent();
        }

        private void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            controlVM._ucaVM.uicanV.DeleteLeftEvent();
        }

        private void AddSceneMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ImportAndroidMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseAllFileMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TooltipMenu_Click(object sender, RoutedEventArgs e)
        {
            Window childWindow = new TutorialWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }


    }
}