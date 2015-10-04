using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using Miseng.Common;

namespace Miseng.ViewModel.UICanvas
{
    public class UIElementInfo : ViewModelBase
    {
        private string _type;
        private string _id;
        private string _text;
        private string _vertical;
        private string _horizon;
        private string _visible;
        private string _cursor;
        private string _toolTip;
        private string _imageUrl;
        private Color _backGround;
        private Color _foreGround;
        private Color _borderGround;
        private double _LThickness;
        private double _RThickness;
        private double _UThickness;
        private double _DThickness;
        private double _width;
        private double _height;
        private double _xPosition;
        private double _yPosition;
        private double _angle;
        private double _opacity;
        private double _borderOpacity;
        private int _zIndex;
        private int _fontSize;
        private List<UIElementInfo> _list;

        public UIElementInfo()
        {
            UIELEMENT_TYPE = "";
            UIELEMENT_ID = "";
            UIELEMENT_TEXT = "";
            UIELEMENT_VERTICAL = "";
            UIELEMENT_HORIZON = "";
            UIELEMENT_VISIBLE = "Visible";
            UIELEMENT_CURSOR = "Arrow";
            UIELEMENT_TOOLTIP = "";
            UIELEMENT_IMAGEURL = "";
            UIELEMENT_BACKGROUND = (Color)ColorConverter.ConvertFromString("#3F3F46");
            UIELEMENT_FOREGROUND = (Color)ColorConverter.ConvertFromString("#FFFFFF");
            UIELEMENT_BORDERGROUND = (Color)ColorConverter.ConvertFromString("#0054545C");
            UIELEMENT_LTHICKNESS = 1;
            UIELEMENT_RTHICKNESS = 1;
            UIELEMENT_UTHICKNESS = 1;
            UIELEMENT_DTHICKNESS = 1;
            UIELEMENT_WIDTH = 60;
            UIELEMENT_HEIGHT = 40;
            UIELEMENT_XPOSITION = 0;
            UIELEMENT_YPOSITION = 0;
            UIELEMENT_ANGLE = 0;
            UIELEMENT_ZINDEX = 0;
            UIELEMENT_OPACITY = 255;
            UIELEMENT_BORDER_OPACITY = 255;
            UIELEMENT_FONTSIZE = 12;
        }

        public List<UIElementInfo> UIELEMENT_LIST
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged("UIELEMENT_LIST");
            }
        }

        public int UIELEMENT_FONTSIZE
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                OnPropertyChanged("UIELEMENT_FONTSIZE");
            }
        }
        
        public double UIELEMENT_OPACITY
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnPropertyChanged("UIELEMENT_OPACITY");
            }
        }

        public double UIELEMENT_BORDER_OPACITY
        {
            get { return _borderOpacity; }
            set
            {
                _borderOpacity = value;
                OnPropertyChanged("UIELEMENT_BORDER_OPACITY");
            }
        }

        public string UIELEMENT_TYPE
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("UIELEMENT_TYPE");
            }
        }
        public string UIELEMENT_ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("UIELEMENT_ID");
            }
        }

        public string UIELEMENT_TOOLTIP
        {
            get
            {
                return _toolTip;
            }
            set
            {
                _toolTip = value;
                OnPropertyChanged("UIELEMENT_TOOLTIP");
            }
        }

        public string UIELEMENT_TEXT
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("UIELEMENT_TEXT");
            }
        }

        public string UIELEMENT_IMAGEURL
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("UIELEMENT_IMAGEURL");
            }
        }

        public double UIELEMENT_LTHICKNESS
        {
            get
            {
                return _LThickness;
            }
            set
            {
                _LThickness = value;
                OnPropertyChanged("UIELEMENT_LTHICKNESS");
            }
        }

        public double UIELEMENT_RTHICKNESS
        {
            get
            {
                return _RThickness;
            }
            set
            {
                _RThickness = value;
                OnPropertyChanged("UIELEMENT_RTHICKNESS");
            }
        }

        public double UIELEMENT_UTHICKNESS
        {
            get
            {
                return _UThickness;
            }
            set
            {
                _UThickness = value;
                OnPropertyChanged("UIELEMENT_UTHICKNESS");
            }
        }

        public double UIELEMENT_DTHICKNESS
        {
            get
            {
                return _DThickness;
            }
            set
            {
                _DThickness = value;
                OnPropertyChanged("UIELEMENT_DTHICKNESS");
            }
        }

        public Color UIELEMENT_FOREGROUND
        {
            get { return _foreGround; }
            set
            {
                _foreGround = value;
                OnPropertyChanged("UIELEMENT_FOREGROUND");
            }
        }

        public Color UIELEMENT_BACKGROUND
        {
            get { return _backGround; }
            set
            {
                _backGround = value;
                OnPropertyChanged("UIELEMENT_BACKGROUND");
            }
        }

        public Color UIELEMENT_BORDERGROUND
        {
            get { return _borderGround; }
            set
            {
                _borderGround = value;
                OnPropertyChanged("UIELEMENT_BORDERGROUND");
            }
        }

        public double UIELEMENT_WIDTH
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("UIELEMENT_WIDTH");
            }
        }


        public double UIELEMENT_HEIGHT
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("UIELEMENT_HEIGHT");
            }
        }

        public double UIELEMENT_XPOSITION
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                OnPropertyChanged("UIELEMENT__XPOSITION");
            }
        }

        public double UIELEMENT_YPOSITION
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                OnPropertyChanged("UIELEMENT_YPOSITION");
            }
        }

        public int UIELEMENT_ZINDEX
        {
            get { return _zIndex; }
            set
            {
                _zIndex = value;
                OnPropertyChanged("UIELEMENT_ZINDEX");
            }
        }

        public double UIELEMENT_ANGLE
        {
            get { return _angle; }
            set
            {
                _angle = value;
                OnPropertyChanged("UIELEMENT_ANGLE");
            }
        }

        public string UIELEMENT_VERTICAL
        {
            get { return _vertical; }
            set
            {
                _vertical = value;
                OnPropertyChanged("UIELEMENT_VERTICAL");
            }
        }

        public string UIELEMENT_VISIBLE
        {
            get { return _visible; }
            set
            {
                _visible = value;
                OnPropertyChanged("UIELEMENT_VISIBLE");
            }
        }

        public string UIELEMENT_CURSOR
        {
            get { return _cursor; }
            set
            {
                _cursor = value;
                OnPropertyChanged("UIELEMENT_CURSOR");
            }
        }

        public string UIELEMENT_HORIZON
        {
            get { return _horizon; }
            set
            {
                _horizon = value;
                OnPropertyChanged("UIELEMENT_HORIZON");
            }
        }

    }
}

