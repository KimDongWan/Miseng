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
using Miseng.ViewModel.UICanvas;

namespace Miseng.View
{
    /// <summary>
    /// rename.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class renameWindow : Window
    {
        private ControlViewModel myViewModel;
        private bool _isRename;
        public renameWindow(bool isRename)
        {
            InitializeComponent();
            _isRename = isRename;
            this.Loaded += new RoutedEventHandler(ViewLoaded);
        }



        private void ViewLoaded(object sender, RoutedEventArgs r)
        {
            myViewModel = this.DataContext as ControlViewModel;
        }

        private void reName_Cancle_EventHandler(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void reName_OK_ClickEventHandler(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            string path = myViewModel.FileTreeVM.CurrentTreeItem.Path;
            string existFileName;
            System.IO.FileInfo file;
            if (string.IsNullOrEmpty(change_FileName.Text))
            {
                result = MessageBox.Show("변경될 이름이 없습니다.", "오류");
                return;
            }


            file = new System.IO.FileInfo(path);
            existFileName = myViewModel.FileTreeVM.CurrentTreeItem.Name;
            if (_isRename) path = path.Remove(path.Length - existFileName.Length);
            else path += "\\";
            path += change_FileName.Text;



            if (_isRename && System.IO.File.Exists(path))
            {
                result = MessageBox.Show("같은 이름의 파일이 이미 존재합니다.", "오류");
                return;
            }

            if (!_isRename && System.IO.Directory.Exists(path))
            {
                result = MessageBox.Show("같은 이름의 폴더가 이미 존재합니다.", "오류");
                return;
            }

            if (_isRename)
            {
                file.MoveTo(path);
            }
            else
            {
                System.IO.Directory.CreateDirectory(path);
            }


            myViewModel.Update_ExplorerViewModel();
            this.Close();

        }
    }


}
