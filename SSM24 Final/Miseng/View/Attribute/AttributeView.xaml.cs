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
using Miseng.ViewModel.UICanvas;
namespace Miseng.View.Attribute
{
    public partial class AttributeView : UserControl
    {
        ControlViewModel ctrVM;
        public AttributeView()
        {
            InitializeComponent();
        }
        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
                ctrVM._attributeV = this;
            }
        }
    }
}