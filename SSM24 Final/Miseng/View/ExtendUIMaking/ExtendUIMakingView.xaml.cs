using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Miseng.ViewModel.UICanvas;
namespace Miseng.View.ExtendUIMaking
{

    public partial class ExtendUIMakingView : UserControl
    {
        
        ControlViewModel ctrVM;
        public ExtendUIMakingView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                window.Topmost = true;
                ctrVM = window.DataContext as ControlViewModel;
                ctrVM.extendUIV = this;
                FontSelector.ItemsSource = Fonts.SystemFontFamilies;
                ctrVM.fontSizeCollection();
            }
        }

        private void TextBox_WidthChanged(object sender, TextChangedEventArgs e)
        {
            string tempWidth = WidthTB.Text;
            double crrentWidth;
            if (string.IsNullOrEmpty(tempWidth))
            {
                crrentWidth = 0;
            }
            else
            {
                crrentWidth = Convert.ToDouble(tempWidth);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginWidth(crrentWidth);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tempText = TextTB.Text;
            string crrentText;
            if (string.IsNullOrEmpty(tempText))
            {
                crrentText = null;
            }
            else
            {
                crrentText = tempText;
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginText(crrentText);
            }
        }

        private void TextBox_ToolTipChanged(object sender, TextChangedEventArgs e)
        {
            string tempToolTip = TooltipTB.Text;
            string crrentToolTip;
            if (string.IsNullOrEmpty(tempToolTip))
            {
                crrentToolTip = null;
            }
            else
            {
                crrentToolTip = tempToolTip;
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginToolTip(crrentToolTip);
            }
        }
        //Angle
        private void TextBox_AngleChanged(object sender, TextChangedEventArgs e)
        {
            string tempAngle = AngleTB.Text;
            double crrentAngle;
            if (string.IsNullOrEmpty(tempAngle))
            {
                crrentAngle = 0;
            }
            else
            {
                crrentAngle = Convert.ToDouble(tempAngle);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginAngle(crrentAngle);
            }
        }

        //XPosition
        /*
        private void TextBox_XPositionChanged(object sender, TextChangedEventArgs e)
        {
            string tempXPosition = XPositionTB.Text;
            double crrentXPosition;
            if (string.IsNullOrEmpty(tempXPosition))
            {
                crrentXPosition = 0;
            }
            else
            {
                crrentXPosition = Convert.ToDouble(tempXPosition);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginXPosition(crrentXPosition);
            }
        }
        */

        //YPosition
        /*
        private void TextBox_YPositionChanged(object sender, TextChangedEventArgs e)
        {
            string tempYPosition = YPositionTB.Text;
            double crrentYPosition;
            if (string.IsNullOrEmpty(tempYPosition))
            {
                crrentYPosition = 0;
            }
            else
            {
                crrentYPosition = Convert.ToDouble(tempYPosition);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginYPosition(crrentYPosition);
            }
        }
        */

        private void TextBox_HeightChanged(object sender, TextChangedEventArgs e)
        {
            string tempHeight = HeightTB.Text;
            double crrentHeight;
            if (string.IsNullOrEmpty(tempHeight))
            {
                crrentHeight = 0;

            }
            else
            {
                crrentHeight = Convert.ToDouble(tempHeight);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginHeight(crrentHeight);
            }

        }

        private void TextBox_LThickChanged(object sender, RoutedEventArgs e)
        {
            string tempLThick = LThickTB.Text;
            double crrentLThick;
            string position = "Left";
            if (string.IsNullOrEmpty(tempLThick))
            {
                crrentLThick = 0;
            }
            else
            {
                crrentLThick = Convert.ToDouble(tempLThick);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginThick(position, crrentLThick);
            }
        }

        private void TextBox_RThickChanged(object sender, RoutedEventArgs e)
        {
            string tempRThick = RThickTB.Text;
            double crrentRThick;
            string position = "Right";
            if (string.IsNullOrEmpty(tempRThick))
            {
                crrentRThick = 0;
            }
            else
            {
                crrentRThick = Convert.ToDouble(tempRThick);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginThick(position, crrentRThick);
            }
        }

        private void TextBox_UThickChanged(object sender, RoutedEventArgs e)
        {
            string tempUThick = UThickTB.Text;
            double crrentUThick;
            string position = "Top";
            if (string.IsNullOrEmpty(tempUThick))
            {
                crrentUThick = 0;
            }
            else
            {
                crrentUThick = Convert.ToDouble(tempUThick);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginThick(position, crrentUThick);
            }
        }

        private void TextBox_DThickChanged(object sender, RoutedEventArgs e)
        {
            string tempDThick = DThickTB.Text;
            double crrentDThick;
            string position = "Bottom";
            if (string.IsNullOrEmpty(tempDThick))
            {
                crrentDThick = 0;
            }
            else
            {
                crrentDThick = Convert.ToDouble(tempDThick);
            }
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginThick(position, crrentDThick);
            }
        }

        private void VisibleBTN_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("visibleBTN") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void CursorBTN_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cursorBTN") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void CursorSelect_Click(object sender, RoutedEventArgs e)
        {
            string selectCursor = (sender as MenuItem).Header.ToString();
            ctrVM.ChagneOriginCursor(selectCursor);
        }

        private void VisibleSelect_Click(object sender, RoutedEventArgs e)
        {
            string selectVisible = (sender as MenuItem).Header.ToString();
            ctrVM.ChagneOriginVisible(selectVisible);
        }

        private void HorizonLeftBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Left";
            ctrVM.ChagneOriginHorizon(chagedPosition);
        }

        private void HorizonCenterBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Center";
            ctrVM.ChagneOriginHorizon(chagedPosition);
        }

        private void HorizonRightBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Right";
            ctrVM.ChagneOriginHorizon(chagedPosition);
        }

        private void HorizonStretchBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Stretch";
            ctrVM.ChagneOriginHorizon(chagedPosition);
        }

        private void VerticalTopBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Top";
            ctrVM.ChagneOriginVertical(chagedPosition);
        }

        private void VerticalCenterBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Center";
            ctrVM.ChagneOriginVertical(chagedPosition);
        }

        private void VerticalBottomBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Bottom";
            ctrVM.ChagneOriginVertical(chagedPosition);
        }

        private void VerticalStretchBTN_Click(object sender, RoutedEventArgs e)
        {
            string chagedPosition = "Stretch";
            ctrVM.ChagneOriginVertical(chagedPosition);
        }
        private void ImageComboBoxButton_Click(object sender, RoutedEventArgs e)
        {
            object temp = ImageCollection.SelectionBoxItem;
            if ((temp as ImageInfo) == null)
            {
                return;
            }
            string _imagePath = (temp as ImageInfo).ImagePath;
            ImageBrush brush = new ImageBrush();
            BitmapImage image = new BitmapImage(new Uri(_imagePath));
            brush.ImageSource = image;
            UIElement _selectedEle = ctrVM._ucaVM.uicanV.selectedElement;
            string _selectedStr = _selectedEle.ToString();
            if (_selectedStr.Contains("RadioButton"))
            {
                (_selectedEle as RadioButton).Background = brush;                
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
                //ctrVM.SearchList()
            }
            else if (_selectedStr.Contains("Button"))
            {
                (_selectedEle as Button).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("Canvas"))
            {
                //ctrVM._ucaVM.currentUIINfo에 CanvasType이 제대로 안들어감.
                (_selectedEle as Canvas).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("TextBox"))
            {
                (_selectedEle as TextBox).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("ListBox"))
            {
                (_selectedEle as ListBox).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("PasswordBox"))
            {
                (_selectedEle as PasswordBox).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("Menu"))
            {
                (_selectedEle as Menu).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("ComboBox"))
            {
                (_selectedEle as ComboBox).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }
            else if (_selectedStr.Contains("Slider"))
            {
                (_selectedEle as Slider).Background = brush;
                ctrVM._ucaVM.currentUIInfo.UIELEMENT_IMAGEURL = _imagePath;
            }

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginOpacity(opacitySlider.Value);
            }
        }
        
        private void opacityBorderSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginBorderOpacity(opacityBorderSlider.Value);
            }
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                string value = (sender as ComboBox).SelectedItem.ToString();
                int valueInt = Convert.ToInt32(value);

                if (ctrVM != null)
                {
                    ctrVM.ChagneOriginFontSize(valueInt);
                }
            }
            else
            {
                return;
            }
        }
    }
}