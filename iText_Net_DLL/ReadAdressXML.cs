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
            string currentDirectory = @"c:\apps";
            String Filepath = Path.Combine(currentDirectory, filename);
            XDocument loaded = XDocument.Load(Filepath);

            /* Allgemein */
            this.PosY = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferAdress") select (string)c.Element("PosY")).SingleOrDefault()));
            this.PosX = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferAdress") select (string)c.Element("PosX")).SingleOrDefault()));
            this.TotalWidth = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants("OfferAdress") select (string)c.Element("TotalWidth")).SingleOrDefault()));
            this.Border = Convert.ToInt32((from c in loaded.Descendants("OfferAdress") select (string)c.Element("Border")).SingleOrDefault());


            /* Text */
            this.TextFont = (from c in loaded.Descendants("Offer_Adress") select (string)c.Element("Font")).SingleOrDefault();
            this.TextFontSize = Convert.ToInt32((from c in loaded.Descendants("Offer_Adress") select (string)c.Element("FontSize")).SingleOrDefault());
            this.TextFontStyle = Convert.ToInt32((from c in loaded.Descendants("Offer_Adress") select (string)c.Element("FontStyle")).SingleOrDefault());
            this.TextBaseColor = (from c in loaded.Descendants("Offer_Adress") select (string)c.Element("FontBaseColor")).SingleOrDefault();



        }


    }

}
