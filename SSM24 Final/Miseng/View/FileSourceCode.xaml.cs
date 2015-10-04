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
using Miseng.ViewModel.UICanvas;
namespace Miseng.View
{
    /// <summary>
    /// FileSourceCode.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 


    public partial class FileSourceCode : UserControl
    {
        private ControlViewModel myViewModel;
        public FileSourceCode()
        {
            InitializeComponent();            
            this.Loaded += new RoutedEventHandler(UpdateSrcCodeText);
        }

        public void UpdateSrcCodeText(object sender, RoutedEventArgs r)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                myViewModel = window.DataContext as ControlViewModel;
                myViewModel.fileSourceCode = this;
                if (myViewModel.SrcCodeVM != null) myViewModel.SrcCodeVM.avalonEdit = textEditor;
                if (myViewModel.SrcCodeVM != null) myViewModel.SrcCodeVM.avalonEditJS = textEditorJS;
            }
            /*
            if (this.DataContext != null)
            {
                myViewModel = this.DataContext as ControlViewModel;
                if(myViewModel.SrcCodeVM!=null) myViewModel.SrcCodeVM.avalonEdit = textEditor;                     
            }
            */
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

            /*if(!String.IsNullOrEmpty(myViewModel.SrcCodeVM.FileSrcCode.Path)
                && myViewModel.FileTreeVM.CurrentTreeItem.DirType == (int)ObjectType.File)
                    System.IO.File.WriteAllText(myViewModel.SrcCodeVM.FileSrcCode.Path, SrcCodeBlock.Text);         //avalon changed */
        }
         


    }
}
