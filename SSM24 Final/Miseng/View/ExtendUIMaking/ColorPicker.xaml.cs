using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Miseng.ViewModel.UICanvas;

namespace Miseng.View.ExtendUIMaking
{
    /// <summary>
    /// ColorPicker.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorPicker : Control, INotifyPropertyChanged
    {
        ControlViewModel ctrVM;
        public event PropertyChangedEventHandler PropertyChanged;

        public Color _SelectedColor;
        public Color SelectedColor
        {
            get { return _SelectedColor; }
            set
            {
                _SelectedColor = value;
                UpdateHSB();
                UpdateHSBPosition();
                OnPropertyChanged("SelectedColor");
                OnPropertyChanged("Hue");
            }
        }

        public byte Alpha { get; set; }
        public double Hue { get; private set; }
        public double Saturation { get; private set; }
        public double Brightness { get; private set; }

        public FrameworkElement PART_HueHost { get; set; }
        public FrameworkElement PART_HuePicker { get; set; }
        public FrameworkElement PART_SBPicker { get; set; }
        public FrameworkElement PART_SBHost { get; set; }

        public ColorPicker()
        {
            SelectedColor = Colors.White;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
            LayoutUpdated += (s, e) => UpdateHSBPosition();
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
            }
        }

        public override void OnApplyTemplate()
        {
            PART_SBPicker = (FrameworkElement)GetTemplateChild("PART_SBPicker");
            PART_SBHost = (FrameworkElement)GetTemplateChild("PART_SBHost");

            PART_SBHost.PreviewMouseLeftButtonDown += delegate(object s, MouseButtonEventArgs e) { PART_SBHost.CaptureMouse(); CalculateSB(e); };
            PART_SBHost.PreviewMouseLeftButtonUp += (s, e) => PART_SBHost.ReleaseMouseCapture();
            PART_SBHost.PreviewMouseMove += (s, e) => CalculateSB(e);

            PART_HuePicker = (FrameworkElement)GetTemplateChild("PART_HuePicker");
            PART_HueHost = (FrameworkElement)GetTemplateChild("PART_HueHost");

            PART_HueHost.PreviewMouseLeftButtonDown += delegate(object s, MouseButtonEventArgs e) { PART_HueHost.CaptureMouse(); CalculateHue(e); };
            PART_HueHost.PreviewMouseLeftButtonUp += (s, e) => PART_HueHost.ReleaseMouseCapture();
            PART_HueHost.PreviewMouseMove += (s, e) => CalculateHue(e);

            base.OnApplyTemplate();
        }

        protected void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected void UpdateHSB()
        {
            double[] HSB = SelectedColor.ToHSB();
            Alpha = SelectedColor.A;
            Hue = HSB[0];
            Saturation = HSB[1];
            Brightness = HSB[2];

        }

        protected void UpdateColor()
        {
            _SelectedColor = ColorUtilities.CreateColorFromHSB(Alpha, Hue, Saturation, Brightness);

            Color tempBackground = SelectedColor;
            Color crrentBackground;

            crrentBackground = tempBackground;

            if (ctrVM != null)
            {
                ctrVM.ChagneOriginBackground(crrentBackground);
            }
            OnPropertyChanged("SelectedColor");
        }

        protected void UpdateHSBPosition()
        {
            if (PART_SBPicker == null || PART_SBHost == null) return;
            Canvas.SetTop(PART_HuePicker, PART_HueHost.ActualHeight * Hue / 360.0);
            Canvas.SetLeft(PART_SBPicker, PART_SBHost.ActualWidth * Saturation);
            Canvas.SetTop(PART_SBPicker, PART_SBHost.ActualHeight * (1 - Brightness));
        }

        protected void CalculateHue(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;

            Point CurrentPoint = e.GetPosition(PART_HueHost);

            if (CurrentPoint.Y < 0) CurrentPoint.Y = 0;
            if (CurrentPoint.Y > PART_HueHost.ActualHeight) CurrentPoint.Y = PART_HueHost.ActualHeight;

            Hue = 360.0 * (CurrentPoint.Y / PART_HueHost.ActualHeight);
            OnPropertyChanged("Hue");
            UpdateHSBPosition();
            UpdateColor();
        }

        protected void CalculateSB(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;

            Point CurrentPoint = e.GetPosition(PART_SBHost);

            if (CurrentPoint.X < 0) CurrentPoint.X = 0;
            if (CurrentPoint.X > PART_SBHost.ActualWidth) CurrentPoint.X = PART_SBHost.ActualWidth;

            if (CurrentPoint.Y < 0) CurrentPoint.Y = 0;
            if (CurrentPoint.Y > PART_SBHost.ActualHeight) CurrentPoint.Y = PART_SBHost.ActualHeight;

            Saturation = CurrentPoint.X / PART_SBHost.ActualWidth;
            Brightness = 1 - (CurrentPoint.Y / PART_SBHost.ActualHeight);
            UpdateHSBPosition();
            UpdateColor();
        }
    }
}
