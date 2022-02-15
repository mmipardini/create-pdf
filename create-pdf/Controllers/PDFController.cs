using create_pdf.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            var person = new List<Person>()
            {
                new Person() { Id = 1, Name = "Johnny", Age = 27 },
                new Person() { Id = 2, Name = "Micaela", Age = 23 }
            };

            PdfPTable contentTable = new(3);
            contentTable.SetWidths(new float[] { 50, 200, 200 });

            contentTable.AddCell("Id: ");
            contentTable.AddCell("Name: ");
            contentTable.AddCell("Age: ");

            foreach (var p in person)
            {                
                contentTable.AddCell(p.Id.ToString());                
                contentTable.AddCell(p.Name);              
                contentTable.AddCell(p.Age.ToString());
            }            

            pdf.Add(contentTable);

            pdf.Close();

            byte[] content = stream.ToArray();

            return File(content, "application/pdf", "teste.pdf");            
        }
    }
}