using create_pdf.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace create_pdf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Person> CreatePdf()
        {
            Document pdf = new(PageSize.A4);
            MemoryStream stream = new();
            PdfWriter.GetInstance(pdf, stream);

            pdf.Open();

            // i'm not very creative as we can see
            var title = new Paragraph("Person etc");

            pdf.Add(title);

            PdfPTable contentTable = new(3);
            contentTable.SetWidths(new float[] { 200, 200, 200 });
            contentTable.AddCell("Id: ");
            contentTable.AddCell("Name: ");
            contentTable.AddCell("Age: ");

            pdf.Add(contentTable);

            pdf.Close();

            byte[] content = stream.ToArray();

            return File(content, "application/pdf", "teste.pdf");
        }
    }
}
