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
using Miseng.View.UICanvas;
using Miseng.ViewModel.UICanvas;
using Miseng.View.FileTab;
using System.Collections.ObjectModel;
using Miseng.ViewModel;
using Miseng.View.ExtendUIMaking;

namespace Miseng.View.TabControls
{
    public partial class TabControlsView : UserControl
    {
        ControlViewModel ctrVM;
        public TabControlsView()
        {
            InitializeComponent();
        }
        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
            }
        }
    }
}