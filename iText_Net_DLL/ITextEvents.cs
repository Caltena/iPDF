using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iText_Net_DLL
{

    public class ITextEvents : PdfPageEventHelper
    {

        PdfContentByte cb;
        PdfTemplate headerTemplate, footerTemplate;
        BaseFont bf = null;
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        private string _sCol1;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public string sCol1
        {
            get { return _sCol1; }
            set { _sCol1 = value; }
        }
        #endregion

        public ITextEvents (string Col1)
        {
            this.sCol1 = Col1;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
                //handle exception here
            }
            catch (System.IO.IOException ioe)
            {
                //handle exception here
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = FontFactory.GetFont("HELVETICA", 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.Black);

            iTextSharp.text.Font baseFontBig = FontFactory.GetFont("HELVETICA", 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.Black);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            String text = "Page " + writer.PageNumber + " of ";


            //Add paging to header
            {

                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                //cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));

            }


            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 8);
                cb.SetTextMatrix(document.PageSize.GetRight(100), document.PageSize.GetBottom(40));
                cb.ShowText(text);

                cb.SetTextMatrix(document.PageSize.GetLeft(100), document.PageSize.GetBottom(40));
                cb.ShowText(this.sCol1);

                cb.EndText();
                float len = bf.GetWidthPoint(text, 8);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(100) + len, document.PageSize.GetBottom(40));
            }
            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(50));

            cb.LineTo(document.PageSize.Width - 10, document.PageSize.GetBottom(50));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            // headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 8);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();


        }
    }
}
