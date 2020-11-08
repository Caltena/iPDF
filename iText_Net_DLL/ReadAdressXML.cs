using System.IO;

using iTextSharp.text;


using System.Linq;
using System.Xml.Linq;
using System;

namespace iText_Net_DLL
{
    public class ReadAdress : ConverterXML
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int TotalWidth { get; set; }
        public BaseColor BorderColor;
        public int Border { get; set; }




        /* Text
         * **************************************   */
        public string TextFont { get; set; }
        public int TextFontSize { get; set; }
        public int TextFontStyle { get; set; }
        public BaseColor TextFontBaseColor { get; set; }

        private string _TextBaseColor;
        public string TextBaseColor
        {
            get { return _TextBaseColor; }
            set
            {
                _TextBaseColor = value;
                TextFontBaseColor = cBasecolor(_TextBaseColor);
            }
        }



        private void initClass()
        {
            this.PosX = 10;
            this.PosY = 10;
            this.Border = 0;


            this.TextFont = "Arial";
            this.TextFontSize = 10;
            this.TextFontStyle = 0;
            this.TextBaseColor = "black";

            this.BorderColor = BaseColor.Orange;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        public ReadAdress()
        {
            initClass();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public ReadAdress(string filename)
        {
            initClass();
            XDocument loaded = XDocument.Load(filename);

            /* Allgemein */
            XElement xTest = (from c in loaded.Descendants("OfferAdress") select c.Element("STYLE")).SingleOrDefault();
            this.PosY = MmmToPixel(Convert.ToDouble(xTest.Attribute("PosY").Value));
            this.PosX = MmmToPixel(Convert.ToDouble(xTest.Attribute("PosX").Value));
            this.TotalWidth = MmmToPixel(Convert.ToDouble(xTest.Attribute("TotalWidth").Value));
            this.Border = Convert.ToInt32(xTest.Attribute("Border").Value);

            /* TEXT */
            xTest = (from c in loaded.Descendants("OfferAdress") select c.Element("TEXT")).SingleOrDefault();
            this.TextFontSize = Convert.ToInt32(xTest.Attribute("FontSize").Value);
            this.TextFontStyle = Convert.ToInt32(xTest.Attribute("FontStyle").Value);
            this.TextBaseColor = xTest.Attribute("FontBaseColor").Value.ToString();
            this.TextFont = xTest.Attribute("Font").Value.ToString();
        }


    }

}
