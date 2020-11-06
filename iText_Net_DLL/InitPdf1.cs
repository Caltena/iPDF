using System;
using System.IO;
using System.Xml;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf.codec;
using System.ComponentModel;

using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Dynamic;
//using System.Drawing;

namespace iText_Net_DLL
{





    public class InitPdf : DocWriter
    {

        private Document document;
        private PdfWriter writer;


        private CustomerAddress cAdd;


        public InitPdf(string _File, Document _doc)
        {
            FileStream fs = new FileStream(_File, FileMode.Create, FileAccess.Write, FileShare.None);
            cAdd = new CustomerAddress("Customer.xml");
            this.document = _doc;
            this.writer = PdfWriter.GetInstance(document, fs);

        }


        public void Start()
        {
            this.document.Open();

            SetMetaPDF("Claus Altena");

            /* QR-Code */
            // Image qrcodeImage = CreateQrCodeImage("This is a text ...");
            // qrcodeImage.SetAbsolutePosition(cAdd.PosX, cAdd.PosY );
            // qrcodeImage.ScalePercent(200);
            // qrcodeImage.Width =840;
            // document.Add(qrcodeImage);
            /* New Page */
            // document.NewPage();
            // this.document.Add(createTable5());
            // BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            // SetText(bf, 200, 500);

        }


        public void Ende()
        {
            this.document.Close();
            writer.Close();
        }









        public PdfPTable createTable5()
        {

            PdfPTable table = new PdfPTable(9);


            // Header row.
            table.AddCell(GetCell("Header 1", 1, 2));
            table.AddCell(GetCell("Header 2", 1, 2));
            table.AddCell(GetCell("Header 3", 5, 1));
            table.AddCell(GetCell("Header 4", 1, 2));
            table.AddCell(GetCell("Header 5", 1, 2));



            for (int i = 0; i < 30; i++)
            {
                table.AddCell(GetCell("D1"));
                table.AddCell(GetCell("D2"));
                table.AddCell(GetCell("D3"));
                table.AddCell(GetCell("D4"));
                table.AddCell(GetCell("D5"));
                table.AddCell(GetCell("D6"));
                table.AddCell(GetCell("D7"));
                table.AddCell(GetCell("D8"));
                table.AddCell(GetCell("D9"));
            }
            return (table);
        }


        /// <summary>Customers the reference.</summary>
        /// <param name="CustomerNo">The customer no.</param>
        /// <param name="OfferNo">The offer no.</param>
        /// <param name="OfferDate">The offer date.</param>
        /// <param name="ma">The ma.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public PdfPTable CustomerRef(string CustomerNo, string OfferNo, string OfferDate, string ma)
        {
            PdfContentByte pcb = writer.DirectContent;
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = Convert.ToSingle(cAdd.TotalWidth);
            table.SplitRows = true;

            iTextSharp.text.Font fontTable = FontFactory.GetFont(cAdd.LabelFont, cAdd.LabelFontSize, cAdd.LabelFontStyle, cAdd.LabelFontBaseColor);
            iTextSharp.text.Font fontTable1 = FontFactory.GetFont(cAdd.TextFont, cAdd.TextFontSize, cAdd.TextFontStyle, cAdd.TextFontBaseColor);

            table.DefaultCell.Border = Rectangle.NO_BORDER;

            table.AddCell(CreateAdressCell(cAdd.KundenNr, fontTable, cAdd.Border, 0, 0, cAdd.Border));
            table.AddCell(CreateAdressCell(CustomerNo, fontTable1, cAdd.Border, 0, cAdd.Border, 0));

            table.AddCell(CreateAdressCell(cAdd.Nummer, fontTable, 0, 0, 0, cAdd.Border));
            table.AddCell(CreateAdressCell(OfferNo, fontTable1, 0, 0, cAdd.Border, 0));

            table.AddCell(CreateAdressCell(cAdd.Datum, fontTable, 0, 0, 0, cAdd.Border));
            table.AddCell(CreateAdressCell(OfferDate, fontTable1, 0, 0, cAdd.Border, 0));

            table.AddCell(CreateAdressCell(cAdd.AP, fontTable, 0, cAdd.Border, 0, cAdd.Border));
            table.AddCell(CreateAdressCell(ma, fontTable1, 0, cAdd.Border, cAdd.Border, 0));

            table.WriteSelectedRows(0, -1, cAdd.PosX, document.PageSize.Height - cAdd.PosY, pcb);
            return (table);
        }


        private PdfPCell CreateAdressCell(string sText, iTextSharp.text.Font fontTable, int bTop, int bBottom, int bRight, int bLeft)
        {
            PdfPCell pcell = new PdfPCell();
            pcell.Phrase = new Phrase(sText, fontTable);
            pcell.Border = 0;
            pcell.BorderColor = cAdd.BorderColor;

            if (bTop == 1) pcell.BorderWidthTop = 1;
            if (bBottom == 1) pcell.BorderWidthBottom = 1;
            if (bRight == 1) pcell.BorderWidthRight = 1;
            if (bLeft == 1) pcell.BorderWidthLeft = 1;


            pcell.VerticalAlignment = Element.ALIGN_BOTTOM;
            pcell.HorizontalAlignment = Element.ALIGN_LEFT;
            return pcell;
        }

        private PdfPCell GetCell(string text)
        {

            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.HorizontalAlignment = 1;
            cell.BorderWidth = 0;
            cell.Rowspan = 1;
            cell.Colspan = 1;
            return cell;
        }

        private PdfPCell GetCell(string text, int colSpan, int rowSpan)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text));
            cell.HorizontalAlignment = 1;
            cell.BorderWidth = 0;
            cell.BorderWidthBottom = 0.5f;

            cell.Rowspan = rowSpan;
            cell.Colspan = colSpan;

            return cell;
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


}
