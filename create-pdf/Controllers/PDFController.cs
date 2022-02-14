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

            var title = new Paragraph("Test!");

            pdf.Add(title);

            pdf.Close();

            byte[] content = stream.ToArray();

            return File(content, "application/pdf", "teste.pdf");
        }
    }
}
