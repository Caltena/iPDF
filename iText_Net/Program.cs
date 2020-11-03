using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText_Net_DLL;

namespace iText_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            InitPdf iPDF = new InitPdf(@"C:\apps\test.pdf");
            //iPDF.SetHeader("Header");
            //iPDF.SetBarQR("Claus Altena \nLuxzerner Weg 19\nDüsseldorf");
            //iPDF.SetTable();
            //iPDF.ClosePDF();
        }
    }
}
