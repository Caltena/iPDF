using System.IO;

using iTextSharp.text;


using System.Linq;
using System.Xml.Linq;
using System;

namespace iText_Net_DLL  
{
    public class CustomerReferenz : ConverterXML
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int TotalWidth { get; set; }
        public BaseColor BorderColor;
        public int Border { get; set; }

        /* Label
         * **************************************   */
        public string LabelFont { get; set; }
        public int LabelFontSize { get; set; }
        public int LabelFontStyle { get; set; }
        public BaseColor LabelFontBaseColor { get; set; }

        private string _LabelBaseColor;
        public string LabelBaseColor
        {
            get { return _LabelBaseColor; }
            set
            {
                _LabelBaseColor = value;
                LabelFontBaseColor = cBasecolor(_LabelBaseColor);
            }
        }



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

        /* LabelText
         * **************************************   */
        public String KundenNr { get; set; }
        public String Nummer { get; set; }
        public String Datum { get; set; }
        public String AP { get; set; }


        private void initClass()
        {
            this.PosX = 10;
            this.PosY = 10;
            this.Border = 0;

            this.LabelFont = "Arial";
            this.LabelFontSize = 10;
            this.LabelFontStyle = 0;
            this.LabelBaseColor = "black";

            this.TextFont = "Arial";
            this.TextFontSize = 10;
            this.TextFontStyle = 0;
            this.TextBaseColor = "black";

            this.BorderColor = BaseColor.Orange ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        public CustomerReferenz()
        {
            initClass();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public CustomerReferenz(string filename)
        {
            initClass();
            XDocument loaded = XDocument.Load(filename);

            /* Allgemein */
            XElement xTest = (from c in loaded.Descendants("OfferCustomerInfo") select c.Element("STYLE")).SingleOrDefault();
            this.PosY = MmmToPixel(Convert.ToDouble(xTest.Attribute("PosY").Value));
            this.PosX = MmmToPixel(Convert.ToDouble(xTest.Attribute("PosX").Value));
            this.TotalWidth = MmmToPixel(Convert.ToDouble(xTest.Attribute("TotalWidth").Value));
            this.Border = Convert.ToInt32(xTest.Attribute("Border").Value);

            /* Label */
            xTest = (from c in loaded.Descendants("OfferCustomerInfo") select c.Element("LABEL")).SingleOrDefault();
            this.LabelFontSize = Convert.ToInt32(xTest.Attribute("FontSize").Value);
            this.LabelFontStyle = Convert.ToInt32(xTest.Attribute("FontStyle").Value);
            this.LabelBaseColor = xTest.Attribute("FontBaseColor").Value.ToString();
            this.LabelFont = xTest.Attribute("Font").Value.ToString();

            /* Text */
            xTest = (from c in loaded.Descendants("OfferCustomerInfo") select c.Element("TEXT")).SingleOrDefault();
            this.TextFontSize = Convert.ToInt32(xTest.Attribute("FontSize").Value);
            this.TextFontStyle = Convert.ToInt32(xTest.Attribute("FontStyle").Value);
            this.TextBaseColor = xTest.Attribute("FontBaseColor").Value.ToString();
            this.TextFont = xTest.Attribute("Font").Value.ToString();

        }
    }

}
