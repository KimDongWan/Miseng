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
using System.Xml;
using System.Windows.Markup;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using System.Windows.Ink;
using System.Diagnostics;
using Miseng.ViewModel.UICanvas;
using Miseng.View.TabControls;

namespace Miseng.View.UICanvas
{
    public partial class UICanvasView : UserControl
    {
        public ControlViewModel ctrVM;
        UICanvasViewModel ucaVM;
        UIElement _draggedElement;
        Point _dragStartPoint;
        Canvas canvas;
        UIElement temp3;
        public UIElement selectedElement;
        object rightSelectElement;
        AdornerLayer layer;
        AdornerLayer aLayer;
        AdornerLayer bLayer;
        private bool _isPreMouseDown = false;
        private bool _isDragging = false;
        private bool _rightMouseDown = false;
        private double _originalLeft;
        private double _originalTop;
        private SimpleCircleAdorner _overlayElement;
        private ResizingAdorner preResizeAorner;
        private UIElement _cutItem;
        private UIElementInfo _cutInfo;
        public RotateAdorner _rotateAdorner;
        const double ScaleRate = 1.1;
        private string _draggedElementToString;
        private TabControlsView _tabControls;
        private UIElement agoSelectedElement;


        public UICanvasView()
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
                ucaVM = ctrVM._ucaVM;
                ctrVM._ucaVM.uicanV = this;
                _tabControls = ctrVM.tabControlsV;
                ctrVM.currentCanvas = MyCanvas;
                _cutItem = new UIElement();
                _cutInfo = new UIElementInfo();
            }
        }


        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            canvas = sender as Canvas;
            Point dropPoint = e.GetPosition(sender as UIElement);
            if (ctrVM._isMouseDown == true)
            {
                ucaVM.RenderingUICanvas(canvas, dropPoint, ctrVM.UIType);
                ctrVM._isMouseDown = false;
            }
            else
            {
            }
        }

        public void PreviewClickControl(string controlType, string controlName)
        {
            string childString;
            string temp2 = "test";
            foreach (UIElementInfo temp in ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(controlName))
                {
                    if (controlName == "MyCanvas")
                    {
                        temp2 = "MyCanvas";
                    }
                    if (temp2.Equals(controlName))
                    {
                        temp3 = MyCanvas;
                    }
                    else
                    {
                        foreach (UIElement child in MyCanvas.Children)
                        {
                            childString = child.ToString();
                            if (childString.Contains("RadioButton"))
                            {
                                temp2 = (child as RadioButton).Name;
                            }
                            else if (childString.Contains("TextBox"))
                            {
                                temp2 = (child as TextBox).Name;
                            }
                            else if (childString.Contains("PasswordBox"))
                            {
                                temp2 = (child as PasswordBox).Name;
                            }
                            else if (childString.Contains("ListBox"))
                            {
                                temp2 = (child as ListBox).Name;
                            }
                            else if (childString.Contains("ComboBox"))
                            {
                                temp2 = (child as ComboBox).Name;
                            }
                            else if (childString.Contains("Menu"))
                            {
                                temp2 = (child as Menu).Name;
                            }
                            else if (childString.Contains("Button"))
                            {
                                temp2 = (child as Button).Name;
                            }
                            else if (childString.Contains("Slider"))
                            {
                                temp2 = (child as Slider).Name;
                            }
                            if (temp2.Equals(controlName))
                            {
                                temp3 = child;
                            }

                        }
                    }
                }
            }


            if (selectedElement != null)
            {
                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                aLayer.Remove(preResizeAorner);
                bLayer.Remove(_rotateAdorner);
            }

            selectedElement = temp3;
            aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
            preResizeAorner = new ResizingAdorner(selectedElement, this);
            aLayer.Add(preResizeAorner);

            bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
            _rotateAdorner = new RotateAdorner(selectedElement, this);
            bLayer.Add(_rotateAdorner);

            string tempString = temp3.ToString();
            ctrVM.LeftButtonMakeTempListItem(temp3, tempString);
        }

        public void _PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string _typeName = null;
            if (agoSelectedElement != null)
            {
                _draggedElementToString = agoSelectedElement.ToString();
                _typeName = ctrVM.UIelementCheckTypeName(agoSelectedElement, _draggedElementToString);
                string xmlTag = ctrVM.fileTabV.scriptV.getBlocks();
                ctrVM.hiddenfileSave(_typeName, xmlTag);
                string jsCode = ctrVM.fileTabV.scriptV.getJSCode();
                ctrVM.JSFileSave(jsCode);
                ctrVM._ucaVM.DomtreeVM.importScriptSrc_To_HTMLDocument(ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js");
            }
            _draggedElementToString = (e.Source as UIElement).ToString();
            _tabControls.TabContols.SelectedIndex = 0;
            if (e.Source == MyCanvas || _draggedElementToString == "System.Windows.Controls.Grid")
            {
                if (selectedElement != null)
                {
                    aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                    bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                    if (aLayer != null && bLayer != null)
                    {
                        aLayer.Remove(preResizeAorner);
                        bLayer.Remove(_rotateAdorner);
                    }
                }
                _draggedElementToString = MyCanvas.ToString();
                selectedElement = MyCanvas;
                object canvasObject = MyCanvas;
                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                preResizeAorner = new ResizingAdorner(selectedElement, this);
                aLayer.Add(preResizeAorner);

                bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                _rotateAdorner = new RotateAdorner(selectedElement, this);
                bLayer.Add(_rotateAdorner);
                //여기오류
                ctrVM._ucaVM.currentUIInfo = ctrVM._ucaVM.UI_Element_List[0];
                ctrVM._ucaVM.preview.clickedControl("MyCanvas");
                ctrVM.LeftButtonMakeTempListItem(canvasObject, _draggedElementToString);
                string pathJS = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\Tizen\\" + ctrVM.current_projectName + "\\js\\" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";
                ctrVM.SrcCodeVM.GetSrcOfPathJS(pathJS);
                ctrVM.CURRENT_JS = ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";

            }
            else
            {
                _isPreMouseDown = true;
                _dragStartPoint = e.GetPosition(MyCanvas);
                _draggedElement = e.Source as UIElement;
                MyCanvas.CaptureMouse();
                e.Handled = true;

                if (selectedElement != null)
                {
                    aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                    bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                    aLayer.Remove(preResizeAorner);
                    bLayer.Remove(_rotateAdorner);
                }
                ctrVM._ucaVM.currentUIInfo = search_UIinfo(e.Source.ToString().Split(new char[] { ':', ' ' }).Last());
                selectedElement = e.Source as UIElement;
                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                preResizeAorner = new ResizingAdorner(selectedElement, this);
                aLayer.Add(preResizeAorner);

                bLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                _rotateAdorner = new RotateAdorner(selectedElement, this);
                bLayer.Add(_rotateAdorner);
                //ctrVM._ucaVM.preview 자꾸 널.. 버튼 눌렀다가 캔버스 눌렀다가 버튼누르면 널 .. 널죽이겠다..                

                ctrVM._ucaVM.preview.clickedControl(e.Source);
                ctrVM.LeftButtonMakeTempListItem(e.Source, _draggedElementToString);
                string pathJS = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\Tizen\\" + ctrVM.current_projectName + "\\js\\" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";
                ctrVM.SrcCodeVM.GetSrcOfPathJS(pathJS);
                ctrVM.CURRENT_JS = ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";

            }
            agoSelectedElement = selectedElement;

        }

        private UIElementInfo search_UIinfo(string id)
        {
            UIElementInfo result = null;

            foreach (UIElementInfo i in ctrVM._ucaVM.UI_Element_List)
            {
                if (i.UIELEMENT_ID == id)
                    result = i;
            }
            return result;

        }
        private UIElement GetTopContainer()
        {
            // return  LogicalTreeHelper.FindLogicalNode(Application.Current.MainWindow, "canvas") as UIElement;

            return Application.Current.MainWindow.Content as UIElement;
        }

        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isPreMouseDown)
            {
                if ((_isDragging == false) && ((Math.Abs(e.GetPosition(MyCanvas).X - _dragStartPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                     (Math.Abs(e.GetPosition(MyCanvas).Y - _dragStartPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                {
                    DragStarted();
                }
                if (_isDragging)
                {

                    DragMoved();
                }
            }
        }

        private bool IsDragGesture(Point point)
        {
            bool hGesture = Math.Abs(point.X - _dragStartPoint.X) > SystemParameters.MinimumHorizontalDragDistance;
            bool vGesture = Math.Abs(point.Y - _dragStartPoint.Y) > SystemParameters.MinimumVerticalDragDistance;

            return (hGesture | vGesture);
        }

        public void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MessageBox.Show("Double Click");
            }
        }

        private void DragStarted()
        {
            _isDragging = true;

            _originalLeft = Canvas.GetLeft(_draggedElement);
            _originalTop = Canvas.GetTop(_draggedElement);

            _overlayElement = new SimpleCircleAdorner(_draggedElement);
            layer = AdornerLayer.GetAdornerLayer(_draggedElement);
            layer.Add(_overlayElement);
        }

        private void DragMoved()
        {
            if (MyCanvas.IsMouseOver)
            {
                this.Cursor = Cursors.Hand;
                Point CurrentPosition = System.Windows.Input.Mouse.GetPosition(MyCanvas);
                _overlayElement.LeftOffset = CurrentPosition.X - _dragStartPoint.X;
                _overlayElement.TopOffset = CurrentPosition.Y - _dragStartPoint.Y;
            }
            else
            {
                MessageBox.Show("test");
                this.Cursor = Cursors.No;
            }
        }

        private void Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isPreMouseDown)
            {
                DragFinished(false);
                e.Handled = true;


            }
        }

        private void DragFinished(bool cancelled)
        {
            System.Windows.Input.Mouse.Capture(null);
            if (_isDragging)
            {
                AdornerLayer.GetAdornerLayer(_overlayElement.AdornedElement).Remove(_overlayElement);

                if (cancelled == false)
                {
                    Canvas.SetTop(_draggedElement, _originalTop + _overlayElement.TopOffset);
                    Canvas.SetLeft(_draggedElement, _originalLeft + _overlayElement.LeftOffset);
                    ctrVM.ChagneOriginXPosition(_originalLeft + _overlayElement.LeftOffset);
                    ctrVM.ChagneOriginYPosition(_originalTop + _overlayElement.TopOffset);
                }
                _overlayElement = null;

            }

            _isDragging = false;
            _isPreMouseDown = false;
        }

        private void MyCanvas_PreviewDragLeave(object sender, DragEventArgs e)
        {
            this.Cursor = Cursors.No;
        }

        private void MyCanvas_PreviewDragOver(object sender, DragEventArgs e)
        {

        }

        private void MyCanvas_PreviewDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Label)))
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void MyCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            if (e.Delta > 0)
            {
                st.ScaleX *= ScaleRate;
                st.ScaleY *= ScaleRate;
            }
            else
            {
                st.ScaleX /= ScaleRate;
                st.ScaleY /= ScaleRate;
            }
        }

        private void MyCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _rightMouseDown = true;
        }

        private void MyCanvas_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            rightSelectElement = e.Source;
            if (_rightMouseDown && e.Source.ToString().Contains("Canvas"))
            {
                ContextMenu cm = this.FindResource("cmCButton") as ContextMenu;
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
                _rightMouseDown = false;
            }
            else if (_rightMouseDown && !(e.Source.ToString().Contains("Canvas")))
            {
                string typeName = (e.Source as UIElement).ToString();
                ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
                if (typeName.Contains("ListBox"))
                {
                    cm = this.FindResource("listAddButton") as ContextMenu;
                }
                else if (typeName.Contains("ComboBox"))
                {
                    cm = this.FindResource("comboAddButton") as ContextMenu;
                }
                else if (typeName.Contains("Menu"))
                {
                    cm = this.FindResource("menuAddButton") as ContextMenu;
                }
                cm.PlacementTarget = sender as Button;
                cm.IsOpen = true;
                _rightMouseDown = false;
            }
        }

        private void ZindexF_Plus_Click(object sender, RoutedEventArgs e)
        {
            string typeName = null;
            int _zIndex = 0;
            int maxIndex = 0;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                typeName = (selectedElement as RadioButton).Name;
            }

            else if (_draggedElementToString.Contains("TextBox"))
            {
                typeName = (selectedElement as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                typeName = (selectedElement as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                typeName = (selectedElement as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                typeName = (selectedElement as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                typeName = (selectedElement as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                typeName = (selectedElement as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                typeName = (selectedElement as Button).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                typeName = (selectedElement as Slider).Name;
            }
            foreach (UIElementInfo temp in ucaVM.UI_Element_List)
            {
                if (maxIndex < temp.UIELEMENT_ZINDEX)
                {
                    maxIndex = temp.UIELEMENT_ZINDEX;
                }
            }
            maxIndex++;
            _zIndex = maxIndex;
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginZIndex(_zIndex);
            }
        }

        private void ZindexPlus_Click(object sender, RoutedEventArgs e)
        {
            string typeName = null;
            int _zIndex = 0;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                typeName = (selectedElement as RadioButton).Name;
            }
            else if (_draggedElementToString.Contains("TextBox"))
            {
                typeName = (selectedElement as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                typeName = (selectedElement as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                typeName = (selectedElement as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                typeName = (selectedElement as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                typeName = (selectedElement as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                typeName = (selectedElement as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                typeName = (selectedElement as Button).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                typeName = (selectedElement as Slider).Name;
            }
            foreach (UIElementInfo temp in ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(typeName))
                {
                    _zIndex = temp.UIELEMENT_ZINDEX;
                }
            }
            _zIndex++;
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginZIndex(_zIndex);
            }
        }

        private void ZindexMinus_Click(object sender, RoutedEventArgs e)
        {
            string typeName = null;
            int _zIndex = 0;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                typeName = (selectedElement as RadioButton).Name;
            }
            else if (_draggedElementToString.Contains("TextBox"))
            {
                typeName = (selectedElement as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                typeName = (selectedElement as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                typeName = (selectedElement as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                typeName = (selectedElement as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                typeName = (selectedElement as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                typeName = (selectedElement as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                typeName = (selectedElement as Button).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                typeName = (selectedElement as Slider).Name;
            }
            foreach (UIElementInfo temp in ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(typeName))
                {
                    _zIndex = temp.UIELEMENT_ZINDEX;
                }
            }
            _zIndex--;
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginZIndex(_zIndex);
            }
        }

        private void ZindexF_Minus_Click(object sender, RoutedEventArgs e)
        {
            string typeName = null;
            int _zIndex = 0;
            int minIndex = 50;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                typeName = (selectedElement as RadioButton).Name;
            }

            else if (_draggedElementToString.Contains("TextBox"))
            {
                typeName = (selectedElement as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                typeName = (selectedElement as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                typeName = (selectedElement as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                typeName = (selectedElement as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                typeName = (selectedElement as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                typeName = (selectedElement as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                typeName = (selectedElement as Button).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                typeName = (selectedElement as Slider).Name;
            }
            foreach (UIElementInfo temp in ucaVM.UI_Element_List)
            {
                if (minIndex > temp.UIELEMENT_ZINDEX)
                {
                    minIndex = temp.UIELEMENT_ZINDEX;
                }
            }
            minIndex--;
            _zIndex = minIndex;
            if (ctrVM != null)
            {
                ctrVM.ChagneOriginZIndex(_zIndex);
            }
        }

        private void drawingBrushGrid_Click(object sender, RoutedEventArgs e)
        {
            if (drawingBrushGrid.Visibility.ToString().Equals("Visible"))
            {
                drawingBrushGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                drawingBrushGrid.Visibility = Visibility.Visible;
            }
        }
        private void itemAdd_Click(object sender, RoutedEventArgs e)
        {
            string typeName = (e.Source as UIElement).ToString();
            if (typeName.Contains("List"))
            {
                /*    ListBoxItem itm = new ListBoxItem();
                    UIElementInfo currentUIInfo = new UIElementInfo();
                    currentUIInfo.UIELEMENT_TYPE = "ListBoxItem";
                    currentUIInfo.UIELEMENT_ID = "ListBoxItem1";
                    currentUIInfo.UIELEMENT_TEXT = "ListBoxItem1";
                    currentUIInfo.UIELEMENT_ZINDEX = 4;
                    ctrVM._ucaVM.currentUIInfo.UIELEMENT_LIST.Add(currentUIInfo);
                    (rightSelectElement as ListBox).Items.Add(itm);
                 */
            }
            else if (typeName.Contains("ComboBox"))
            {
                //      (rightSelectElement as ComboBox).
            }
            else if (typeName.Contains("Menu"))
            {
                //    (rightSelectElement as Menu).ContextMenu
            }
        }

        public void DeleteLeftEvent()
        {
            string _selctedStr = selectedElement.ToString();
            UIElement _selectedEle = new UIElement();
            _selectedEle = selectedElement as UIElement;
            foreach (UIElementInfo temp in ctrVM._ucaVM.UI_Element_List)
            {
                if (_selctedStr.Contains("RadioButton"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as RadioButton).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Button"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Button).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("TextBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as TextBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("PasswordBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as PasswordBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ListBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as ListBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ComboBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as ComboBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Menu"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Menu).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Slider"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Slider).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
            }
            selectedElement = null;
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\Tizen\\" + ctrVM.current_projectName + "\\js\\" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\hiddenFile\\" + ctrVM.current_projectName + "_" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void Delete_Control_Click(object sender, MouseButtonEventArgs e)
        {

            string _selctedStr = rightSelectElement.ToString();
            UIElement _selectedEle = new UIElement();
            _selectedEle = rightSelectElement as UIElement;
            foreach (UIElementInfo temp in ctrVM._ucaVM.UI_Element_List)
            {
                if (_selctedStr.Contains("RadioButton"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as RadioButton).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Button"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Button).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("TextBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as TextBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("PasswordBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as PasswordBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ListBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as ListBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ComboBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as ComboBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Menu"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Menu).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Slider"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Slider).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
            }
            selectedElement = null;
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\Tizen\\" + ctrVM.current_projectName + "\\js\\" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".js";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\hiddenFile\\" + ctrVM.current_projectName + "_" + ctrVM.current_Scene + "_" + ctrVM.current_typeName + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }
        public void cutEvent()
        {
            string _selctedStr = rightSelectElement.ToString();
            UIElement _selectedEle = new UIElement();
            _selectedEle = rightSelectElement as UIElement;
            foreach (UIElementInfo temp in ctrVM._ucaVM.UI_Element_List)
            {
                if (_selctedStr.Contains("RadioButton"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as RadioButton).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Button"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Button).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("TextBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as TextBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("PasswordBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as PasswordBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ListBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as ListBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ComboBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as ComboBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Menu"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Menu).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Slider"))
                {
                    if (temp.UIELEMENT_ID.Equals((rightSelectElement as Slider).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
            }
        }

        public void cutLeftEvent()
        {
            string _selctedStr = selectedElement.ToString();
            UIElement _selectedEle = new UIElement();
            _selectedEle = selectedElement as UIElement;
            foreach (UIElementInfo temp in ctrVM._ucaVM.UI_Element_List)
            {
                if (_selctedStr.Contains("RadioButton"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as RadioButton).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Button"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Button).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("TextBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as TextBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("PasswordBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as PasswordBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ListBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as ListBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("ComboBox"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as ComboBox).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Menu"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Menu).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
                else if (_selctedStr.Contains("Slider"))
                {
                    if (temp.UIELEMENT_ID.Equals((selectedElement as Slider).Name))
                    {
                        MyCanvas.Children.Remove(_selectedEle);
                        _cutItem = _selectedEle;
                        _cutInfo = temp;
                        ctrVM._ucaVM.UI_Element_List.Remove(temp);
                        ctrVM._ucaVM.Items.Remove(temp);
                        break;
                    }
                }
            }
        }

        private void Cut_Control_Click(object sender, MouseButtonEventArgs e)
        {
            cutEvent();
        }

        public void CopyEvent()
        {
            bool flag = true;
            foreach (UIElement controls in MyCanvas.Children)
            {
                if (_cutItem.ToString() == controls.ToString() || _cutItem.ToString() == "System.Windows.UIElement")
                {
                    flag = false;
                }
            }
            if (flag)
            {
                MyCanvas.Children.Add(_cutItem);
                ctrVM._ucaVM.UI_Element_List.Add(_cutInfo);
                ctrVM._ucaVM.Items.Add(_cutInfo);
            }
        }

        private void Copy_Control_Click(object sender, MouseButtonEventArgs e)
        {
            CopyEvent();
        }

        private void JsCodeViewMenu_Click(object sender, RoutedEventArgs e)
        {
            ctrVM.fileSourceCode.TabContols.SelectedIndex = 1;
        }
    }

    public class SimpleCircleAdorner : Adorner
    {
        // Be sure to call the base class constructor.
        public SimpleCircleAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            VisualBrush _brush = new VisualBrush(adornedElement);

            _child = new Rectangle();
            _child.Width = adornedElement.RenderSize.Width;
            _child.Height = adornedElement.RenderSize.Height;


            DoubleAnimation animation = new DoubleAnimation(0.3, 1, new Duration(TimeSpan.FromSeconds(1)));
            animation.AutoReverse = true;
            animation.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
            _brush.BeginAnimation(System.Windows.Media.Brush.OpacityProperty, animation);

            _child.Fill = _brush;
        }

        // A common way to implement an adorner's rendering behavior is to override the OnRender
        // method, which is called by the layout subsystem as part of a rendering pass.
        protected override void OnRender(DrawingContext drawingContext)
        {
            // Get a rectangle that represents the desired size of the rendered element
            // after the rendering pass.  This will be used to draw at the corners of the 
            // adorned element.
            Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

            // Some arbitrary drawing implements.
            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
            renderBrush.Opacity = 0.2;
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
            double renderRadius = 5.0;

            // Just draw a circle at each corner.
            drawingContext.DrawRectangle(renderBrush, renderPen, adornedElementRect);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
            drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        public double LeftOffset
        {
            get
            {
                return _leftOffset;
            }
            set
            {
                _leftOffset = value;
                UpdatePosition();
            }
        }

        public double TopOffset
        {
            get
            {
                return _topOffset;
            }
            set
            {
                _topOffset = value;
                UpdatePosition();

            }
        }

        private void UpdatePosition()
        {
            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
            {
                adornerLayer.Update(AdornedElement);
            }
        }

        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(_leftOffset, _topOffset));
            return result;
        }

        private Rectangle _child = null;
        private double _leftOffset = 0;
        private double _topOffset = 0;
    }

    public class RotateAdorner : Adorner
    {
        // Resizing adorner uses Thumbs for visual elements.  
        // The Thumbs have built-in mouse input handling.
        Thumb bottomRight;
        private double initialAngle;
        private RotateTransform rotateTransform;
        private Vector startVector;
        private Point centerPoint;
        private UIElement designerItem;
        private Canvas canvas;
        private UIElement _adornedElement;
        public double angle;

        UICanvasView _uiCanV;

        // To store and manage the adorner's visual children.
        VisualCollection visualChildren;

        // Initialize the ResizingAdorner.
        public RotateAdorner(UIElement adornedElement, UICanvasView uiCanV)
            : base(adornedElement)
        {
            _adornedElement = adornedElement;
            visualChildren = new VisualCollection(this);
            _uiCanV = uiCanV;
            // Call a helper method to initialize the Thumbs
            // with a customized cursors.
            BuildAdornerCorner(ref bottomRight, Cursors.Hand);

            // Add handlers for resizing.
            bottomRight.DragDelta += new DragDeltaEventHandler(HandleBottomRight);
            bottomRight.DragStarted += new DragStartedEventHandler(RotateThumb_DragStarted);

        }

        // Handler for resizing from the bottom-right.
        void HandleBottomRight(object sender, DragDeltaEventArgs args)
        {

            if (this.designerItem != null && this.canvas != null)
            {
                Point currentPoint = Mouse.GetPosition(this.canvas);
                Vector deltaVector = Point.Subtract(currentPoint, this.centerPoint);
                angle = Vector.AngleBetween(this.startVector, deltaVector);
                RotateTransform rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                rotateTransform.Angle = this.initialAngle + Math.Round(angle, 0);
                this.designerItem.InvalidateMeasure();

                string _draggingEle = _adornedElement.ToString();
                //    _uiCanV.ctrVM.MakeTempListItem(_adornedElement, _draggingEle, _uiCanV._rotateAdorner.angle);
                _uiCanV.ctrVM.MakeTempListItem(_adornedElement, _draggingEle, rotateTransform.Angle);
            }
        }

        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element that's being adorned.  
            // These will be used to place the ResizingAdorner at the corners of the adorned element.  
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2 + 10, desiredHeight - adornerHeight / 2 + 10, adornerWidth, adornerHeight));

            // Return the final size.
            return finalSize;
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property, 
        // set some appearance properties, and add the elements to the visual tree.
        void BuildAdornerCorner(ref Thumb cornerThumb, Cursor customizedCursor)
        {
            if (cornerThumb != null) return;

            cornerThumb = new Thumb();

            // Set some arbitrary visual characteristics.
            cornerThumb.Cursor = customizedCursor;
            cornerThumb.Height = cornerThumb.Width = 10;
            cornerThumb.Opacity = 0.40;
            cornerThumb.Background = new SolidColorBrush(Colors.MediumBlue);

            visualChildren.Add(cornerThumb);
        }

        // This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        // Width and Height values of Double.NaN.  Because this Adorner explicitly resizes, the Width and Height
        // need to be set first.  It also sets the maximum size of the adorned element.
        void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
        // Override the VisualChildrenCount and GetVisualChild properties to interface with 
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }


        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            this.designerItem = adornedElement as UIElement;

            if (designerItem.ToString().Contains("RadioButton"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as RadioButton).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as RadioButton).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
            else if (designerItem.ToString().Contains("Button"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as Button).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as Button).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
            else if (designerItem.ToString().Contains("TextBox"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as TextBox).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as TextBox).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
            else if (designerItem.ToString().Contains("Image"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as Image).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as Image).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
            else if (designerItem.ToString().Contains("PasswordBox"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as PasswordBox).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as PasswordBox).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
            else if (designerItem.ToString().Contains("ListBox"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as ListBox).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as ListBox).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }

            else if (designerItem.ToString().Contains("ComboBox"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as ComboBox).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as ComboBox).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }

            else if (designerItem.ToString().Contains("Menu"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as Menu).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as Menu).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }

            else if (designerItem.ToString().Contains("Slider"))
            {
                if (this.designerItem != null)
                {
                    this.canvas = VisualTreeHelper.GetParent(this.designerItem) as Canvas;
                    if (this.canvas != null)
                    {
                        this.centerPoint = this.designerItem.TranslatePoint(
                        new Point((this.designerItem as Slider).Width * this.designerItem.RenderTransformOrigin.X,
                        (this.designerItem as Slider).Height * this.designerItem.RenderTransformOrigin.Y),
                        this.canvas);
                        Point startPoint = Mouse.GetPosition(this.canvas);
                        this.startVector = Point.Subtract(startPoint, this.centerPoint);
                        this.rotateTransform = this.designerItem.RenderTransform as RotateTransform;
                        if (this.rotateTransform == null)
                        {
                            this.designerItem.RenderTransform = new RotateTransform(0);
                            this.initialAngle = 0;
                        }
                        else
                        {
                            this.initialAngle = this.rotateTransform.Angle;
                        }
                    }
                }
            }
        }

    }

    public class ResizingAdorner : Adorner
    {
        // Resizing adorner uses Thumbs for visual elements.  
        // The Thumbs have built-in mouse input handling.
        Thumb topLeft, topRight, bottomLeft, bottomRight;

        UICanvasView _uiCanV;

        // To store and manage the adorner's visual children.
        VisualCollection visualChildren;

        // Initialize the ResizingAdorner.
        public ResizingAdorner(UIElement adornedElement, UICanvasView uiCanV)
            : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);
            _uiCanV = uiCanV;
            // Call a helper method to initialize the Thumbs
            // with a customized cursors.
            BuildAdornerCorner(ref topLeft, Cursors.SizeNWSE);
            BuildAdornerCorner(ref topRight, Cursors.SizeNESW);
            BuildAdornerCorner(ref bottomLeft, Cursors.SizeNESW);
            BuildAdornerCorner(ref bottomRight, Cursors.SizeNWSE);

            // Add handlers for resizing.
            bottomLeft.DragDelta += new DragDeltaEventHandler(HandleBottomLeft);
            bottomRight.DragDelta += new DragDeltaEventHandler(HandleBottomRight);
            topLeft.DragDelta += new DragDeltaEventHandler(HandleTopLeft);
            topRight.DragDelta += new DragDeltaEventHandler(HandleTopRight);

        }

        // Handler for resizing from the bottom-right.
        void HandleBottomRight(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;
            if (adornedElement.ToString().Contains("Canvas")) return;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            adornedElement.Width = Math.Round(Math.Max(adornedElement.Width + args.HorizontalChange, hitThumb.DesiredSize.Width), 2);
            adornedElement.Height = Math.Round(Math.Max(args.VerticalChange + adornedElement.Height, hitThumb.DesiredSize.Height), 2);

            string _draggingEle = adornedElement.ToString();
            _uiCanV.ctrVM.MakeTempListItem(adornedElement, _draggingEle, _uiCanV._rotateAdorner.angle);
        }

        // Handler for resizing from the top-right.
        void HandleTopRight(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;
            if (adornedElement.ToString().Contains("Canvas")) return;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            adornedElement.Width = Math.Max(adornedElement.Width + args.HorizontalChange, hitThumb.DesiredSize.Width);
            //adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);

            double height_old = adornedElement.Height;
            double height_new = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);
            double top_old = Canvas.GetTop(adornedElement);
            adornedElement.Height = height_new;
            Canvas.SetTop(adornedElement, top_old - (height_new - height_old));

            string _draggingEle = adornedElement.ToString();
            _uiCanV.ctrVM.MakeTempListItem(adornedElement, _draggingEle, _uiCanV._rotateAdorner.angle);
        }

        // Handler for resizing from the top-left.
        void HandleTopLeft(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;
            if (adornedElement.ToString().Contains("Canvas")) return;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            //adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
            //adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);

            double width_old = adornedElement.Width;
            double width_new = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
            double left_old = Canvas.GetLeft(adornedElement);
            adornedElement.Width = width_new;
            Canvas.SetLeft(adornedElement, left_old - (width_new - width_old));

            double height_old = adornedElement.Height;
            double height_new = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);
            double top_old = Canvas.GetTop(adornedElement);
            adornedElement.Height = height_new;
            Canvas.SetTop(adornedElement, top_old - (height_new - height_old));

            string _draggingEle = adornedElement.ToString();
            _uiCanV.ctrVM.MakeTempListItem(adornedElement, _draggingEle, _uiCanV._rotateAdorner.angle);
        }

        // Handler for resizing from the bottom-left.
        void HandleBottomLeft(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;
            if (adornedElement.ToString().Contains("Canvas")) return;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            //adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
            adornedElement.Height = Math.Max(args.VerticalChange + adornedElement.Height, hitThumb.DesiredSize.Height);

            double width_old = adornedElement.Width;
            double width_new = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
            double left_old = Canvas.GetLeft(adornedElement);
            adornedElement.Width = width_new;
            Canvas.SetLeft(adornedElement, left_old - (width_new - width_old));

            string _draggingEle = adornedElement.ToString();
            _uiCanV.ctrVM.MakeTempListItem(adornedElement, _draggingEle, _uiCanV._rotateAdorner.angle);
        }

        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element that's being adorned.  
            // These will be used to place the ResizingAdorner at the corners of the adorned element.  
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            bottomLeft.Arrange(new Rect(-adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            // Return the final size.
            return finalSize;
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property, 
        // set some appearance properties, and add the elements to the visual tree.
        void BuildAdornerCorner(ref Thumb cornerThumb, Cursor customizedCursor)
        {
            if (cornerThumb != null) return;

            cornerThumb = new Thumb();

            // Set some arbitrary visual characteristics.
            cornerThumb.Cursor = customizedCursor;
            cornerThumb.Height = cornerThumb.Width = 10;
            cornerThumb.Opacity = 0.40;
            cornerThumb.BorderThickness = new Thickness(1);
            cornerThumb.Background = new SolidColorBrush(Colors.Gray);
            //cornerThumb.Background = new SolidColorBrush(Colors.MediumBlue);

            visualChildren.Add(cornerThumb);
        }

        // This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        // Width and Height values of Double.NaN.  Because this Adorner explicitly resizes, the Width and Height
        // need to be set first.  It also sets the maximum size of the adorned element.
        void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
        // Override the VisualChildrenCount and GetVisualChild properties to interface with 
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }
    }


}
