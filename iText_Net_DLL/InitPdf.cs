using System;
using System.IO;
using System.Collections;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Linq;


using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf.codec;
using System.ComponentModel;

using System.Xml.Linq;
using System.Dynamic;


namespace iText_Net_DLL
{

    public class InitPdf : DocWriter
    {

        private Document document;
        private PdfWriter writer;

        private string _sCustomerFile;
        public string sCustomerFile
        {
            get { return _sCustomerFile; }
            set { _sCustomerFile = value; }
        }


        public InitPdf(string _File, Document _doc ,string _sCustomerFile)
        {
            FileStream fs = new FileStream(_File, FileMode.Create, FileAccess.Write, FileShare.None);
            this.document = _doc;
            this.writer = PdfWriter.GetInstance(document, fs);
            this.sCustomerFile = _sCustomerFile;
            writer.PageEvent = new ITextEvents();
        }


        public void Start()
        {
            this.document.Open();
            SetMetaPDF("Claus Altena");
            SetQR();
        }


        public void Ende()
        {
            writer.AddJavaScript("this.print();");
            this.document.Close();
            writer.Close();
        }


        public void SetQR()
        {
            /* QR-Code */
             Image qrcodeImage = CreateQrCodeImage("This is a text ...");
             qrcodeImage.SetAbsolutePosition(100, 100 );
             document.Add(qrcodeImage);
        }



        public void SetImage( string xTag , string sFile)
        {
            ReadImage cAdd = new ReadImage(xTag, "Customer.xml");
            Image qrcodeImage = Image.GetInstance(sFile);
            qrcodeImage.SetAbsolutePosition(cAdd.PosX, (float)(document.PageSize.Height - cAdd.PosY  - (int)Math.Round((qrcodeImage.Height / 254) * 72.0) ));
            qrcodeImage.ScalePercent(cAdd.Scale );
            document.Add(qrcodeImage);
        }



        public void createTable5()
        {

            PdfPTable table = new PdfPTable(9);
            table.TotalWidth = (float)document.PageSize.Width;
            table.LockedWidth = true;

            float[] widths = new float[] { 0.1f, 0.2f , 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };

            table.SetWidths(widths);

            // table.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;

            //leave a gap before and after the table

            table.SpacingBefore = 400f;
            table.SpacingAfter = 3f;

            table.HeaderRows = 1;
            table.AddCell(CreateHeaderCell("H1"));
            table.AddCell(CreateHeaderCell("H2"));
            table.AddCell(CreateHeaderCell("H3"));
            table.AddCell(CreateHeaderCell("H4"));
            table.AddCell(CreateHeaderCell("H5"));
            table.AddCell(CreateHeaderCell("H6"));
            table.AddCell(CreateHeaderCell("H7"));
            table.AddCell(CreateHeaderCell("H8"));
            table.AddCell(CreateHeaderCell("H9"));

            for (int i = 0; i < 60; i++)
            {
                table.AddCell(("D1"));
                table.AddCell(("D2"));
                table.AddCell(("D3"));
                table.AddCell(("D4"));
                table.AddCell(("D5"));
                table.AddCell(("D6"));
                table.AddCell(("D7"));
                table.AddCell(("D8"));
                table.AddCell(("D9"));
            }
       
            this.document.Add (table);
        }

        public PdfPTable CustomerAdress(List<string> sCustomerAdress, string sSender )
        {
            ReadAdress cAdd = new ReadAdress("Customer.xml");
            PdfContentByte pcb = writer.DirectContent;
            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = Convert.ToSingle(cAdd.TotalWidth);
            table.SplitRows = true;

            iTextSharp.text.Font fontTable = FontFactory.GetFont(cAdd.TextFont, cAdd.TextFontSize, cAdd.TextFontStyle, cAdd.TextFontBaseColor);
            iTextSharp.text.Font AdressHeader = FontFactory.GetFont(cAdd.TextFont, 4, cAdd.TextFontStyle, cAdd.TextFontBaseColor);
            table.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cell = new PdfPCell(new Phrase(sSender, AdressHeader));
            cell.Colspan = 2;
            cell.Border = 0;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);


            foreach (string cRef in sCustomerAdress)
            {
                table.AddCell(CreateAdressCell(cRef, fontTable, 0, 0, 0, 0));
            }
            table.WriteSelectedRows(0, -1, cAdd.PosX, document.PageSize.Height - cAdd.PosY, pcb);
            return (table);
        }

        private PdfPCell CreateHeaderCell(string sText )
        {
            PdfPCell pcell = new PdfPCell();
            pcell.Phrase = new Phrase(sText);
            pcell.Border = 0;
            
            pcell.VerticalAlignment = Element.ALIGN_BOTTOM;
            pcell.HorizontalAlignment = Element.ALIGN_CENTER;
            return pcell;
        }

        public PdfPTable CustomerRef(System.Collections.Generic.List<CustomerRef> customerRefs)
        {
            CustomerReferenz cAdd = new CustomerReferenz("Customer.xml");
            PdfContentByte pcb = writer.DirectContent;
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = Convert.ToSingle(cAdd.TotalWidth);
            table.SplitRows = true;

            iTextSharp.text.Font fontTable = FontFactory.GetFont(cAdd.LabelFont, cAdd.LabelFontSize, cAdd.LabelFontStyle, cAdd.LabelFontBaseColor);
            iTextSharp.text.Font fontTable1 = FontFactory.GetFont(cAdd.TextFont, cAdd.TextFontSize, cAdd.TextFontStyle, cAdd.TextFontBaseColor);

            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            int i = 0;
            foreach( CustomerRef cRef in customerRefs)
            {
                i++;
                if (i == 1)
                {
                    table.AddCell(CreateAdressCell(cRef.sLabel, fontTable, cAdd.Border, 0, 0, cAdd.Border));
                    table.AddCell(CreateAdressCell(cRef.sText, fontTable1, cAdd.Border, 0, cAdd.Border, 0));
                }
                else if (i == customerRefs.Count())
                {
                    table.AddCell(CreateAdressCell(cRef.sLabel, fontTable, 0, cAdd.Border, 0, cAdd.Border));
                    table.AddCell(CreateAdressCell(cRef.sText, fontTable1, 0, cAdd.Border, cAdd.Border, 0));
                }
                else
                {
                    table.AddCell(CreateAdressCell(cRef.sLabel, fontTable, 0, 0, 0, cAdd.Border));
                    table.AddCell(CreateAdressCell(cRef.sText, fontTable1, 0, 0, cAdd.Border, 0));

                }

            }
            table.WriteSelectedRows(0, -1, cAdd.PosX, document.PageSize.Height - cAdd.PosY, pcb);
            return (table);
        }

        private PdfPCell CreateAdressCell(string sText, iTextSharp.text.Font fontTable, int bTop, int bBottom, int bRight, int bLeft)
        {
            PdfPCell pcell = new PdfPCell();
            pcell.Phrase = new Phrase(sText, fontTable);
            pcell.Border = 0;
            pcell.BorderColor = BaseColor.Green;

            if (bTop == 1) pcell.BorderWidthTop = 1;
            if (bBottom == 1) pcell.BorderWidthBottom = 1;
            if (bRight == 1) pcell.BorderWidthRight = 1;
            if (bLeft == 1) pcell.BorderWidthLeft = 1;


            pcell.VerticalAlignment = Element.ALIGN_BOTTOM;
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            return pcell;
        }

        public void SetText(BaseFont bF, Int32 x, Int32 y)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            cb.BeginText();
            cb.MoveText(x, y);
            cb.SetFontAndSize(bF, 12);
            cb.ShowText("My status /n ggdgdgdgdggd");
            cb.EndText();
            cb.RestoreState();
        }

        public void SetMetaPDF(string Author)
        {
            document.AddTitle("Hello World example");
            document.AddSubject("This is an Example 4 of Chapter 1 of Book 'iText in Action'");
            document.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
            document.AddCreator("iTextSharp 5.4.4");
            document.AddAuthor(Author);
            document.AddHeader("Nothing", "No Header");
        }

        private static Image CreateQrCodeImage(string content)
        {
            var qrCodeWriter = new QRCodeWriter();
            var byteMatrix = qrCodeWriter.Encode(content, 1, 1, null);
            var width = byteMatrix.GetWidth();
            var height = byteMatrix.GetHeight();
            var stride = (width + 7) / 8;
            var bitMatrix = new byte[stride * height];
            var byteMatrixArray = byteMatrix.GetArray();
            for (int y = 0; y < height; ++y)
            {
                var line = byteMatrixArray[y];
                for (var x = 0; x < width; ++x)
                {
                    if (line[x] != 0)
                    {
                        int offset = stride * y + x / 8;
                        bitMatrix[offset] |= (byte)(0x80 >> (x % 8));
                    }
                }
            }
            var encodedImage = Ccittg4Encoder.Compress(bitMatrix, byteMatrix.GetWidth(), byteMatrix.GetHeight());
            var qrcodeImage = Image.GetInstance(byteMatrix.GetWidth(), byteMatrix.GetHeight(), false, Image.CCITTG4, Image.CCITT_BLACKIS1, encodedImage, null);
            return qrcodeImage;
        }


    }




    public class CustomerRef
    {
        public string sLabel { get; set; }
        public string sText { get; set; }

        public CustomerRef( string _sLabel , string _sText)
        {
            this.sLabel = _sLabel;
            this.sText = _sText;
        }
    }
}
