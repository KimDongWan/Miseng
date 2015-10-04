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
using System.Windows.Markup;
using System.Xml;
using System.IO;
using Miseng.ViewModel.UICanvas;

namespace Miseng.View.UIMaking
{
    public partial class UIMakingView : UserControl
    {
        ControlViewModel ctrVM;
        public UIMakingView()
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
            }
        }
        private void button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock temp = sender as TextBlock;

            ctrVM.UIType = temp.Name;

            ctrVM._isMouseDown = true;
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
        }

    }
}