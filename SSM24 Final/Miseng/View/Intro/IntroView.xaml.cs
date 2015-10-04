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
namespace Miseng.View.Intro
{
  
    public partial class IntroView : UserControl
    {
        
        ControlViewModel ctrVM;
        Image image;
        BitmapImage bitmapImage;
        int i = 1;
        
        
        Image image2;
        BitmapImage bitmapImage2;
        int i2 = 1;

        Image image3;
        BitmapImage bitmapImage3;
        int i3 = 1;
        public IntroView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
            sceneTutorial.Children.Clear();
            string temp = "..\\..\\images\\tutorial" + i + ".JPG";
            image = new Image();
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage.EndInit();
            image.Stretch = Stretch.Fill;
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


        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
                ctrVM._intro = this;
            }
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
                i = i-1;
                string iStr = i.ToString();
                temp = "..\\..\\images\\tutorial" + iStr + ".JPG";
            }
            image = new Image();
            bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(temp, UriKind.Relative);
            bitmapImage.EndInit();
            image.Stretch = Stretch.Fill;
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
            image.Stretch = Stretch.Fill;
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

        private void newProject_Click(object sender, RoutedEventArgs e)
        {
            Window childWindow = new NewProjectWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }

        private void openProject_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void vDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + (sender as ListBox).SelectedItem.ToString() + "\\Tizen\\" + (sender as ListBox).SelectedItem.ToString() + "\\index.html";
            
            if (!File.Exists(path))
            {
                MessageBox.Show("프로젝트가 존재하지 않습니다.");
                return;
            }
            else
            {
                Grid.SetZIndex(ctrVM._intro, 0);
                Grid.SetZIndex(ctrVM.fileTabV, 1);
                ctrVM.CURRENT_SCENE = "index.html";
                ctrVM.current_Scene = "index";
                ctrVM.current_projectName = (sender as ListBox).SelectedItem.ToString();
                ctrVM.projectAndfileName = (sender as ListBox).SelectedItem.ToString() + "_" + "index";

                ctrVM.SrcCodeVM.GetSrcOfPath(path);
                ctrVM.fileTabV.add_Tab_Item("index", path);
                ctrVM.fileTabV.file_path = path;
            }
            
        }

        private void pruginBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TTizenCurtiBtn_Click(object sender, RoutedEventArgs e)
        {
            Window childWindow = new PopupWindow();
            childWindow.ShowInTaskbar = false;
            childWindow.Owner = Application.Current.MainWindow;
            childWindow.DataContext = this.DataContext;
            childWindow.Show();
        }
    }
}