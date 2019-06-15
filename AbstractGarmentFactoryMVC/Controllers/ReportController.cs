using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceImplementDataBase;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using GemBox.Spreadsheet;
using Xceed.Words.NET;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using Paragraph = Xceed.Words.NET.Paragraph;
using Table = Xceed.Words.NET.Table;

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        private readonly AbstractDBScope context = new AbstractDBScope();

        public ReportController(IReportService service)
        {
            reportService = service;
        }

        public ReportController()
        {
        }

        public ActionResult Index()
        {
            ViewBag.ServiceReport = reportService;
            return View();
        }

        public ActionResult CustomerIndents()
        {
            return Redirect("/Report1.aspx");
        }

        public ActionResult StoragesLoad()
        {
            return Redirect("/Report2.aspx");
        }

        public ActionResult FabricValue()
        {
            SaveFabricValue(new ReportBindingModel
             {
                 FileName = @"B:\temp\FabricValue.doc"
             });
            return RedirectToAction("Index");
        }

        public void SaveFabricValue(ReportBindingModel model)
        {
            try
            {
                DocX document = null;
                document = DocX.Create(Server.MapPath("~/mydoc.docx"), DocumentTypes.Document);
                Paragraph picturepara = document.InsertParagraph();
                picturepara.Alignment = Alignment.center;
                var headLineFormat = new Formatting();
                headLineFormat.Size = 18D;
                headLineFormat.Position = 12;

                var products = context.Fabrics.ToList();

                var paraFormat = new Formatting();
                paraFormat.Size = 11.0f;
                paraFormat.CapsStyle = CapsStyle.none;
                Table table = document.AddTable(products.Count+1, 2);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.LightGridAccent2;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Name");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Value");
                
                for (int i = 0; i < products.Count; i++)
                {
                   
                    table.Rows[i+1].Cells[0].Paragraphs.First().Append(products[i].FabricName);
                    table.Rows[i+1].Cells[1].Paragraphs.First().Append(products[i].Value.ToString());
                }
                document.InsertTable(table);

                document.Save();

                MemoryStream ms = new MemoryStream();
                document.SaveAs(ms);
               
                byte[] byteArray = ms.ToArray();
                ms.Flush();
                ms.Close();
                ms.Dispose();
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=report.docx");
                Response.AddHeader("Content-Length", byteArray.Length.ToString());
                Response.ContentType = "application/msword";
                Response.BinaryWrite(byteArray);
                Response.End();              
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}