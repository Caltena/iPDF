
using System.IO;
using iTextSharp.text;
using System.Linq;
using System.Xml.Linq;
using System;

namespace iText_Net_DLL
{
    public class ReadImage : ConverterXML
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Scale { get; set; }


        private void initClass()
        {
            this.PosX = 10;
            this.PosY = 10;
            this.Scale = 100;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        public ReadImage()
        {
            initClass();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerReferenz"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public ReadImage(string xTag ,string filename)
        {
            initClass();
            XDocument loaded = XDocument.Load(filename);

            /* Allgemein */
            this.PosY = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants(xTag) select (string)c.Element("PosY")).SingleOrDefault()));
            this.PosX = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants(xTag) select (string)c.Element("PosX")).SingleOrDefault()));
            this.Scale = MmmToPixel(Convert.ToDouble((from c in loaded.Descendants(xTag) select (string)c.Element("Scale")).SingleOrDefault()));
        }
    }

}
