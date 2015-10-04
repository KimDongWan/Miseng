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
using Miseng.View.TabControls;
namespace Miseng.View.UICanvas
{
    public partial class PreviewCanvasView : UserControl
    {
        private bool _isDown = false;
        private bool _isInit = false;
        public ControlViewModel ctrVM;
        public ExtendUIMakingView exV;
        public string _file_path ="";
        UICanvasViewModel ucaVM;
        RotateTransform transform;
        Canvas __canvas;
        //요기
        private TabControlsView _tabControls;
        public PreviewCanvasView(string file_path, Canvas _ca)
        {
            InitializeComponent();
            _file_path = file_path;
            ucaVM = new UICanvasViewModel();
            __canvas = _ca;
            this.Loaded += new RoutedEventHandler(ViewLoded);

        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null) 
            {
                Window window = Application.Current.MainWindow;                
                ctrVM = window.DataContext as ControlViewModel;
                exV = ctrVM.extendUIV;
                ctrVM._ucaVM = ucaVM;
                ctrVM.extendUIV = exV;
                ctrVM._ucaVM.preview = this;
                //요기
                _tabControls = ctrVM.tabControlsV; 

                if (!_isInit)//.가 안되어 있다면 수행.
                {
                    ucaVM.DomtreeVM = new domTreeManagementViewModel(_file_path);
                    ctrVM._ucaVM.DomtreeVM.InitList(ctrVM._ucaVM.UI_Element_List);
                    ctrVM._ucaVM.UI_Element_List = ctrVM._ucaVM.DomtreeVM.ReadHtmlToCS();
                    
                    ctrVM.SrcCodeVM.GetSrcOfPath(_file_path);
                    //이부분에 리스트들을 초기화 하는부분이 있으면 됨.

                    foreach (UIElementInfo temp in ctrVM._ucaVM.UI_Element_List)
                    {
                        
                        UIElementInfo currentUIInfo = new UIElementInfo();
                        ctrVM._ucaVM.currentUIInfo.UIELEMENT_TYPE = temp.UIELEMENT_TYPE;
                        currentUIInfo.UIELEMENT_TYPE = temp.UIELEMENT_TYPE;
                        currentUIInfo.UIELEMENT_ID = temp.UIELEMENT_ID;
                        ctrVM._ucaVM.Items.Add(currentUIInfo);
                        lvDataBinding.ItemsSource = ctrVM._ucaVM.Items;

                        ImageBrush brush = new ImageBrush();
                        BitmapImage image;
                        string _imagePath = temp.UIELEMENT_IMAGEURL;

                        if (!string.IsNullOrEmpty(_imagePath))
                        {
                            image = new BitmapImage(new Uri(_imagePath));
                            brush.ImageSource = image;
                        }
                        SolidColorBrush backgroundBrush = new SolidColorBrush(temp.UIELEMENT_BACKGROUND);
                        SolidColorBrush foregroundBrush = new SolidColorBrush(temp.UIELEMENT_FOREGROUND);
                        SolidColorBrush bordergroundBrush = new SolidColorBrush(temp.UIELEMENT_BORDERGROUND);
                        
                        transform = new RotateTransform();
                        transform.Angle = temp.UIELEMENT_ANGLE;
                        Thickness thickness = new Thickness();
                        thickness.Bottom = temp.UIELEMENT_DTHICKNESS;
                        thickness.Top = temp.UIELEMENT_UTHICKNESS;
                        thickness.Right = temp.UIELEMENT_RTHICKNESS;
                        thickness.Left = temp.UIELEMENT_LTHICKNESS;

                        if (temp.UIELEMENT_TYPE.Equals("Canvas"))
                        {
                            __canvas.Background = backgroundBrush;
                            __canvas.Name = temp.UIELEMENT_ID;                         
                            __canvas.RenderTransform = transform;
                            __canvas.Height = temp.UIELEMENT_HEIGHT;
                            __canvas.Width = temp.UIELEMENT_WIDTH;
                            __canvas.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                __canvas.Background = backgroundBrush;
                            else
                                __canvas.Background = brush;

                            __canvas.ToolTip = temp.UIELEMENT_TOOLTIP;
                            //   Visibility visibility = Visibility.Visible;
                        }

                        if (temp.UIELEMENT_TYPE.Equals("radiobutton"))
                        {
                            RadioButton radiobutton = new RadioButton();
                            radiobutton.Name = temp.UIELEMENT_ID;
                            radiobutton.Content = temp.UIELEMENT_TEXT;
                            radiobutton.RenderTransform = transform;
                            radiobutton.Height = temp.UIELEMENT_HEIGHT;
                            radiobutton.Width = temp.UIELEMENT_WIDTH;
                            radiobutton.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                radiobutton.Background = backgroundBrush;
                            else
                                radiobutton.Background = brush;
                            radiobutton.Foreground = foregroundBrush;
                            radiobutton.BorderBrush = bordergroundBrush;
                            radiobutton.BorderThickness = thickness;
                            radiobutton.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            radiobutton.Visibility = visibility;
                            Canvas.SetZIndex(radiobutton, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(radiobutton);
                            Canvas.SetLeft(radiobutton, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(radiobutton, temp.UIELEMENT_YPOSITION);
                        }

                        else if (temp.UIELEMENT_TYPE.Equals("button"))
                        {
                            Button btn = new Button();
                            btn.Name = temp.UIELEMENT_ID;
                            btn.Content = temp.UIELEMENT_TEXT;
                            btn.RenderTransform = transform;
                            btn.Height = temp.UIELEMENT_HEIGHT;
                            btn.Width = temp.UIELEMENT_WIDTH;
                            btn.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                btn.Background = backgroundBrush;
                            else
                                btn.Background = brush;
                            btn.Foreground = foregroundBrush;
                            btn.BorderBrush = bordergroundBrush;
                            btn.BorderThickness = thickness;
                            btn.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                               visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            btn.Visibility = visibility;
                            Canvas.SetZIndex(btn, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(btn);
                            Canvas.SetLeft(btn, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(btn, temp.UIELEMENT_YPOSITION);
                        }
                        else if (temp.UIELEMENT_TYPE.Equals("image"))
                        {
                            Image imageTagImg = new Image();
                            imageTagImg.Name = temp.UIELEMENT_ID;
                            imageTagImg.Height = temp.UIELEMENT_HEIGHT;
                            imageTagImg.Width = temp.UIELEMENT_WIDTH;
                            imageTagImg.ToolTip = temp.UIELEMENT_TOOLTIP;
                            imageTagImg.RenderTransform = transform;
                            imageTagImg.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            imageTagImg.Visibility = visibility;
                            Canvas.SetZIndex(imageTagImg, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(imageTagImg);
                            Canvas.SetLeft(imageTagImg, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(imageTagImg, temp.UIELEMENT_YPOSITION);
                        }
                        else if (temp.UIELEMENT_TYPE.Equals("passwordbox"))
                        {
                            PasswordBox passwordBox = new PasswordBox();
                            passwordBox.Name = temp.UIELEMENT_ID;
                            passwordBox.Height = temp.UIELEMENT_HEIGHT;
                            passwordBox.Width = temp.UIELEMENT_WIDTH;
                            passwordBox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                passwordBox.Background = backgroundBrush;
                            else
                                passwordBox.Background = brush;
                            passwordBox.Foreground = foregroundBrush;
                            passwordBox.BorderBrush = bordergroundBrush;
                            passwordBox.BorderThickness = thickness;
                            passwordBox.RenderTransform = transform;
                            passwordBox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            passwordBox.Visibility = visibility;
                            Canvas.SetZIndex(passwordBox, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(passwordBox);
                            Canvas.SetLeft(passwordBox, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(passwordBox, temp.UIELEMENT_YPOSITION);
                        }
                        else if (temp.UIELEMENT_TYPE.Equals("textbox"))
                        {
                            TextBox txtbox = new TextBox();
                            txtbox.Name = temp.UIELEMENT_ID;
                            txtbox.Text = temp.UIELEMENT_TEXT;
                            txtbox.Height = temp.UIELEMENT_HEIGHT;
                            txtbox.Width = temp.UIELEMENT_WIDTH;
                            txtbox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                txtbox.Background = backgroundBrush;
                            else
                                txtbox.Background = brush;
                            txtbox.Foreground = foregroundBrush;
                            txtbox.BorderBrush = bordergroundBrush;
                            txtbox.BorderThickness = thickness;
                            txtbox.RenderTransform = transform;
                            txtbox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            txtbox.Visibility = visibility;
                            Canvas.SetZIndex(txtbox, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(txtbox);
                            Canvas.SetLeft(txtbox, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(txtbox, temp.UIELEMENT_YPOSITION);
                        }

                        else if (temp.UIELEMENT_TYPE.Equals("listbox"))
                        {
                            ListBox listbox = new ListBox();
                            listbox.Name = temp.UIELEMENT_ID;
                            listbox.RenderTransform = transform;
                            listbox.Height = temp.UIELEMENT_HEIGHT;
                            listbox.Width = temp.UIELEMENT_WIDTH;
                            listbox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                listbox.Background = backgroundBrush;
                            else
                                listbox.Background = brush;
                            listbox.Foreground = foregroundBrush;
                            listbox.BorderBrush = bordergroundBrush;
                            listbox.BorderThickness = thickness;
                            listbox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            listbox.Visibility = visibility;
                            Canvas.SetZIndex(listbox, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(listbox);
                            Canvas.SetLeft(listbox, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(listbox, temp.UIELEMENT_YPOSITION);
                        }

                        else if (temp.UIELEMENT_TYPE.Equals("combobox"))
                        {
                            ComboBox combobox = new ComboBox();
                            combobox.Name = temp.UIELEMENT_ID;
                            combobox.Text = temp.UIELEMENT_TEXT;
                            combobox.RenderTransform = transform;
                            combobox.Height = temp.UIELEMENT_HEIGHT;
                            combobox.Width = temp.UIELEMENT_WIDTH;
                            combobox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                combobox.Background = backgroundBrush;
                            else
                                combobox.Background = brush;
                            combobox.Foreground = foregroundBrush;
                            combobox.BorderBrush = bordergroundBrush;
                            combobox.BorderThickness = thickness;
                            combobox.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            combobox.Visibility = visibility;
                            Canvas.SetZIndex(combobox, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(combobox);
                            Canvas.SetLeft(combobox, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(combobox, temp.UIELEMENT_YPOSITION);
                        }

                        else if (temp.UIELEMENT_TYPE.Equals("menu"))
                        {
                            Menu menu = new Menu();
                            menu.Name = temp.UIELEMENT_ID;
                            menu.RenderTransform = transform;
                            menu.Height = temp.UIELEMENT_HEIGHT;
                            menu.Width = temp.UIELEMENT_WIDTH;
                            menu.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                menu.Background = backgroundBrush;
                            else
                                menu.Background = brush;
                            menu.Foreground = foregroundBrush;
                            menu.BorderBrush = bordergroundBrush;
                            menu.BorderThickness = thickness;
                            menu.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            menu.Visibility = visibility;
                            Canvas.SetZIndex(menu, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(menu);
                            Canvas.SetLeft(menu, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(menu, temp.UIELEMENT_YPOSITION);
                        }

                        else if (temp.UIELEMENT_TYPE.Equals("slider"))
                        {
                            Slider slider = new Slider();
                            slider.Name = temp.UIELEMENT_ID;
                            slider.RenderTransform = transform;
                            slider.Height = temp.UIELEMENT_HEIGHT;
                            slider.Width = temp.UIELEMENT_WIDTH;
                            slider.ToolTip = temp.UIELEMENT_TOOLTIP;
                            if (string.IsNullOrEmpty(_imagePath))
                                slider.Background = backgroundBrush;
                            else
                                slider.Background = brush;
                            slider.Foreground = foregroundBrush;
                            slider.BorderBrush = bordergroundBrush;
                            slider.BorderThickness = thickness;
                            slider.ToolTip = temp.UIELEMENT_TOOLTIP;
                            Visibility visibility = Visibility.Visible;
                            if (temp.UIELEMENT_VISIBLE.Equals("Visible"))
                            {
                                visibility = Visibility.Visible;
                            }
                            else
                            {
                                visibility = Visibility.Hidden;
                            }
                            slider.Visibility = visibility;
                            Canvas.SetZIndex(slider, temp.UIELEMENT_ZINDEX);
                            __canvas.Children.Add(slider);
                            Canvas.SetLeft(slider, temp.UIELEMENT_XPOSITION);
                            Canvas.SetTop(slider, temp.UIELEMENT_YPOSITION);
                        }
                    }

                    _isInit = true;
                }
            }
        }
         
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;

        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //요기
            _tabControls.TabContols.SelectedIndex = 0;
            string clikedControlType = "test";
            string clikedControlName = "test";
            if (_isDown)
            {
                clikedControlType = (sender as TextBlock).Text;
                clikedControlName = (sender as TextBlock).ToolTip.ToString();
                ctrVM._ucaVM.uicanV.PreviewClickControl(clikedControlType, clikedControlName);
            }
        }

        public void clickedControl(string items)
        {
            int i = 0;
            if (items.Contains("Slider"))
            {
                items = "slider";
            }
            else if (items.Contains("RadioButton"))
            {
                items = "radioButton";
            }
            else if (items.Contains("Button"))
            {
                items = "button";
            }
            else if (items.Contains("TextBox"))
            {
                items = "textBox";
            }
            else if (items.Contains("ComboBox"))
            {
                items = "comboBox";
            }
            else if (items.Contains("PasswordBox"))
            {
                items = "passwordBox";
            }
            else if (items.Contains("Menu"))
            {
                items = "menu";
            }
            else if (items.Contains("ListBox"))
            {
                items = "listBox";
            }
            for (i = 0; i < lvDataBinding.Items.Count; i++)
            {
                /*
                if(items.Contains((lvDataBinding.Items[i] as UIElementInfo).UIELEMENT_ID))
                {
                    break;
                }
                */
                if ((lvDataBinding.Items[i] as UIElementInfo).UIELEMENT_ID.Contains(items))
                {
                    break;
                }
            }
            lvDataBinding.SelectedIndex = i;
        }

        public void clickedControl(object eSource)
        {
            int i = 0;
            string eSourceStr = eSource.ToString();
            string typeID = "test";
            if (eSourceStr.Contains("Slider"))
            {
                typeID = (eSource as Slider).Name.ToString();
            }
            else if (eSourceStr.Contains("RadioButton"))
            {
                typeID = (eSource as RadioButton).Name.ToString();
            }
            else if (eSourceStr.Contains("Button"))
            {
                typeID = (eSource as Button).Name.ToString();
            }
            else if (eSourceStr.Contains("TextBox"))
            {
                typeID = (eSource as TextBox).Name.ToString();
            }
            else if (eSourceStr.Contains("ComboBox"))
            {
                typeID = (eSource as ComboBox).Name.ToString();
            }
            else if (eSourceStr.Contains("PasswordBox"))
            {
                typeID = (eSource as PasswordBox).Name.ToString();
            }
            else if (eSourceStr.Contains("Menu"))
            {
                typeID = (eSource as Menu).Name.ToString();
            }
            else if (eSourceStr.Contains("ListBox"))
            {
                typeID = (eSource as ListBox).Name.ToString();
            }
            for (i = 0; i < lvDataBinding.Items.Count; i++)
            {
                /*
                if(items.Contains((lvDataBinding.Items[i] as UIElementInfo).UIELEMENT_ID))
                {
                    break;
                }
                */
                if ((lvDataBinding.Items[i] as UIElementInfo).UIELEMENT_ID.Equals(typeID))
                {
                    break;
                }
            }
            lvDataBinding.SelectedIndex = i;
        }
    }
}