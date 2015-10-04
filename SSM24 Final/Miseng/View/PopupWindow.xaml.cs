using System.Windows;

namespace Miseng.View
{
    /// <summary>
    /// Description for PopupWindow.
    /// </summary>
    public partial class PopupWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the PopupWindow class.
        /// </summary>
        public PopupWindow()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://eastperfect.tistory.com/");
        }
    }
}