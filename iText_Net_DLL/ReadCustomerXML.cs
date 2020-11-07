﻿using System.IO;

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
            string currentDirectory = @"c:\apps";
            String Filepath = Path.Combine(currentDirectory, filename);
            XDocument loaded = XDocument.Load(Filepath);

            /* Allgemein */
            this.PosY = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferCustomerInfo") select (string)c.Element("PosY")).SingleOrDefault()));
            this.PosX = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferCustomerInfo") select (string)c.Element("PosX")).SingleOrDefault()));
            this.TotalWidth = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferCustomerInfo") select (string)c.Element("TotalWidth")).SingleOrDefault()));
            this.Border = Convert.ToInt32((from c in loaded.Descendants("OfferCustomerInfo") select (string)c.Element("Border")).SingleOrDefault());

            /* Label */
            this.LabelFont = (from c in loaded.Descendants("Offer_Label") select (string)c.Element("Font")).SingleOrDefault();
            this.LabelFontSize = Convert.ToInt32((from c in loaded.Descendants("Offer_Label") select (string)c.Element("FontSize")).SingleOrDefault());
            this.LabelFontStyle = Convert.ToInt32((from c in loaded.Descendants("Offer_Label") select (string)c.Element("FontStyle")).SingleOrDefault());
            this.LabelBaseColor = (from c in loaded.Descendants("Offer_Label") select (string)c.Element("FontBaseColor")).SingleOrDefault();

            /* Text */
            this.TextFont = (from c in loaded.Descendants("Offer_Text") select (string)c.Element("Font")).SingleOrDefault();
            this.TextFontSize = Convert.ToInt32((from c in loaded.Descendants("Offer_Text") select (string)c.Element("FontSize")).SingleOrDefault());
            this.TextFontStyle = Convert.ToInt32((from c in loaded.Descendants("Offer_Text") select (string)c.Element("FontStyle")).SingleOrDefault());
            this.TextBaseColor = (from c in loaded.Descendants("Offer_Text") select (string)c.Element("FontBaseColor")).SingleOrDefault();



        }
    }

}