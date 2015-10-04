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
using System.ComponentModel;
using Miseng.View.FileTab;
using System.Collections.ObjectModel;
using Miseng.View.TabControls;
using Miseng.View.Script;
using Miseng.View.FunctionMaking;
using Miseng.View.ExtendUIMaking;
using Miseng.View.ExtendFunctionMaking;
using Miseng.View;
using Miseng.View.Intro;
using Miseng.View.Attribute;
using Miseng.View.Preview;
namespace Miseng.ViewModel.UICanvas
{
    public class ControlViewModel : INotifyPropertyChanged
    {
       
        private string _uiType;
        private string _tempId;
        private string _tempType;
        private Color _tempBackGround;
        private Color _tempForeGround;
        private Color _tempBorderGround;
        private double _tempLThickness;
        private double _tempRThickness;
        private double _tempUThickness;
        private double _tempDThickness;
        private string _tempText;
        private string _tempCursor;
        private string _tempVisivility;
        private string _tempToolTip;
        private double _tempHeight;
        private double _tempWidth;
        private double _tempXPosition;
        private double _tempYPosition;
        private double _tempAngle;
        private double _tempOpacity;
        private double _tempBoderOpacity;
        private int _tempZIndex;
        private int _tempFontSize;
        public string SelectedTypeId;
        public string SelectedCanvasText;
        public bool _isMouseDown = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public FunctionMakingView functionMV;
        public UICanvasViewModel _ucaVM;
        public Canvas currentCanvas;
        private UIElementInfo _uiEleInfo;
        private DirInfo _currentDirectory;
        private DirInfo _updateDirectory;
        public FileTabView fileTabV;
        public TabControlsView tabControlsV;
        public string projectAndfileName;
        private FileExplorerViewModel _fileTreeVM;
        private SrcCodeViewModel _srcCodeVM;
        private ICommand _showTreeCommand;
        private string fileFullName;
        public ScriptView scriptV;
        public string current_Scene;
        public string current_S;
        public string cureent_Js;
        public ExtendUIMakingView extendUIV;
        public ExtendFunctionMakingView extendFunctionMV;
        public string current_typeName;
        public string current_projectName;
        public string _containTooltip;
        private double _rotX;
        private double _rotY;
        private double _rotZ;
        private double _distance;
        private double _speed;
        public IntroView _intro;
        public Grid _panelGrid;
        public FileSourceCode fileSourceCode;
        public AttributeView _attributeV;
        public PreviewImage _previewImageV;
        private ObservableCollection<string> agoFileList;
        private ObservableCollection<int> fontSizeList;
        public ControlViewModel()
        {
            _tempToolTip = "Tooltip Here";
            _tempHeight = 0;
            _tempWidth = 0;
            _tempCursor = "Arrow";
            _tempAngle = 0;
            _tempVisivility = "Visible";
            _tempOpacity = 255;
            _tempBoderOpacity = 0;
            _tempFontSize = 11;
            _containTooltip = "선택된 애니메이션이 없습니다.";
            _rotX = 0;
            _rotY = 0;
            _rotZ = 0;
            _distance = 50;
            _speed = 1.5;
            current_S = "HTML";
            cureent_Js = "JavaScript";
            FileTreeVM = new FileExplorerViewModel(this);
            SrcCodeVM = new SrcCodeViewModel(this);   
        }

        public void writeFileStory()
        {
            MessageBoxResult result1;
            string orderProjectPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\basicMissengProject\\opendForder\\order.txt";

            if (!File.Exists(orderProjectPath))
            {
                result1 = MessageBox.Show("order.txt가 존재하지 않습니다.", "오류");
                return;

            }
            string line;
            string agoLine = "test232323";
            string finalFIle = "Test232";
            agoFileList = new ObservableCollection<string>();
            using (var reader = File.OpenText(orderProjectPath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (agoLine != line && line!="" && finalFIle!= line)
                    {
                        AGOFILELIST.Add(line);
                        _intro.vDataBinding.ItemsSource = AGOFILELIST;
                        finalFIle = line;
                    }
                    agoLine = line;
                }
            }
        }

        public void fontSizeCollection()
        {
            fontSizeList = new ObservableCollection<int>();
            for (int i = 1; i <= 25; i++)
            {
                fontSizeList.Add(i);
                extendUIV.FontSize.ItemsSource = fontSizeList;
            }
        }
            //
        

        public ObservableCollection<string> AGOFILELIST
        {
            get
            {
                return agoFileList;
            }
            set
            {
                agoFileList = value;
                OnPropertyChanged("AGOFILELIST");
            }
        }

        public ObservableCollection<int> FONTSIZELIST
        {
            get
            {
                return fontSizeList;
            }
            set
            {
                fontSizeList = value;
                OnPropertyChanged("FONTSIZELIST");
            }
        }

        public int TempFontSize
        {
            get
            {
                return _tempFontSize;
            }
            set
            {
                _tempFontSize = value;
                OnPropertyChanged("TempFontSize");
            }
        }

        public double TempOpacity
        {
            get
            {
                return _tempOpacity;
            }
            set
            {
                _tempOpacity = value;
                OnPropertyChanged("TempOpacity");
            }
        }

      

        public double TempBoderOpacity
        {
            get
            {
                return _tempBoderOpacity;
            }
            set
            {
                _tempBoderOpacity = value;
                OnPropertyChanged("TempBoderOpacity");
            }
        } 


        public string CURRENT_JS
        {
            get { return cureent_Js; }
            set
            {
                cureent_Js = value;
                OnPropertyChanged("CURRENT_JS");
            }
        }

        public string CURRENT_SCENE
        {
            get { return current_S; }
            set
            {
                current_S = value;
                OnPropertyChanged("CURRENT_SCENE");
            }
        }
        public double ROTX
        {
            get { return _rotX; }
            set
            {
                _rotX = value;
                OnPropertyChanged("ROTX");
            }
        }

        public double ROTY
        {
            get { return _rotY; }
            set
            {
                _rotY = value;
                OnPropertyChanged("ROTY");
            }
        }

        public double ROTZ
        {
            get { return _rotZ; }
            set
            {
                _rotZ = value;
                OnPropertyChanged("ROTZ");
            }
        }

        public double DISTANCE
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged("DISTANCE");
            }
        }

        public double SPEED
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnPropertyChanged("SPEED");
            }
        }

        public string ContainTooltip
        {
            get { return _containTooltip; }
            set
            {
                _containTooltip = value;
                OnPropertyChanged("ContainTooltip");
            }
        }


        public DirInfo CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;
                OnPropertyChanged("CurrentDirectory");
            }
        }
        public DirInfo UpdateDirectory
        {
            get { return _updateDirectory; }
            set { _updateDirectory = value; }
        }


        /// <summary>
        /// Tree View model
        /// </summary>
        public FileExplorerViewModel FileTreeVM
        {
            get { return _fileTreeVM; }
            set
            {
                _fileTreeVM = value;
                OnPropertyChanged("FileTreeVM");
            }
        }

        public SrcCodeViewModel SrcCodeVM
        {
            get { return _srcCodeVM; }
            set
            {
                _srcCodeVM = value;
                OnPropertyChanged("SrcCodeVM");
            }
        }
        public ICommand ShowTreeCommand
        {
            get { return _showTreeCommand; }
            set
            {
                _showTreeCommand = value;
                OnPropertyChanged("ShowTreeCommand");
            }
        }
        public void Update_ExplorerViewModel()
        {
            FileTreeVM.UpDateFileExplorerViewModel();
        }


        public string UIType
        {
            get
            {
                return _uiType;
            }
            set
            {
                if (_uiType != null)
                {
                    _uiType = null;
                }
                _uiType = value;
                OnPropertyChanged("UIType");
            }
        }

        public string TempType
        {
            get
            {
                return _tempType;
            }
            set
            {
                if (_tempType != null)
                {
                    _tempType = null;
                }
                _tempType = value;
                OnPropertyChanged("TempType");
            }
        }

        public double TempHeight
        {
            get
            {
                return _tempHeight;
            }
            set
            {
                _tempHeight = value;
                OnPropertyChanged("TempHeight");
            }
        }

        public double TempWidth
        {
            get
            {
                return _tempWidth;
            }
            set
            {
                _tempWidth = value;
                OnPropertyChanged("TempWidth");
            }
        }

        public string TempId
        {
            get
            {
                return _tempId;
            }
            set
            {
                _tempId = value;
                OnPropertyChanged("TempId");
            }
        }

        public Color TempBackGround
        {
            get
            {
                return _tempBackGround;
            }
            set
            {
                _tempBackGround = value;
                OnPropertyChanged("TempBackGround");
            }
        }

        public Color TempForeGround
        {
            get
            {
                return _tempForeGround;
            }
            set
            {
                _tempForeGround = value;
                OnPropertyChanged("TempForeGround");
            }
        }
        public Color TempBorderGround
        {
            get
            {
                return _tempBorderGround;
            }
            set
            {
                _tempBorderGround = value;
                OnPropertyChanged("TempBorderGround");
            }
        }

        public double TempLThickness
        {
            get
            {
                return _tempLThickness;
            }
            set
            {
                _tempLThickness = value;
                OnPropertyChanged("TempLThickness");
            }
        }

        public double TempRThickness
        {
            get
            {
                return _tempRThickness;
            }
            set
            {
                _tempRThickness = value;
                OnPropertyChanged("TempRThickness");
            }
        }

        public double TempUThickness
        {
            get
            {
                return _tempUThickness;
            }
            set
            {
                _tempUThickness = value;
                OnPropertyChanged("TempUThickness");
            }
        }

        public double TempDThickness
        {
            get
            {
                return _tempDThickness;
            }
            set
            {
                _tempDThickness = value;
                OnPropertyChanged("TempDThickness");
            }
        }

        public string TempText
        {
            get
            {
                return _tempText;
            }
            set
            {
                _tempText = value;
                OnPropertyChanged("TempText");
            }
        }

        public string TempToolTip
        {
            get
            {
                return _tempToolTip;
            }
            set
            {
                _tempToolTip = value;
                OnPropertyChanged("TempToolTip");
            }
        }

        public double TempXPosition
        {
            get
            {
                return _tempXPosition;
            }
            set
            {
                _tempXPosition = value;
                OnPropertyChanged("TempXPosition");
            }
        }

        public double TempYPosition
        {
            get
            {
                return _tempYPosition;
            }
            set
            {
                _tempYPosition = value;
                OnPropertyChanged("TempYPosition");
            }
        }

        public double TempAngle
        {
            get
            {
                return _tempAngle;
            }
            set
            {
                _tempAngle = value;
                OnPropertyChanged("TempAngle");
            }
        }

        public int TempZIndex
        {
            get
            {
                return _tempZIndex;
            }
            set
            {
                _tempZIndex = value;
                OnPropertyChanged("TempZIndex");
            }
        }

        public string TempCursor
        {
            get
            {
                return _tempCursor;
            }
            set
            {
                _tempCursor = value;
                OnPropertyChanged("TempCursor");
            }
        }

        public string TempVisivility
        {
            get
            {
                return _tempVisivility;
            }
            set
            {
                _tempVisivility = value;
                OnPropertyChanged("TempVisivility");
            }
        }
        public void JSFileSave(string _jsString)
        {         
            string filePath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\"+current_projectName+"\\Tizen\\"+current_projectName+"\\js\\" + current_Scene+"_"+current_typeName + ".js";
            System.IO.File.WriteAllText(filePath, _jsString);
        }
        public void hiddenfileSave(string _typeName,string writeBuffer)
        {
            fileFullName = projectAndfileName + "_" + _typeName;
            string filePath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + current_projectName +"\\hiddenFile\\" + fileFullName + ".txt";            
            System.IO.File.WriteAllText(filePath, writeBuffer);
        }

        public string hiddenfileLoad(string _typeName)
        {
            fileFullName = projectAndfileName + "_" + _typeName;
            string filePath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + current_projectName +"\\hiddenFile\\" + fileFullName + ".txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                
            }
            return System.IO.File.ReadAllText(filePath);                        
        }

        public string CheckTypeName(object eSource, string _draggedElementToString)
        {
            string _typeName = null;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                _typeName = (eSource as RadioButton).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                _typeName = (eSource as Button).Name;
            }
            else if (_draggedElementToString.Contains("TextBox"))
            {
                _typeName = (eSource as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                _typeName = (eSource as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                _typeName = (eSource as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("Canvas"))
            {
                _typeName = (eSource as Canvas).Name;
            }
            else if (_draggedElementToString.Contains("ListBoxItem"))
            {
                _typeName = (eSource as ListBoxItem).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                _typeName = (eSource as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                _typeName = (eSource as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                _typeName = (eSource as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                _typeName = (eSource as Slider).Name;
            }
            return _typeName;
        }
        public string UIelementCheckTypeName(UIElement eSource, string _draggedElementToString)
        {
            string _typeName = null;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                _typeName = (eSource as RadioButton).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                _typeName = (eSource as Button).Name;
            }
            else if (_draggedElementToString.Contains("TextBox"))
            {
                _typeName = (eSource as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                _typeName = (eSource as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                _typeName = (eSource as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("Canvas"))
            {
                _typeName = (eSource as Canvas).Name;
            }
            else if (_draggedElementToString.Contains("ListBoxItem"))
            {
                _typeName = (eSource as ListBoxItem).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                _typeName = (eSource as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                _typeName = (eSource as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                _typeName = (eSource as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                _typeName = (eSource as Slider).Name;
            }
            return _typeName;
        }

        public void LeftButtonMakeTempListItem(object eSource, string _draggedElementToString)
        {
            string typeName = null;
            typeName = CheckTypeName(eSource, _draggedElementToString);
            current_typeName = typeName;
            SelectedTypeId = typeName;
            SelectedCanvasText = _draggedElementToString;
            _uiEleInfo = SearchList(typeName);
            fileTabV.scriptV._webBrowser.InvokeScript("setStartXML", new object[] {_uiEleInfo.UIELEMENT_ID,_uiEleInfo.UIELEMENT_TYPE,current_Scene, hiddenfileLoad(typeName)});
            fileTabV.scriptV.AddMouseMoveEvnet();

            if (_draggedElementToString.Contains("RadioButton"))
            {
                RadioButton radiobutton = eSource as RadioButton;
                _uiEleInfo.UIELEMENT_HEIGHT = radiobutton.Height;
                _uiEleInfo.UIELEMENT_WIDTH = radiobutton.Width;
            }           
            else if (_draggedElementToString.Contains("TextBox"))
            {
                TextBox txtBox = eSource as TextBox;
                _uiEleInfo.UIELEMENT_HEIGHT = txtBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = txtBox.Width;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                Image image = eSource as Image;
                _uiEleInfo.UIELEMENT_HEIGHT = image.Height;
                _uiEleInfo.UIELEMENT_WIDTH = image.Width;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                PasswordBox passwordBox = eSource as PasswordBox;
                _uiEleInfo.UIELEMENT_HEIGHT = passwordBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = passwordBox.Width;
            }
            else if (_draggedElementToString.Contains("Canvas"))
            {
            }
            else if (_draggedElementToString.Contains("ListBoxItem"))
            {
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                ListBox listBox = eSource as ListBox;
                _uiEleInfo.UIELEMENT_HEIGHT = listBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = listBox.Width;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                ComboBox combobox = eSource as ComboBox;
                _uiEleInfo.UIELEMENT_HEIGHT = combobox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = combobox.Width;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                Menu menu = eSource as Menu;
                _uiEleInfo.UIELEMENT_HEIGHT = menu.Height;
                _uiEleInfo.UIELEMENT_WIDTH = menu.Width;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                Button btn = eSource as Button;
                _uiEleInfo.UIELEMENT_HEIGHT = btn.Height;
                _uiEleInfo.UIELEMENT_WIDTH = btn.Width;
                
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                Slider slider = eSource as Slider;
                _uiEleInfo.UIELEMENT_HEIGHT = slider.Height;
                _uiEleInfo.UIELEMENT_WIDTH = slider.Width;
            }
        }

        public void MakeTempListItem(object eSource, string _draggedElementToString, double angle)
        {
            string typeName = null;
            if (_draggedElementToString.Contains("RadioButton"))
            {
                typeName = (eSource as RadioButton).Name;
            }            
            else if (_draggedElementToString.Contains("TextBox"))
            {
                typeName = (eSource as TextBox).Name;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                typeName = (eSource as Image).Name;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                typeName = (eSource as PasswordBox).Name;
            }
            else if (_draggedElementToString.Contains("Canvas"))
            {
                typeName = (eSource as Canvas).Name;
            }
            else if (_draggedElementToString.Contains("ListBoxItem"))
            {
                typeName = (eSource as ListBoxItem).Name;
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                typeName = (eSource as ListBox).Name;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                typeName = (eSource as ComboBox).Name;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                typeName = (eSource as Menu).Name;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                typeName = (eSource as Button).Name;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                typeName = (eSource as Slider).Name;
            }

            _uiEleInfo.UIELEMENT_ANGLE = angle;
            SelectedTypeId = typeName;
            SelectedCanvasText = _draggedElementToString;
            _uiEleInfo = SearchList(typeName);

            if (_draggedElementToString.Contains("RadioButton"))
            {
                RadioButton radiobutton = eSource as RadioButton;
                _uiEleInfo.UIELEMENT_HEIGHT = radiobutton.Height;
                _uiEleInfo.UIELEMENT_WIDTH = radiobutton.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            
            else if (_draggedElementToString.Contains("TextBox"))
            {
                TextBox txtBox = eSource as TextBox;
                _uiEleInfo.UIELEMENT_HEIGHT = txtBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = txtBox.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("Image"))
            {
                Image image = eSource as Image;
                _uiEleInfo.UIELEMENT_HEIGHT = image.Height;
                _uiEleInfo.UIELEMENT_WIDTH = image.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("PasswordBox"))
            {
                PasswordBox passwordBox = eSource as PasswordBox;
                _uiEleInfo.UIELEMENT_HEIGHT = passwordBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = passwordBox.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("Canvas"))
            {
                Canvas canvas = eSource as Canvas;
                _uiEleInfo.UIELEMENT_HEIGHT = canvas.Height;
                _uiEleInfo.UIELEMENT_WIDTH = canvas.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("ListBoxItem"))
            {
            }
            else if (_draggedElementToString.Contains("ListBox"))
            {
                ListBox listBox = eSource as ListBox;
                _uiEleInfo.UIELEMENT_HEIGHT = listBox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = listBox.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("ComboBox"))
            {
                ComboBox combobox = eSource as ComboBox;
                _uiEleInfo.UIELEMENT_HEIGHT = combobox.Height;
                _uiEleInfo.UIELEMENT_WIDTH = combobox.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("Menu"))
            {
                Menu menu = eSource as Menu;
                _uiEleInfo.UIELEMENT_HEIGHT = menu.Height;
                _uiEleInfo.UIELEMENT_WIDTH = menu.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("Button"))
            {
                Button btn = eSource as Button;
                _uiEleInfo.UIELEMENT_HEIGHT = btn.Height;
                _uiEleInfo.UIELEMENT_WIDTH = btn.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }
            else if (_draggedElementToString.Contains("Slider"))
            {
                Slider slider = eSource as Slider;
                _uiEleInfo.UIELEMENT_HEIGHT = slider.Height;
                _uiEleInfo.UIELEMENT_WIDTH = slider.Width;
                _uiEleInfo.UIELEMENT_ANGLE = angle;
            }

        }

        public UIElementInfo SearchList(string typeName)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(typeName))
                {
                    TempId = temp.UIELEMENT_ID;
                    TempType = temp.UIELEMENT_TYPE;
                    TempText = temp.UIELEMENT_TEXT;
                    TempHeight = temp.UIELEMENT_HEIGHT;
                    TempWidth = temp.UIELEMENT_WIDTH;
                    TempAngle = temp.UIELEMENT_ANGLE;
                    TempZIndex = temp.UIELEMENT_ZINDEX;
                    TempOpacity = temp.UIELEMENT_OPACITY;
                    TempBoderOpacity = temp.UIELEMENT_BORDER_OPACITY;
                    TempXPosition = temp.UIELEMENT_XPOSITION;
                    TempYPosition = temp.UIELEMENT_YPOSITION;
                    TempBackGround = temp.UIELEMENT_BACKGROUND;
                    TempForeGround = temp.UIELEMENT_FOREGROUND;
                    TempBorderGround = temp.UIELEMENT_BORDERGROUND;
                    TempToolTip = temp.UIELEMENT_TOOLTIP;
                    TempLThickness = temp.UIELEMENT_LTHICKNESS;
                    TempRThickness = temp.UIELEMENT_RTHICKNESS;
                    TempUThickness = temp.UIELEMENT_UTHICKNESS;
                    TempDThickness = temp.UIELEMENT_DTHICKNESS;
                    TempFontSize = temp.UIELEMENT_FONTSIZE;
                   
                    return temp;
                }
            }
            return null;
        }

        public void ChagneOriginThick(string position, double changedThick)
        {
            Thickness thickness = new Thickness();

            foreach (UIElement child in currentCanvas.Children)
            {
                string temp2 = child.ToString();
                string temp3 = "";
                if (temp2.Contains("RadioButton"))
                {
                    temp3 = (child as RadioButton).Name;
                }
                else if (temp2.Contains("TextBox"))
                {
                    temp3 = (child as TextBox).Name;
                }
                else if (temp2.Contains("PasswordBox"))
                {
                    temp3 = (child as PasswordBox).Name;
                }
                else if (temp2.Contains("Button"))
                {
                    temp3 = (child as Button).Name;
                }
                else if (temp2.Contains("ListBox"))
                {
                    temp3 = (child as ListBox).Name;
                }
                else if (temp2.Contains("Menu"))
                {
                    temp3 = (child as Menu).Name;
                }
                else if (temp2.Contains("ComboBox"))
                {
                    temp3 = (child as ComboBox).Name;
                }
                else if (temp2.Contains("Slider"))
                {
                    temp3 = (child as Slider).Name;
                }
                if (SelectedTypeId.Equals(temp3))
                {
                    if (temp2.Contains("RadioButton"))
                    {
                        thickness.Left = (child as RadioButton).BorderThickness.Left;
                        thickness.Right = (child as RadioButton).BorderThickness.Right;
                        thickness.Top = (child as RadioButton).BorderThickness.Top;
                        thickness.Bottom = (child as RadioButton).BorderThickness.Bottom;
                    }
                   
                    else if (temp2.Contains("TextBox"))
                    {
                        thickness.Left = (child as TextBox).BorderThickness.Left;
                        thickness.Right = (child as TextBox).BorderThickness.Right;
                        thickness.Top = (child as TextBox).BorderThickness.Top;
                        thickness.Bottom = (child as TextBox).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("PasswordBox"))
                    {
                        thickness.Left = (child as PasswordBox).BorderThickness.Left;
                        thickness.Right = (child as PasswordBox).BorderThickness.Right;
                        thickness.Top = (child as PasswordBox).BorderThickness.Top;
                        thickness.Bottom = (child as PasswordBox).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("ListBox"))
                    {
                        thickness.Left = (child as ListBox).BorderThickness.Left;
                        thickness.Right = (child as ListBox).BorderThickness.Right;
                        thickness.Top = (child as ListBox).BorderThickness.Top;
                        thickness.Bottom = (child as ListBox).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("ComboBox"))
                    {
                        thickness.Left = (child as ComboBox).BorderThickness.Left;
                        thickness.Right = (child as ComboBox).BorderThickness.Right;
                        thickness.Top = (child as ComboBox).BorderThickness.Top;
                        thickness.Bottom = (child as ComboBox).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("Menu"))
                    {
                        thickness.Left = (child as Menu).BorderThickness.Left;
                        thickness.Right = (child as Menu).BorderThickness.Right;
                        thickness.Top = (child as Menu).BorderThickness.Top;
                        thickness.Bottom = (child as Menu).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("Button"))
                    {
                        thickness.Left = (child as Button).BorderThickness.Left;
                        thickness.Right = (child as Button).BorderThickness.Right;
                        thickness.Top = (child as Button).BorderThickness.Top;
                        thickness.Bottom = (child as Button).BorderThickness.Bottom;
                    }
                    else if (temp2.Contains("Slider"))
                    {
                        thickness.Left = (child as Slider).BorderThickness.Left;
                        thickness.Right = (child as Slider).BorderThickness.Right;
                        thickness.Top = (child as Slider).BorderThickness.Top;
                        thickness.Bottom = (child as Slider).BorderThickness.Bottom;
                    }
                }
            }


            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {

                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    if (position.Equals("Left"))
                    {
                        thickness.Left = changedThick;
                        temp.UIELEMENT_LTHICKNESS = changedThick;
                    }
                    else if (position.Equals("Right"))
                    {
                        thickness.Right = changedThick;
                        temp.UIELEMENT_RTHICKNESS = changedThick;
                    }
                    else if (position.Equals("Top"))
                    {
                        thickness.Top = changedThick;
                        temp.UIELEMENT_UTHICKNESS = changedThick;
                    }
                    else if (position.Equals("Bottom"))
                    {
                        thickness.Bottom = changedThick;
                        temp.UIELEMENT_DTHICKNESS = changedThick;
                    }

                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).BorderThickness = thickness;
                            }                            
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).BorderThickness = thickness;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).BorderThickness = thickness;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginHeight(double changedHeight)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_HEIGHT = changedHeight;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Height = changedHeight;
                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Height = changedHeight;
                            }
                            else if (temp2.Contains("Image"))
                            {
                                (child as Image).Height = changedHeight;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Height = changedHeight;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Height = changedHeight;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Height = changedHeight;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Height = changedHeight;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Height = changedHeight;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Height = changedHeight;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginWidth(double changedWidth)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_WIDTH = changedWidth;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Width = changedWidth;
                            }                            
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Width = changedWidth;
                            }
                            else if (temp2.Contains("Image"))
                            {
                                (child as Image).Width = changedWidth;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Width = changedWidth;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Width = changedWidth;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Width = changedWidth;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Width = changedWidth;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Width = changedWidth;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Width = changedWidth;
                            }
                        }
                    }

                }
            }
        }

        public void ChagneOriginFontSize(int changedFontSize)
        {
            if (_ucaVM != null)
            {
                foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
                {
                    if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                    {
                        temp.UIELEMENT_FONTSIZE = changedFontSize;
                        foreach (UIElement child in currentCanvas.Children)
                        {
                            string temp1 = temp.UIELEMENT_ID;
                            string temp2 = child.ToString();
                            string temp3 = "";
                            if (temp2.Contains("RadioButton"))
                            {
                                temp3 = (child as RadioButton).Name;
                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                temp3 = (child as TextBox).Name;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                temp3 = (child as PasswordBox).Name;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                temp3 = (child as ListBox).Name;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                temp3 = (child as ComboBox).Name;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                temp3 = (child as Menu).Name;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                temp3 = (child as Button).Name;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                temp3 = (child as Slider).Name;
                            }
                            if (SelectedTypeId.Equals(temp3))
                            {
                                if (temp2.Contains("RadioButton"))
                                {
                                    (child as RadioButton).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("TextBox"))
                                {
                                    (child as TextBox).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("PasswordBox"))
                                {
                                    (child as PasswordBox).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("ListBox"))
                                {
                                    (child as ListBox).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("ComboBox"))
                                {
                                    (child as ComboBox).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("Menu"))
                                {
                                    (child as Menu).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("Button"))
                                {
                                    (child as Button).FontSize = changedFontSize;
                                }
                                else if (temp2.Contains("Slider"))
                                {
                                    (child as Slider).FontSize = changedFontSize;
                                }
                            }
                        }

                    }
                }
            }
        }

        public void ChagneOriginBorderOpacity(double changedOpacity)
        {
            Byte ScR, ScG, ScB;
            Color color = new Color();

            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    ScR = temp.UIELEMENT_BORDERGROUND.R;
                    ScG = temp.UIELEMENT_BORDERGROUND.G;
                    ScB = temp.UIELEMENT_BORDERGROUND.B;
                    color.A = (byte)(changedOpacity);
                    color.R = ScR;
                    color.G = ScG;
                    color.B = ScB;

                    temp.UIELEMENT_BORDER_OPACITY = changedOpacity;
                    temp.UIELEMENT_BORDERGROUND = color;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).BorderBrush = new SolidColorBrush(color);

                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).BorderBrush = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).BorderBrush = new SolidColorBrush(color);
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginOpacity(double changedOpacity)
        {
            Byte ScR, ScG, ScB;
            Color color = new Color();
            
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    ScR = temp.UIELEMENT_BACKGROUND.R;
                    ScG = temp.UIELEMENT_BACKGROUND.G;
                    ScB = temp.UIELEMENT_BACKGROUND.B;
                    color.A = (byte)(changedOpacity);
                    color.R = ScR;
                    color.G = ScG;
                    color.B = ScB;
                    
                    temp.UIELEMENT_OPACITY = changedOpacity;
                    temp.UIELEMENT_BACKGROUND = color;
                    foreach (UIElement child in currentCanvas.Children)
                    {                     
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Background = new SolidColorBrush(color);

                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Background = new SolidColorBrush(color);
                            }                           
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Background = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Background = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Background = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Background = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Button"))
                            {        
                                (child as Button).Background = new SolidColorBrush(color);
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Background = new SolidColorBrush(color);
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginText(string changedText)
        {

            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_TEXT = changedText;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                       
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Content = changedText;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Content = changedText;
                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Text = changedText;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Text = changedText;
                            }                         
                        }
                    }
                }
            }
        }

        //Zindex        
        public void ChagneOriginZIndex(int changedZIndex)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_ZINDEX = changedZIndex;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString();
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                Canvas.SetZIndex(child as RadioButton, changedZIndex);
                            }                            
                            else if (temp2.Contains("TextBox"))
                            {
                                Canvas.SetZIndex(child as TextBox, changedZIndex);
                            }
                            else if (temp2.Contains("Image"))
                            {
                                Canvas.SetZIndex(child as Image, changedZIndex);
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                Canvas.SetZIndex(child as PasswordBox, changedZIndex);
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                Canvas.SetZIndex(child as ListBox, changedZIndex);
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                Canvas.SetZIndex(child as Menu, changedZIndex);
                            }
                            else if (temp2.Contains("Button"))
                            {
                                Canvas.SetZIndex(child as Button, changedZIndex);
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                Canvas.SetZIndex(child as ComboBox, changedZIndex);
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                Canvas.SetZIndex(child as Slider, changedZIndex);
                            }
                        }
                    }
                }
            }
        }

        //Angle        
        public void ChagneOriginAngle(double changedAngle)
        {
            RotateTransform transform = new RotateTransform();
            transform.Angle = changedAngle;
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_ANGLE = changedAngle;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).RenderTransform = transform;
                            }                            
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).RenderTransform = transform;
                            }
                            else if (temp2.Contains("Image"))
                            {
                                (child as Image).RenderTransform = transform;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).RenderTransform = transform;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).RenderTransform = transform;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).RenderTransform = transform;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).RenderTransform = transform;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).RenderTransform = transform;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).RenderTransform = transform;
                            }
                        }
                    }
                }
            }
        }
        //XPosition

        public void ChagneOriginXPosition(double changedXPosition)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_XPOSITION = changedXPosition;
                }
            }
        }

        //YPosition
        public void ChagneOriginYPosition(double changedYPosition)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_YPOSITION = changedYPosition;
                }
            }
        }

        //Background
        public void ChagneOriginBackground(Color changedBackground)
        {
            SolidColorBrush brush = new SolidColorBrush(changedBackground);
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_BACKGROUND = changedBackground;
                    if (SelectedTypeId == "MyCanvas")
                    {
                        currentCanvas.Background = brush;
                    }
                    else
                    {
                        foreach (UIElement child in currentCanvas.Children)
                        {
                            string temp1 = temp.UIELEMENT_ID;
                            string temp2 = child.ToString(); string temp3 = "";
                            if (temp2.Contains("RadioButton"))
                            {
                                temp3 = (child as RadioButton).Name;
                            }
                            else if (temp2.Contains("TextBox"))
                            {
                                temp3 = (child as TextBox).Name;
                            }
                            else if (temp2.Contains("Image"))
                            {
                                temp3 = (child as Image).Name;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                temp3 = (child as PasswordBox).Name;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                temp3 = (child as ListBox).Name;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                temp3 = (child as ComboBox).Name;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                temp3 = (child as Menu).Name;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                temp3 = (child as Button).Name;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                temp3 = (child as Slider).Name;
                            }
                            if (SelectedTypeId.Equals(temp3))
                            {
                                if (temp2.Contains("RadioButton"))
                                {
                                    (child as RadioButton).Background = brush;
                                }                                
                                else if (temp2.Contains("TextBox"))
                                {
                                    (child as TextBox).Background = brush;
                                }
                                else if (temp2.Contains("PasswordBox"))
                                {
                                    (child as PasswordBox).Background = brush;
                                }
                                else if (temp2.Contains("ListBox"))
                                {
                                    (child as ListBox).Background = brush;
                                }
                                else if (temp2.Contains("ComboBox"))
                                {
                                    (child as ComboBox).Background = brush;
                                }
                                else if (temp2.Contains("Menu"))
                                {
                                    (child as Menu).Background = brush;
                                }
                                else if (temp2.Contains("Button"))
                                {
                                    (child as Button).Background = brush;
                                }
                                else if (temp2.Contains("Slider"))
                                {
                                    (child as Slider).Background = brush;
                                }
                               
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginBorderground(Color changedBorderground)
        {
            SolidColorBrush brush = new SolidColorBrush(changedBorderground);
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_BORDERGROUND = changedBorderground;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).BorderBrush = brush;
                            }                         
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).BorderBrush = brush;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).BorderBrush = brush;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).BorderBrush = brush;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).BorderBrush = brush;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).BorderBrush = brush;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).BorderBrush = brush;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).BorderBrush = brush;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginForeground(Color changedForeground)
        {
            SolidColorBrush brush = new SolidColorBrush(changedForeground);
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_FOREGROUND = changedForeground;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Foreground = brush;
                            }                           
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Foreground = brush;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Foreground = brush;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Foreground = brush;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Foreground = brush;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Foreground = brush;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Foreground = brush;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Foreground = brush;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginHorizon(string changedHorizon)
        {
            double horizonPosition = 0;
            HorizontalAlignment horizon = HorizontalAlignment.Left;
            if (changedHorizon == "Left")
            {
                horizon = HorizontalAlignment.Left;
                horizonPosition = 0;

            }
            else if (changedHorizon == "Center")
            {
                horizon = HorizontalAlignment.Center;
                horizonPosition = 160;
            }
            else if (changedHorizon == "Right")
            {
                horizon = HorizontalAlignment.Right;
                horizonPosition = 320;
            }
            else
            {
                horizon = HorizontalAlignment.Stretch;
            }

            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_HORIZON = changedHorizon;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as RadioButton, horizonPosition);
                                    (child as RadioButton).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as RadioButton).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as RadioButton, horizonPosition);
                                    (child as RadioButton).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as RadioButton).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as RadioButton, horizonPosition);
                                    (child as RadioButton).HorizontalAlignment = horizon;
                                }
                            }
                           
                            else if (temp2.Contains("TextBox"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as TextBox, horizonPosition);
                                    (child as TextBox).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as TextBox).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as TextBox, horizonPosition);
                                    (child as TextBox).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as TextBox).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as TextBox, horizonPosition);
                                    (child as TextBox).HorizontalAlignment = horizon;
                                }
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as PasswordBox, horizonPosition);
                                    (child as PasswordBox).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as PasswordBox).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as PasswordBox, horizonPosition);
                                    (child as PasswordBox).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as PasswordBox).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as PasswordBox, horizonPosition);
                                    (child as PasswordBox).HorizontalAlignment = horizon;
                                }
                            }

                            else if (temp2.Contains("ListBox"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ListBox, horizonPosition);
                                    (child as ListBox).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as ListBox).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ListBox, horizonPosition);
                                    (child as ListBox).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as ListBox).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ListBox, horizonPosition);
                                    (child as ListBox).HorizontalAlignment = horizon;
                                }
                            }

                            else if (temp2.Contains("ComboBox"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ComboBox, horizonPosition);
                                    (child as ComboBox).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as ComboBox).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ComboBox, horizonPosition);
                                    (child as ComboBox).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as ComboBox).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as ComboBox, horizonPosition);
                                    (child as ComboBox).HorizontalAlignment = horizon;
                                }
                            }

                            else if (temp2.Contains("Menu"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Menu, horizonPosition);
                                    (child as Menu).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as Menu).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Menu, horizonPosition);
                                    (child as Menu).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as Menu).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Menu, horizonPosition);
                                    (child as Menu).HorizontalAlignment = horizon;
                                }
                            }

                            else if (temp2.Contains("Button"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Button, horizonPosition);
                                    (child as Button).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as Button).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Button, horizonPosition);
                                    (child as Button).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as Button).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Button, horizonPosition);
                                    (child as Button).HorizontalAlignment = horizon;
                                }
                            }

                            else if (temp2.Contains("Slider"))
                            {
                                if (changedHorizon == "Left")
                                {
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Slider, horizonPosition);
                                    (child as Slider).HorizontalAlignment = horizon;
                                }
                                else if (changedHorizon == "Center")
                                {
                                    horizonPosition -= ((child as Slider).Width / 2);
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Slider, horizonPosition);
                                    (child as Slider).HorizontalAlignment = horizon;
                                }
                                else
                                {
                                    horizonPosition -= (child as Slider).Width;
                                    ChagneOriginXPosition(horizonPosition);
                                    Canvas.SetLeft(child as Slider, horizonPosition);
                                    (child as Slider).HorizontalAlignment = horizon;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginVertical(string changedVertical)
        {
            double verticalPosition = 0;
            VerticalAlignment vertical = VerticalAlignment.Top;
            if (changedVertical == "Top")
            {
                vertical = VerticalAlignment.Top;
                verticalPosition = 0;
            }
            else if (changedVertical == "Center")
            {
                vertical = VerticalAlignment.Center;
                verticalPosition = 160;
            }
            else if (changedVertical == "Bottom")
            {
                vertical = VerticalAlignment.Bottom;
                verticalPosition = 320;
            }
            else
            {
                vertical = VerticalAlignment.Stretch;
            }
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_VERTICAL = changedVertical;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); 
                        string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as RadioButton, verticalPosition);
                                    (child as RadioButton).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as RadioButton).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as RadioButton, verticalPosition);
                                    (child as RadioButton).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as RadioButton).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as RadioButton, verticalPosition);
                                    (child as RadioButton).VerticalAlignment = vertical;
                                }
                            }
                            
                            else if (temp2.Contains("TextBox"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as TextBox, verticalPosition);
                                    (child as TextBox).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as TextBox).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as TextBox, verticalPosition);
                                    (child as TextBox).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as TextBox).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as TextBox, verticalPosition);
                                    (child as TextBox).VerticalAlignment = vertical;
                                }
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as PasswordBox, verticalPosition);
                                    (child as PasswordBox).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as PasswordBox).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as PasswordBox, verticalPosition);
                                    (child as PasswordBox).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as PasswordBox).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as PasswordBox, verticalPosition);
                                    (child as PasswordBox).VerticalAlignment = vertical;
                                }
                            }

                            else if (temp2.Contains("ListBox"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ListBox, verticalPosition);
                                    (child as ListBox).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as ListBox).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ListBox, verticalPosition);
                                    (child as ListBox).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as ListBox).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ListBox, verticalPosition);
                                    (child as ListBox).VerticalAlignment = vertical;
                                }
                            }

                            else if (temp2.Contains("ComboBox"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ComboBox, verticalPosition);
                                    (child as ComboBox).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as ComboBox).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ComboBox, verticalPosition);
                                    (child as ComboBox).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as ComboBox).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as ComboBox, verticalPosition);
                                    (child as ComboBox).VerticalAlignment = vertical;
                                }
                            }

                            else if (temp2.Contains("Menu"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Menu, verticalPosition);
                                    (child as Menu).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as Menu).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Menu, verticalPosition);
                                    (child as Menu).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as Menu).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Menu, verticalPosition);
                                    (child as Menu).VerticalAlignment = vertical;
                                }
                            }

                            else if (temp2.Contains("Button"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Button, verticalPosition);
                                    (child as Button).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as Button).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Button, verticalPosition);
                                    (child as Button).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as Button).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Button, verticalPosition);
                                    (child as Button).VerticalAlignment = vertical;
                                }
                            }

                            else if (temp2.Contains("Slider"))
                            {
                                if (changedVertical == "Top")
                                {
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Slider, verticalPosition);
                                    (child as Slider).VerticalAlignment = vertical;
                                }
                                else if (changedVertical == "Center")
                                {
                                    verticalPosition -= ((child as Slider).Height / 2);
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Slider, verticalPosition);
                                    (child as Slider).VerticalAlignment = vertical;
                                }
                                else
                                {
                                    verticalPosition -= (child as Slider).Height;
                                    ChagneOriginYPosition(verticalPosition);
                                    Canvas.SetTop(child as Slider, verticalPosition);
                                    (child as Slider).VerticalAlignment = vertical;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginCursor(string changedCursor)
        {
            Cursor cursor = Cursors.None;
            TempCursor = changedCursor;
            if (changedCursor.Equals("None"))
            {
                cursor = Cursors.None;
            }
            else if (changedCursor.Equals("No"))
            {
                cursor = Cursors.No;
            }
            else if (changedCursor.Equals("Arrow"))
            {
                cursor = Cursors.Arrow;
            }
            else if (changedCursor.Equals("Cross"))
            {
                cursor = Cursors.Cross;
            }
            else if (changedCursor.Equals("Help"))
            {
                cursor = Cursors.Help;
            }
            else if (changedCursor.Equals("IBeam"))
            {
                cursor = Cursors.IBeam;
            }
            else if (changedCursor.Equals("SizeAll"))
            {
                cursor = Cursors.SizeAll;
            }
            else if (changedCursor.Equals("SizeNESW"))
            {
                cursor = Cursors.SizeNESW;
            }
            else if (changedCursor.Equals("SizeNWSE"))
            {
                cursor = Cursors.SizeNWSE;
            }
            else if (changedCursor.Equals("SizeNS"))
            {
                cursor = Cursors.SizeNS;
            }
            else if (changedCursor.Equals("SizeWE"))
            {
                cursor = Cursors.SizeWE;
            }
            else if (changedCursor.Equals("Pen"))
            {
                cursor = Cursors.Pen;
            }
            else if (changedCursor.Equals("Wait"))
            {
                cursor = Cursors.Wait;
            }
            else if (changedCursor.Equals("ScrollAll"))
            {
                cursor = Cursors.ScrollAll;
            }
            else if (changedCursor.Equals("ScrollE"))
            {
                cursor = Cursors.ScrollE;
            }
            else if (changedCursor.Equals("ScrollN"))
            {
                cursor = Cursors.ScrollN;
            }
            else if (changedCursor.Equals("ScrollNE"))
            {
                cursor = Cursors.ScrollNE;
            }
            else if (changedCursor.Equals("ScrollNS"))
            {
                cursor = Cursors.ScrollNS;
            }
            else if (changedCursor.Equals("ScrollNW"))
            {
                cursor = Cursors.ScrollNW;
            }
            else if (changedCursor.Equals("ScrollS"))
            {
                cursor = Cursors.ScrollS;
            }
            else if (changedCursor.Equals("ScrollSE"))
            {
                cursor = Cursors.ScrollSE;
            }
            else if (changedCursor.Equals("ScrollSW"))
            {
                cursor = Cursors.ScrollSW;
            }
            else if (changedCursor.Equals("ScrollW"))
            {
                cursor = Cursors.ScrollW;
            }
            else if (changedCursor.Equals("ScrollWE"))
            {
                cursor = Cursors.ScrollWE;
            }
            else if (changedCursor.Equals("UpArrow"))
            {
                cursor = Cursors.UpArrow;
            }
            else if (changedCursor.Equals("Hand"))
            {
                cursor = Cursors.Hand;
            }


            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_CURSOR = changedCursor;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Cursor = cursor;
                            }
                           
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Cursor = cursor;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Cursor = cursor;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Cursor = cursor;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Cursor = cursor;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Cursor = cursor;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Cursor = cursor;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Cursor = cursor;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginVisible(string changedVisible)
        {
            Visibility visibility = Visibility.Visible;
            TempVisivility = changedVisible;
            if (changedVisible.Equals("Visible"))
            {
                visibility = Visibility.Visible;
            }
            else if (changedVisible.Equals("Hidden"))
            {
                visibility = Visibility.Hidden;
            }
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_VISIBLE = changedVisible;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).Visibility = visibility;
                            }
                           
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).Visibility = visibility;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).Visibility = visibility;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).Visibility = visibility;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).Visibility = visibility;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).Visibility = visibility;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).Visibility = visibility;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).Visibility = visibility;
                            }
                        }
                    }
                }
            }
        }

        public void ChagneOriginToolTip(string changedToolTip)
        {
            foreach (UIElementInfo temp in _ucaVM.UI_Element_List)
            {
                if (temp.UIELEMENT_ID.Equals(SelectedTypeId))
                {
                    temp.UIELEMENT_TOOLTIP = changedToolTip;
                    foreach (UIElement child in currentCanvas.Children)
                    {
                        string temp1 = temp.UIELEMENT_ID;
                        string temp2 = child.ToString(); string temp3 = "";
                        if (temp2.Contains("RadioButton"))
                        {
                            temp3 = (child as RadioButton).Name;
                        }
                        else if (temp2.Contains("TextBox"))
                        {
                            temp3 = (child as TextBox).Name;
                        }
                        else if (temp2.Contains("Image"))
                        {
                            temp3 = (child as Image).Name;
                        }
                        else if (temp2.Contains("PasswordBox"))
                        {
                            temp3 = (child as PasswordBox).Name;
                        }
                        else if (temp2.Contains("ListBox"))
                        {
                            temp3 = (child as ListBox).Name;
                        }
                        else if (temp2.Contains("ComboBox"))
                        {
                            temp3 = (child as ComboBox).Name;
                        }
                        else if (temp2.Contains("Menu"))
                        {
                            temp3 = (child as Menu).Name;
                        }
                        else if (temp2.Contains("Button"))
                        {
                            temp3 = (child as Button).Name;
                        }
                        else if (temp2.Contains("Slider"))
                        {
                            temp3 = (child as Slider).Name;
                        }
                        if (SelectedTypeId.Equals(temp3))
                        {
                            if (temp2.Contains("RadioButton"))
                            {
                                (child as RadioButton).ToolTip = changedToolTip;
                            }
                           
                            else if (temp2.Contains("TextBox"))
                            {
                                (child as TextBox).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("PasswordBox"))
                            {
                                (child as PasswordBox).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("ListBox"))
                            {
                                (child as ListBox).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("ComboBox"))
                            {
                                (child as ComboBox).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("Menu"))
                            {
                                (child as Menu).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("Button"))
                            {
                                (child as Button).ToolTip = changedToolTip;
                            }
                            else if (temp2.Contains("Slider"))
                            {
                                (child as Slider).ToolTip = changedToolTip;
                            }
                        }
                    }
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
