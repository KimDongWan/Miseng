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
    public partial class TutorialWindow : Window
    {
        Image image;
        BitmapImage bitmapImage;
        int i = 1;

        Image image2;
        BitmapImage bitmapImage2;
        int i2 = 1;

        Image image3;
        BitmapImage bitmapImage3;
        int i3 = 1;

        public TutorialWindow()
        {
            InitializeComponent();
            sceneTutorial.Children.Clear();
            string temp = "..\\..\\images\\tutorial" + i + ".JPG";
            image = new Image();
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage.EndInit();
            image.Source = bitmapImage;
            sceneTutorial.Children.Add(image);

            sceneTutorial2.Children.Clear();
            string temp2 = "..\\..\\images\\tutorial" + i + ".JPG";
            image2 = new Image();
            bitmapImage2 = new BitmapImage();
            bitmapImage2.BeginInit();
            bitmapImage2.UriSource = new Uri(temp2, UriKind.Relative);
            bitmapImage2.EndInit();
            image2.Stretch = Stretch.Fill;
            image2.Source = bitmapImage2;
            sceneTutorial2.Children.Add(image2);

            sceneTutorial3.Children.Clear();
            string temp3 = "..\\..\\images\\t" + i + ".JPG";
            image3 = new Image();
            bitmapImage3 = new BitmapImage();
            bitmapImage3.BeginInit();
            bitmapImage3.UriSource = new Uri(temp3, UriKind.Relative);
            bitmapImage3.EndInit();
            image3.Stretch = Stretch.Fill;
            image3.Source = bitmapImage3;
            sceneTutorial3.Children.Add(image3);
        }
       
        private void tuLeftButton_Click(object sender, RoutedEventArgs e)
        {
            sceneTutorial.Children.Clear();
            string temp = "test";
            if (i == 1)
            {
                temp = "..\\..\\images\\tutorial" + 1 + ".JPG";
            }
            else
            {
                i = i - 1;
                string iStr = i.ToString();
                temp = "..\\..\\images\\tutorial" + iStr + ".JPG";
            }
            image = new Image();
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage.EndInit();
            image.Source = bitmapImage;
            sceneTutorial.Children.Add(image);
        }

        private void tuRightButton_Click(object sender, RoutedEventArgs e)
        {
            sceneTutorial.Children.Clear();
            string temp = "test";
            if (i == 24)
            {
                temp = "..\\..\\images\\tutorial" + 24 + ".JPG";
            }
            else
            {
                i = i + 1;
                string iStr = i.ToString();
                temp = "..\\..\\images\\tutorial" + iStr + ".JPG";
            }
            image = new Image();
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage.EndInit();
            image.Source = bitmapImage;
            sceneTutorial.Children.Add(image);
        }

        private void tuLeftButton_Click2(object sender, RoutedEventArgs e)
        {
            sceneTutorial2.Children.Clear();
            string temp = "test";
            if (i2 == 1)
            {
                temp = "..\\..\\images\\tutorial" + 1 + ".JPG";
            }
            else
            {
                i2 = i2 - 1;
                string iStr = i2.ToString();
                temp = "..\\..\\images\\tutorial" + iStr + ".JPG";
            }
            image2 = new Image();
            bitmapImage2 = new BitmapImage();
            bitmapImage2.BeginInit();
            bitmapImage2.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage2.EndInit();
            image2.Stretch = Stretch.Fill;
            image2.Source = bitmapImage2;
            sceneTutorial2.Children.Add(image2);
        }

        private void tuRightButton_Click2(object sender, RoutedEventArgs e)
        {
            sceneTutorial2.Children.Clear();
            string temp = "test";
            if (i2 == 7)
            {
                temp = "..\\..\\images\\tutorial" + 7 + ".JPG";
            }
            else
            {
                i2 = i2 + 1;
                string iStr = i2.ToString();
                temp = "..\\..\\images\\tutorial" + iStr + ".JPG";
            }
            image2 = new Image();
            bitmapImage2 = new BitmapImage();
            bitmapImage2.BeginInit();
            bitmapImage2.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage2.EndInit();
            image2.Stretch = Stretch.Fill;
            image2.Source = bitmapImage2;
            sceneTutorial2.Children.Add(image2);
        }

        private void tuLeftButton_Click3(object sender, RoutedEventArgs e)
        {
            sceneTutorial3.Children.Clear();
            string temp = "test";
            if (i3 == 1)
            {
                temp = "..\\..\\images\\t" + 1 + ".JPG";
            }
            else
            {
                i3 = i3 - 1;
                string iStr = i3.ToString();
                temp = "..\\..\\images\\t" + iStr + ".JPG";
            }
            image3 = new Image();
            bitmapImage3 = new BitmapImage();
            bitmapImage3.BeginInit();
            bitmapImage3.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage3.EndInit();
            image3.Stretch = Stretch.Fill;
            image3.Source = bitmapImage3;
            sceneTutorial3.Children.Add(image3);
        }

        private void tuRightButton_Click3(object sender, RoutedEventArgs e)
        {
            sceneTutorial3.Children.Clear();
            string temp = "test";
            if (i3 == 7)
            {
                temp = "..\\..\\images\\t" + 16 + ".JPG";
            }
            else
            {
                i3 = i3 + 1;
                string iStr = i3.ToString();
                temp = "..\\..\\images\\t" + iStr + ".JPG";
            }
            image3 = new Image();
            bitmapImage3 = new BitmapImage();
            bitmapImage3.BeginInit();
            bitmapImage3.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage3.EndInit();
            image3.Stretch = Stretch.Fill;
            image3.Source = bitmapImage3;
            sceneTutorial3.Children.Add(image3);
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
