using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Data;

namespace Miseng.View.ExtendUIMaking
{
    public class HueToSolidBrush : IValueConverter
    {
        public static HueToSolidBrush Instance = new HueToSolidBrush();
        #region IValueConverter 멤버

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double Hue = (double)value;
            if (Hue >= 360) Hue = 0;
            return ColorUtilities.CreateColorFromHSB(255, Hue, 1, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public static class ColorUtilities
    {

        public static double[] ToHSB(this Color Source)
        {
            double R = ((double)Source.R / 255.0);
            double G = ((double)Source.G / 255.0);
            double B = ((double)Source.B / 255.0);

            double Max = Math.Max(R, Math.Max(G, B));
            double Min = Math.Min(R, Math.Min(G, B));

            double Hue = 0.0, Saturation = 0.0, Brightness = 0.0;

            if (Max == R && G >= B && (Max - Min == 0)) Hue = 0;
            else if (Max == R && G >= B) Hue = 60 * (G - B) / (Max - Min);
            else if (Max == R && G < B) Hue = 60 * (G - B) / (Max - Min) + 360;
            else if (Max == G) Hue = 60 * (B - R) / (Max - Min) + 120;
            else if (Max == B) Hue = 60 * (R - G) / (Max - Min) + 240;

            Saturation = (Max == 0) ? 0.0 : (1.0 - (Min / Max));
            Brightness = Max;

            return new double[3] { Hue, Saturation, Brightness };
        }

        public static Color CreateColorFromHSB(byte Alpha, double Hue, double Saturation, double Brightness)
        {

            if (Hue >= 360) Hue = 0;

            if (Saturation == 0)
            {
                byte ColorValue = (byte)(Brightness * 255);
                return Color.FromArgb(Alpha, ColorValue, ColorValue, ColorValue);
            }

            double SectorPosistion = Hue / 60.0;
            int SectorIndex = (int)(Math.Floor(SectorPosistion));
            double FractionalSector = SectorPosistion - SectorIndex;

            double p = Brightness * (1.0 - Saturation);
            double q = Brightness * (1.0 - (Saturation * FractionalSector));
            double t = Brightness * (1.0 - (Saturation * (1 - FractionalSector)));

            if (SectorIndex == 0) return Color.FromArgb(Alpha, (byte)(Brightness * 255), (byte)(t * 255), (byte)(p * 255));
            else if (SectorIndex == 1) return Color.FromArgb(Alpha, (byte)(q * 255), (byte)(Brightness * 255), (byte)(p * 255));
            else if (SectorIndex == 2) return Color.FromArgb(Alpha, (byte)(p * 255), (byte)(Brightness * 255), (byte)(t * 255));
            else if (SectorIndex == 3) return Color.FromArgb(Alpha, (byte)(p * 255), (byte)(q * 255), (byte)(Brightness * 255));
            else if (SectorIndex == 4) return Color.FromArgb(Alpha, (byte)(t * 255), (byte)(p * 255), (byte)(Brightness * 255));
            else if (SectorIndex == 5) return Color.FromArgb(Alpha, (byte)(Brightness * 255), (byte)(p * 255), (byte)(q * 255));

            return Colors.White;

        }

    }
}