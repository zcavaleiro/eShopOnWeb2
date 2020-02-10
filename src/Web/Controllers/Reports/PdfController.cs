using Microsoft.AspNetCore.Mvc;
using IronPdf;
using System;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers.Pdf
{
    [Route("reports/[controller]/[action]")]
    public class PdfController : Controller
    {

        [HttpGet]
        public async Task<FileResult> Catalog(){
            var query = this.Request.QueryString.ToString();
            string root = $"{Request.Scheme}://{Request.Host}/pdf{query}";
            var uri = new Uri(root);//"https://localhost:5001/reports/pdf/catalog"
            var urlToPdf = new HtmlToPdf();
            var pdf = await urlToPdf.RenderUrlAsPdfAsync(uri);
            //pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "UrlToPdfExample1.Pdf"));
            return File(pdf.BinaryData, "application/pdf;", "Catalog.pdf");
        }
    }
}