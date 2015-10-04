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
    static class styleTransform_CS2HTML
    {
        public static string DefaultElementAttribute(UIElementInfo element)
        {
            string style = "";

            style += zeroToMarginPadding();
            style += positionFixed();
            style += getCursor(element.UIELEMENT_CURSOR);
            style += getBackground_img(element.UIELEMENT_IMAGEURL, element.UIELEMENT_HEIGHT, element.UIELEMENT_WIDTH, element.UIELEMENT_OPACITY);
            style += getBorderThickness(element);
            style += getVisibility(element.UIELEMENT_VISIBLE);
            style += getVerticalAlignment(element.UIELEMENT_VERTICAL);
            style += getHorizonAlignment(element.UIELEMENT_HORIZON);
            style += getStyleBackGroundColor(element.UIELEMENT_BACKGROUND);
            style += getStyleForeGround(element.UIELEMENT_FOREGROUND);
            style += getStyleHeight(element.UIELEMENT_HEIGHT);
            style += getStyleWidth(element.UIELEMENT_WIDTH);
            style += getStyleAngle(element.UIELEMENT_ANGLE);
            style += getStylePosition(element.UIELEMENT_XPOSITION, element.UIELEMENT_YPOSITION, element.UIELEMENT_WIDTH, element.UIELEMENT_HEIGHT, element.UIELEMENT_ANGLE);
            style += getStyleZIndex(element.UIELEMENT_ZINDEX);
            style += getStyleFontSize(element.UIELEMENT_FONTSIZE);

            return style;
        }

        private static string getStyleFontSize(int fontSize)
        {
            string result = "";
            result += "font-size : " + fontSize + "px;";
            return result;
        }

        private static string getBackground_img(string image_url, double height, double width, double opacity)
        {
            string result = "";

            string fileName = image_url.Split(new char[] { '\\', '/' }).Last();

            if (!String.IsNullOrEmpty(fileName))
            {
                result = "background : url(";
                result += "'images/" + fileName + "');";
                result += "background-repeat : no-repeat;";
                result += "background-size : " + width + "px " + height + "px ;";
                result += "background-color: rgba(0,0,0,0) !important;";
                result += "border-color : rgba(0,0,0,0);";
            }


            // result += "background-size : 100% 100% ;";
            return result;
        }
        private static string getBorderThickness(UIElementInfo element)
        {
            string style = "";
            style += "border-top-width : " + element.UIELEMENT_UTHICKNESS + "px; ";
            style += "border-bottom-width : " + element.UIELEMENT_DTHICKNESS + "px; ";
            style += "border-left-width : " + element.UIELEMENT_LTHICKNESS + "px; ";
            style += "border-right-width : " + element.UIELEMENT_RTHICKNESS + "px; ";
            return style;
        }
        private static string getBorderGround(Color borderColor)
        {
            string style = "";
            style += "border-color :rgba(" + borderColor.R + ", " + borderColor.G + ", " + borderColor.B + ", " + borderColor.A + "); ";
            return style;
        }
        private static string getCursor(string cursor)
        {
            return "cursor : " + cursor + "; ";
        }
        private static string getVisibility(string visible)
        {
            string style = "";
            switch (visible)
            {
                case "visible": style += "visibility : " + visible + "; ";
                    break;
                case "hidden": style += "visibility : " + visible + "; ";
                    break;
                default: break;
            }
            return style;
        }
        private static string getHorizonAlignment(string horizontalType)
        {
            string style = "";
            switch (horizontalType)
            {
                case "left": style += "horizontal-align : left; ";
                    break;
                case "center": style += "horizontal-align : center; ";
                    break;
                case "right": style += "horizontal-align : right; ";
                    break;
                default: break;
            }
            return style;
        }
        private static string getVerticalAlignment(string verticalType)
        {
            string style = "";
            switch (verticalType)
            {
                case "top": style += "vertical-align : top; ";
                    break;
                case "center": style += "vertical-align : middle; ";
                    break;
                case "bottom": style += "vertical-align : bottom; ";
                    break;
                default: break;
            }
            return style;
        }
        private static Point getLeftTop(double width, double height, double angle) // leftTop을 회전했을 때 left탑의 위치
        {
            Point leftTop = new Point();
            double originLeftX = -1 * width / 2;
            double originLeftY = height / 2;
            double rad = -1 * (angle / 180) * Math.PI;

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
        private static Point getNearLeft(double xPosition, double yPosition, double width, double height, double angle)
        {
            Point LeftTop = new Point();
            Point LeftBtm = new Point();
            Point RightTop = new Point();
            Point RightBtm = new Point();
            Point Result;
            double originLeftX = xPosition;
            double originLeftY = yPosition;
            double rad = (angle / 180) * Math.PI;


            LeftTop.X = (-1 * width / 2) * Math.Cos(rad) - (height / 2) * Math.Sin(rad);
            LeftTop.Y = (-1 * width / 2) * Math.Sin(rad) + (height / 2) * Math.Cos(rad);
            Result = LeftTop;

            LeftBtm.X = (-1 * width / 2) * Math.Cos(rad) - (-1 * height / 2) * Math.Sin(rad);
            LeftBtm.Y = (-1 * width / 2) * Math.Sin(rad) + (-1 * height / 2) * Math.Cos(rad);
            if (Result.X > LeftBtm.X) Result = LeftBtm;

            RightTop.X = (width / 2) * Math.Cos(rad) - (height / 2) * Math.Sin(rad);
            RightTop.Y = (width / 2) * Math.Sin(rad) + (height / 2) * Math.Cos(rad);
            if (Result.X > RightTop.X) Result = RightTop;

            RightBtm.X = (width / 2) * Math.Cos(rad) - (-1 * height / 2) * Math.Sin(rad);
            RightBtm.Y = (width / 2) * Math.Sin(rad) + (-1 * height / 2) * Math.Cos(rad);
            if (Result.X > RightBtm.X) Result = RightBtm;


            return Result;
        }
        private static Point getNearTop(double xPosition, double yPosition, double width, double height, double angle)
        {
            Point LeftTop = new Point();
            Point LeftBtm = new Point();
            Point RightTop = new Point();
            Point RightBtm = new Point();
            Point Result;
            double originLeftX = xPosition;
            double originLeftY = yPosition;
            double rad = (angle / 180) * Math.PI;


            LeftTop.X = (-1 * width / 2) * Math.Cos(rad) - (height / 2) * Math.Sin(rad);
            LeftTop.Y = (-1 * width / 2) * Math.Sin(rad) + (height / 2) * Math.Cos(rad);
            Result = LeftTop;

            LeftBtm.X = (-1 * width / 2) * Math.Cos(rad) - (-1 * height / 2) * Math.Sin(rad);
            LeftBtm.Y = (-1 * width / 2) * Math.Sin(rad) + (-1 * height / 2) * Math.Cos(rad);
            if (Result.Y < LeftBtm.Y) Result = LeftBtm;

            RightTop.X = (width / 2) * Math.Cos(rad) - (height / 2) * Math.Sin(rad);
            RightTop.Y = (width / 2) * Math.Sin(rad) + (height / 2) * Math.Cos(rad);
            if (Result.Y < RightTop.Y) Result = RightTop;

            RightBtm.X = (width / 2) * Math.Cos(rad) - (-1 * height / 2) * Math.Sin(rad);
            RightBtm.Y = (width / 2) * Math.Sin(rad) + (-1 * height / 2) * Math.Cos(rad);
            if (Result.Y < RightBtm.Y) Result = RightBtm;


            return Result;
        }
        private static string getStylePosition(double xPosition, double yPosition, double width, double height, double angle)
        {
            string style = "";
            Point OriginleftTop = getOriginLeftTop(width, height);
            Point after_rotate_leftTop = getLeftTop(width, height, angle);

            double left;
            double top;

            if (after_rotate_leftTop.Y < OriginleftTop.Y)
                top = yPosition - Math.Abs(after_rotate_leftTop.Y - OriginleftTop.Y);
            else
                top = yPosition + Math.Abs(after_rotate_leftTop.Y - OriginleftTop.Y);

            if (after_rotate_leftTop.X < OriginleftTop.X)
                left = xPosition + Math.Abs(after_rotate_leftTop.X - OriginleftTop.X);
            else
                left = xPosition - Math.Abs(after_rotate_leftTop.X - OriginleftTop.X);


            style += "left : " + left + "px; ";
            style += "top : " + top + "px; ";
            return style;

        }
        public static string zeroToMarginPadding()
        {
            string style = "margin : 0px; padding:0px; ";
            return style;
        }
        public static string positionFixed()
        {
            string style = "position : fixed; ";
            return style;
        }
        public static string getStyleAngle(double angle)
        {
            string style = " transform: rotate(" + angle + "deg); ";
            return style;
        }
        public static string getStyleBackGroundColor(Color bgColor)
        {
            string style = " background-color : rgba(" + bgColor.R + "," + bgColor.G + "," + bgColor.B + "," + bgColor.A + "); ";
            return style;
        }
        public static string getStyleForeGround(Color fontColor)
        {
            string style = " color : " + "rgba(" + fontColor.R + "," + fontColor.G + "," + fontColor.B + "," + fontColor.A + "); ";
            return style;
        }
        public static string getStyleHeight(double height)
        {
            string style = "";
            style += " height : " + height + "px; ";
            return style;
        }
        public static string getStyleWidth(double width)
        {
            string style = "";
            style += "width : " + width + "px; ";
            return style;
        }
        public static string getStyleHeightAndWidth(double width, double height, double angle)
        {
            string style = "";
            double rad = (angle / 180) * Math.PI;
            double htmlHeight = Math.Abs(width * Math.Sin(rad)) + Math.Abs(height * Math.Cos(rad));
            double htmlWidth = Math.Abs(width * Math.Cos(rad)) + Math.Abs(height * Math.Sin(rad));
            style += " height : " + htmlHeight + "px; ";
            style += " width : " + htmlWidth + "px; ";
            return style;
        }

        public static string getStyle_Margin_Left(double XPosition)
        {
            string style = " margin-left : " + XPosition + "px; ";
            return style;
        }
        public static string getStyle_Margin_top(double YPosition)
        {
            string style = " margin-top : " + YPosition + "px; ";
            return style;
        }
        public static string getStyleZIndex(int zIndex)
        {
            string style = " z-index : " + zIndex + "; ";
            return style;
        }

    }
}