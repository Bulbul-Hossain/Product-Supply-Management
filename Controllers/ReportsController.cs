using ProductSupplyManagement.Models;
using ProductSupplyManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;

namespace ProductSupplyManagement.Controllers
{
    public class ReportsController : Controller
    {
        private PsiDbContext db = new PsiDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InventoryByMonth(string format="")
        {
            var data = db.Inventories
                .GroupBy(x => new { Month = x.Date.Month, Year = x.Date.Year })
                .OrderByDescending(x => x.Key.Month).ThenByDescending(x=>x.Key.Month)
                .AsEnumerable()
                .Select(x => new InventoryByMonthViewModel
                {
                    Year = x.Key.Year,
                    Month = x.Key.Year,
                    MonthYear = GetMonthName(x.Key.Month) + ", " + x.Key.Year,
                    Inventories = x.Select(i => i),
                    TotalQ = x.Sum(i=> i.Quantity)
                });
            if (string.IsNullOrEmpty(format))
                return View("InventoryByMonth",data.ToList());
            else
                return View("InventoryByMonthPrint", data.ToList());
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml, string reportName = "")
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                string fileName = (reportName == "" ? "Report" : reportName) + DateTime.Now.Ticks + ".pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
        private string GetMonthName(int m)
        {
            switch (m)
            {
                case 1:
                    return "JAN";
                case 2:
                    return "FEB";
                case 3:
                    return "MAR";
                case 4:
                    return "APR";
                case 5:
                    return "MAY";
                case 6:
                    return "JUN";
                case 7:
                    return "JUL";
                case 8:
                    return "AUG";
                case 9:
                    return "SEP";
                case 10:
                    return "OCT";
                case 11:
                    return "NOV";
                case 12:
                    return "DEC";
                default:
                    return m.ToString();
            }
        }
    }
}