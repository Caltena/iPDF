using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iText_Net_DLL
{
    public class ConverterXML
    {



        /// <summary>
        /// Centimeterses to pixel.
        /// </summary>
        /// <param name="mmm">The centimeters.</param>
        /// <returns></returns>
        public int MmmToPixel(double mmm)
        {
            double inches = (mmm / 254) * 72.0;
            return (int)Math.Round(inches);
        }



        public BaseColor cBasecolor(string _Basecolor)
        {
            BaseColor rBaseColor;
            switch (_Basecolor.ToLower())
            {
                case "black":
                    rBaseColor = BaseColor.Black;
                    break;
                case "red":
                    rBaseColor = BaseColor.Red;
                    break;
                case "green":
                    rBaseColor = BaseColor.Green;
                    break;
                case "grey":
                    rBaseColor = BaseColor.Gray;
                    break;
                default:
                    rBaseColor = BaseColor.Black;
                    break;
            }
            return rBaseColor;
        }
    }
}
