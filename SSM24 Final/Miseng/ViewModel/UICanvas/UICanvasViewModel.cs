using System;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using System.Windows.Markup;
using Miseng.View.UICanvas;
using System.ComponentModel;
using Miseng.Common;
using System.Collections.ObjectModel;
using Miseng.View.ExtendUIMaking;
using Miseng.View.FileTab;


namespace Miseng.ViewModel.UICanvas
{
    public class UICanvasViewModel : ViewModelBase
    {
        RotateTransform transform;
        public domTreeManagementViewModel DomtreeVM = null;
        public PreviewCanvasView preview;
        public ExtendUIMakingView extendUIV;
        public UICanvasView uicanV;
        int currentZIndex = 0;
        private List<UIElementInfo> ui_Element_List;
        //   private List<UIElementInfo> sub_Element_List;
        public UIElementInfo currentUIInfo;
        //     public UIElementInfo subUIinfo;
        private ObservableCollection<UIElementInfo> items;
        private ObservableCollection<ImageInfo> imageCollection;
        public ObservableCollection<UIElementInfo> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }
        public ObservableCollection<ImageInfo> ImageCollection
        {
            get
            {
                return imageCollection;
            }
            set
            {
                imageCollection = value;
                OnPropertyChanged("ImageCollection");
            }
        }
        public List<UIElementInfo> UI_Element_List
        {
            get
            {
                 return ui_Element_List;
            }
            set
            {
                ui_Element_List = value;
                 OnPropertyChanged("UI_Element_List");
            }
        }
        
        public UICanvasViewModel()
        {
            ui_Element_List = new List<UIElementInfo>();           
            currentUIInfo = new UIElementInfo();
            items = new ObservableCollection<UIElementInfo>();
            ImageCollection = new ObservableCollection<ImageInfo>();
            //transform = new RotateTransform();
        }

       
        public void RenderingUICanvas(Canvas canvas, Point dropPoint, string type)
        {
            string uiTextName = numbering(type);
            currentUIInfo = new UIElementInfo();
            
            currentUIInfo.UIELEMENT_TYPE = type;
            currentUIInfo.UIELEMENT_TOOLTIP = "Tooltip Here";
            currentUIInfo.UIELEMENT_ID = uiTextName;
            currentUIInfo.UIELEMENT_TEXT = uiTextName;
            currentUIInfo.UIELEMENT_ZINDEX = currentZIndex;
            currentUIInfo.UIELEMENT_XPOSITION = dropPoint.X;
            currentUIInfo.UIELEMENT_YPOSITION = dropPoint.Y;
            currentZIndex++;
            ui_Element_List.Add(currentUIInfo);
            items.Add(currentUIInfo);
            preview.lvDataBinding.ItemsSource = items;
            transform = new RotateTransform();
            transform.Angle = currentUIInfo.UIELEMENT_ANGLE;
            if (type.Equals("button"))
            {
                Button btn = new Button();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#0054545C");
                btn.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                btn.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                btn.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                btn.Name = uiTextName;
                btn.Content = uiTextName;
                btn.Height = 40;
                btn.Width = 60;
                btn.ToolTip = "Tooltip Here";
                btn.RenderTransform = transform;
                Canvas.SetZIndex(btn, currentZIndex);
                canvas.Children.Add(btn);
                Canvas.SetLeft(btn, dropPoint.X);
                Canvas.SetTop(btn, dropPoint.Y);
            }
            else if (type.Equals("image"))
            {

                transform.Angle = 0;
                Image image = new Image();
                image.Name = uiTextName;
                image.Height = 40;
                image.Width = 60;
                image.RenderTransform = transform;
                Canvas.SetZIndex(image, currentZIndex);
                canvas.Children.Add(image);
                Canvas.SetLeft(image, dropPoint.X);
                Canvas.SetTop(image, dropPoint.Y);
            }
            else if (type.Equals("passwordBox"))
            {
                transform.Angle = 0;
                PasswordBox passwordBox = new PasswordBox();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#0054545C");
                passwordBox.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                passwordBox.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                passwordBox.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                passwordBox.Name = uiTextName;
                passwordBox.Height = 40;
                passwordBox.Width = 60;
                passwordBox.ToolTip = "Tooltip Here";
                passwordBox.RenderTransform = transform;
                Canvas.SetZIndex(passwordBox, currentZIndex);
                canvas.Children.Add(passwordBox);
                Canvas.SetLeft(passwordBox, dropPoint.X);
                Canvas.SetTop(passwordBox, dropPoint.Y);
            }
            else if (type.Equals("textbox"))
            {
                transform.Angle = 0;
                TextBox txtBox = new TextBox();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#0054545C");
                txtBox.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                txtBox.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                txtBox.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                txtBox.Height = 40;
                txtBox.Width = 60;
                txtBox.Text = uiTextName;
                txtBox.Name = uiTextName;
                txtBox.ToolTip = "Tooltip Here";
                txtBox.RenderTransform = transform;
                Canvas.SetZIndex(txtBox, currentZIndex);
                canvas.Children.Add(txtBox);
                Canvas.SetLeft(txtBox, dropPoint.X);
                Canvas.SetTop(txtBox, dropPoint.Y);
            }
            
            else if (type.Equals("listBox"))
            {
                transform.Angle = 0;
                ListBox listbox = new ListBox();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#54545C");
                listbox.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                listbox.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                listbox.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                listbox.Height = 40;
                listbox.Width = 60;
                listbox.Name = uiTextName;
                listbox.ToolTip = "Tooltip Here";
                listbox.RenderTransform = transform;
                Canvas.SetZIndex(listbox, currentZIndex);
                canvas.Children.Add(listbox);
                Canvas.SetLeft(listbox, dropPoint.X);
                Canvas.SetTop(listbox, dropPoint.Y);

            }
            else if (type.Equals("comboBox"))
            {
                transform.Angle = 0;
                ComboBox combobox = new ComboBox();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#54545C");
                combobox.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                combobox.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                combobox.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                combobox.Height = 40;
                combobox.Width = 60;
                combobox.Text = uiTextName;
                combobox.Name = uiTextName;
                combobox.ToolTip = "Tooltip Here";
                combobox.RenderTransform = transform;
                Canvas.SetZIndex(combobox, currentZIndex);
                canvas.Children.Add(combobox);
                Canvas.SetLeft(combobox, dropPoint.X);
                Canvas.SetTop(combobox, dropPoint.Y);

            }
            else if (type.Equals("menu"))
            {
                transform.Angle = 0;
                Menu menu = new Menu();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#54545C");
                menu.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                menu.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                menu.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                menu.Height = 40;
                menu.Width = 60;
                menu.ToolTip = "Tooltip Here";
                menu.Name = uiTextName;
                menu.RenderTransform = transform;
                Canvas.SetZIndex(menu, currentZIndex);
                canvas.Children.Add(menu);
                Canvas.SetLeft(menu, dropPoint.X);
                Canvas.SetTop(menu, dropPoint.Y);

            }
            else if (type.Equals("radioButton"))
            {
                transform.Angle = 0;
                RadioButton radiobutton = new RadioButton();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#54545C");
                radiobutton.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                radiobutton.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                radiobutton.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                radiobutton.Height = 40;
                radiobutton.Width = 60;
                radiobutton.Name = uiTextName;
                radiobutton.ToolTip = "Tooltip Here";
                radiobutton.RenderTransform = transform;
                Canvas.SetZIndex(radiobutton, currentZIndex);
                canvas.Children.Add(radiobutton);
                Canvas.SetLeft(radiobutton, dropPoint.X);
                Canvas.SetTop(radiobutton, dropPoint.Y);

            }
            else if (type.Equals("slider"))
            {
                transform.Angle = 0;
                Slider slider = new Slider();
                currentUIInfo.UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
                currentUIInfo.UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
                currentUIInfo.UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#0054545C");
                slider.Background = new SolidColorBrush(currentUIInfo.UIELEMENT_BACKGROUND);
                slider.Foreground = new SolidColorBrush(currentUIInfo.UIELEMENT_FOREGROUND);
                slider.BorderBrush = new SolidColorBrush(currentUIInfo.UIELEMENT_BORDERGROUND);
                slider.Height = 40;
                slider.Width = 60;
                slider.Name = uiTextName;
                slider.ToolTip = "Tooltip Here";
                slider.RenderTransform = transform;
                Canvas.SetZIndex(slider, currentZIndex);
                canvas.Children.Add(slider);
                Canvas.SetLeft(slider, dropPoint.X);
                Canvas.SetTop(slider, dropPoint.Y);

            }

        }

        private string numbering(string type)
        {
            int no = 1;
            if (ui_Element_List.Count == 0) return type + 1;
            foreach (UIElementInfo uiInfo in ui_Element_List)
            {
                if (uiInfo.UIELEMENT_TYPE.Equals(type))
                    no++;
            }
            return type + no;
        }
         
    }
    
}