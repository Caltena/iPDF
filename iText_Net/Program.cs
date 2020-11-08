using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iText_Net_DLL;

namespace iText_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitPdf iPDF = new InitPdf(@"c:\apps\test.pdf", new Document(PageSize.A4 ,72, 72, 108, 108) , @"C:\apps\Customer.xml");
            InitPdf iPDF = new InitPdf(@"/home/claus/Projekte/iText_Net/test.pdf", new Document(PageSize.A4, 72, 72, 108, 108), @"/home/claus/Projekte/iText_Net/Customer.xml");
            iPDF.SetMeta();


            /* Eintragen der Referenzen
             * 
             * */
            List<CustomerRef> customerRefs = new List<CustomerRef>();
            customerRefs.Add(new CustomerRef("Kunden-Nr.", "1234"));
            customerRefs.Add(new CustomerRef("Angebots-Nr.", "1234"));
            customerRefs.Add(new CustomerRef("Angebotsdatum", "1234"));
            customerRefs.Add(new CustomerRef("Ansprechpartner", "1234"));
            customerRefs.Add(new CustomerRef("Und noch vieles Mehr", "1234"));
            iPDF.CustomerRef(customerRefs);


            /* Eintragen der Anschrift
             * 
             * */
            List<string> sAddress = new List<string>();
            sAddress.Add("Name");
            sAddress.Add("1Nameerweiterung");
            sAddress.Add("Zusatz");
            sAddress.Add("Zuhaenden");
            sAddress.Add("Strasse");
            sAddress.Add("PLZ Ort");
            sAddress.Add("Land");
            iPDF.CustomerAdress(sAddress, "Kunde * Strasse * PLZ ORT");


            /* Bilder & QR-Code
             * 
             */
           // iPDF.SetImage("ImageTOP", @"C:\Users\Claus.Altena\Pictures\Saved Pictures\itsm.png");
            iPDF.SetQR("ImageTOPRight");

            iPDF.createTable();


        }
    }
}
