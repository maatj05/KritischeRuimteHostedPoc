using KritischeRuimteHostedPoc.Shared;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KritischeRuimteHostedPoc.Server.Services
{
    public class PdfFormCreator : IPdfFormCreator
    {

        public PdfFormCreator(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        private readonly IWebHostEnvironment hostEnvironment;

        public string Create(CreatePdfRequest formulierModel)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
           // Create an empty page

            PdfPage page = document.AddPage();
            page.TrimMargins = new TrimMargins() { All = new XUnit(40) };

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            // Draw the text
            gfx.DrawString(formulierModel.Name, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.CenterRight);


            var bytes = Convert.FromBase64String(formulierModel.Signature.Replace("data:image/png;base64,", ""));
            using (var signatureStream = new MemoryStream(bytes))
            {
                gfx.DrawImage(XImage.FromStream(signatureStream), 0, 0);
            }



            // Save the document...
            //const string filename = "HelloWorld.pdf";

            string file = System.IO.Path.Combine("pdf", $"file-{Guid.NewGuid()}.pdf");

            string filePth = System.IO.Path.Combine(hostEnvironment.WebRootPath, file);

            document.Save(filePth);
            return file;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    document.Save(ms);
            //    return ms.ToArray();
            //}
        }
    }
}
