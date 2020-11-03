using System;
using System.IO;
using System.Xml;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

using iTextSharp.text.pdf.qrcode;
using iTextSharp.text.pdf.codec;
using System.ComponentModel;

namespace iText_Net_DLL
{
    public class InitPdf
    {

        private Document document;
        private PdfWriter writer;

        public InitPdf(string _File)
        {
            FileStream fs = new FileStream(_File, FileMode.Create, FileAccess.Write, FileShare.None);

            /*
             So, we need to do set the following points for the Left, Right, Top, Bottom Margins respectively as we already know that iTextSharp library only understand points where 72 points = 1 inch.
            Left Margin: 36pt => 1,27 cm
            Right Margin: 72pt => 2,54 cm
            Top Margin: 108pt => 3,81 cm
            Bottom Margini: 180pt => 6,35 cm
            */

            this.document = new Document( PageSize.A4, 180, 72, 108, 180);
            this.writer = PdfWriter.GetInstance(document, fs);
            Start();

            
        }


        public void Start()
        {
            this.document.Open();

            SetMetaPDF("Claus Altena");

            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));

            /* QR-Code */
            Image qrcodeImage = CreateQrCodeImage("This is a text ...");
            qrcodeImage.SetAbsolutePosition(10, 500);
            //qrcodeImage.ScalePercent(200);
            qrcodeImage.Width =840;

            document.Add(qrcodeImage);

            /* New Page */
            document.NewPage();

            /* Fill Document */
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));
            this.document.Add(new Paragraph("Hello"));

           
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            SetText(bf, 200, 500);


            this.document.Close();
            writer.Close();

        }



        public void SetText ( BaseFont bF , Int32 x , Int32 y)
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


        public void SetMetaPDF( string Author )
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
