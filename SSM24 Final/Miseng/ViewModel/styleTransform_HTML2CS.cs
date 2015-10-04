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
using System.Xaml;
using System.IO;
using Miseng.ViewModel.UICanvas;

namespace Miseng.ViewModel
{
    class styleTransform_HTML2CS
    {
        public static UIElementInfo DefaultElementAttribute(string[] styleArr, string id, string tooltip, string type, string text, string html_path)
        {
            UIElementInfo element = new UIElementInfo();
            double width = 0.0, height = 0.0, angle = 0.0;
            element.UIELEMENT_TYPE = type;
            element.UIELEMENT_ID = id;
            element.UIELEMENT_TOOLTIP = tooltip;
            element.UIELEMENT_TEXT = text;
            int n = styleArr.Count();

            for (int i = 0; i < n; i += 2)
            {
                switch (styleArr[i])
                {
                    case "background": element.UIELEMENT_IMAGEURL = BG_URL(styleArr[i + 1], html_path);
                        break;
                    case "opacity": element.UIELEMENT_OPACITY = Double.Parse(styleArr[i + 1]) * 255;
                        break;
                    case "font-size": element.UIELEMENT_FONTSIZE = Int32.Parse(styleArr[i + 1].Replace(" ", "").Replace("px;", ""));
                        break;
                    case "cursor": element.UIELEMENT_CURSOR = styleArr[i + 1];
                        break;
                    case "border-top-width": element.UIELEMENT_UTHICKNESS = Double.Parse(styleArr[i + 1]);
                        break;
                    case "border-bottom-width": element.UIELEMENT_DTHICKNESS = Double.Parse(styleArr[i + 1]);
                        break;
                    case "border-left-width": element.UIELEMENT_LTHICKNESS = Double.Parse(styleArr[i + 1]);
                        break;
                    case "border-right-width": element.UIELEMENT_RTHICKNESS = Double.Parse(styleArr[i + 1]);
                        break;
                    case "border-color": element.UIELEMENT_BORDERGROUND = Html2CS_Color(styleArr[i + 1]);
                        break;
                    case "visibility": element.UIELEMENT_VISIBLE = styleArr[i + 1];
                        break;
                    case "horizontal-align": element.UIELEMENT_HORIZON = styleArr[i + 1];
                        break;
                    case "vertical-align": element.UIELEMENT_VERTICAL = getVerticalAlign(styleArr[i + 1]);
                        break;
                    case "left": element.UIELEMENT_XPOSITION = getXposition(styleArr[i + 1], width, height, angle);
                        break;
                    case "top": element.UIELEMENT_YPOSITION = getYposition(styleArr[i + 1], width, height, angle);
                        break;
                    case "transform": element.UIELEMENT_ANGLE = Double.Parse(styleArr[i + 1].Replace("deg)", "").Replace("rotate(", "")); angle = Double.Parse(styleArr[i + 1].Replace("deg)", "").Replace("rotate(", ""));
                        break;
                    case "background-color": element.UIELEMENT_BACKGROUND = Html2CS_Color(styleArr[i + 1]);
                        break;
                    case "color": element.UIELEMENT_FOREGROUND = Html2CS_Color(styleArr[i + 1]);
                        break;
                    case "height": element.UIELEMENT_HEIGHT = Double.Parse(styleArr[i + 1]); height = Double.Parse(styleArr[i + 1]);
                        break;
                    case "width": element.UIELEMENT_WIDTH = Double.Parse(styleArr[i + 1]); width = Double.Parse(styleArr[i + 1]);
                        break;
                    case "z-index": element.UIELEMENT_ZINDEX = Int32.Parse(styleArr[i + 1]);
                        break;
                    default: break;
                }
            }
            return element;
        }


        private static string BG_URL(string bg_url, string html_path)
        {
            string result = "";

            string image_name = bg_url.Replace("url('", "").Replace("')", "");
            result += html_path.Replace(html_path.Split(new char[] { '\\' }).Last(), "");
            result += image_name;
            return result;
        }
        private static Color Html2CS_Color(string htmlColor)
        {
            Color color = new Color();
            string[] rgb = htmlColor.Replace("rgb(", "").Replace("rgba(", "").Replace(")", "").Replace("!important", "").Split(',');
            color.R = Byte.Parse(rgb[0]);
            color.G = Byte.Parse(rgb[1]);
            color.B = Byte.Parse(rgb[2]);
            color.A = Byte.Parse(rgb[3]);
            return color;
        }

        private static double getYposition(string top, double width, double height, double angle)
        {
            double htmlTop = Double.Parse(top);
            double csTop;
            Point after_rotate_LeftTop = getLeftTop(width, height, angle);
            Point OriginLeftTop = getOriginLeftTop(width, height);
            double Ygap = after_rotate_LeftTop.Y - OriginLeftTop.Y;

            if (Ygap < 0)
                csTop = htmlTop + Math.Abs(Ygap);
            else
                csTop = htmlTop - Math.Abs(Ygap);

            return csTop;
        }
        private static double getXposition(string left, double width, double height, double angle)
        {
            double htmlLeft = Double.Parse(left);
            double csLeft;
            Point after_rotate_LeftTop = getLeftTop(width, height, angle);
            Point OriginLeftTop = getOriginLeftTop(width, height);
            double Xgap = after_rotate_LeftTop.X - OriginLeftTop.X;
            if (Xgap < 0)
                csLeft = htmlLeft - Math.Abs(Xgap);
            else
                csLeft = htmlLeft + Math.Abs(Xgap);
            return csLeft;
        }
        private static Point getLeftTop(double width, double height, double angle) // leftTop을 회전했을 때 left탑의 위치
        {
            Point leftTop = new Point();
            double _angle = angle;
            if (_angle < 0)
            {
                _angle = angle % 360;
                _angle += 360;
            }
            double originLeftX = -1 * width / 2;
            double originLeftY = height / 2;
            double rad = -1 * (_angle / 180) * Math.PI;

            leftTop.X = originLeftX * Math.Cos(rad) - originLeftY * Math.Sin(rad);
            leftTop.Y = originLeftX * Math.Sin(rad) + originLeftY * Math.Cos(rad);
            return leftTop;
        }
        private static Point getOriginLeftTop(double width, double height)
        {
            Point leftTop = new Point();
            double originLeftX = -1 * (width / 2);
            double originLeftY = 1 * (height / 2);


            leftTop.X = originLeftX;
            leftTop.Y = originLeftY;
            return leftTop;
        }
        private static string getVerticalAlign(string verticalAttr)
        {
            string csVerticalAttr = "";
            switch (verticalAttr)
            {
                case "top": csVerticalAttr = "top";
                    break;
                case "middle": csVerticalAttr = "center";
                    break;
                case "bottom": csVerticalAttr = "bottom";
                    break;

                default: break;
            }
            return csVerticalAttr;
        }
    }
}