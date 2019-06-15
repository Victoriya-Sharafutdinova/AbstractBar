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
              //  headLineFormat.FontFamily = new System.Drawing.FontFamily("Arial Black");
                headLineFormat.Size = 18D;
                headLineFormat.Position = 12;

                var products = context.Fabrics.ToList();

                var paraFormat = new Formatting();
              //  paraFormat.FontFamily = new System.Drawing.FontFamily("Calibri");
                paraFormat.Size = 11.0f;
                paraFormat.CapsStyle = CapsStyle.none;
                Table table = document.AddTable(products.Count+1, 2);
                table.Alignment = Alignment.center;
                table.Design = TableDesign.LightGridAccent2;
                table.Rows[0].Cells[0].Paragraphs.First().Append("Name");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Value");
                //for (int i = 0; i < products.Count; ++i)
                //{
                //    table.Rows[i+1].Cells[1].Paragraphs.First().Append( products[i].FabricName);
                //    table.Rows[i + 1].Cells[2].Paragraphs.First().Append (products[i].Value.ToString());
                //}
                for (int i = 0; i < products.Count; i++)
                {
                    
                    table.Rows[i+1].Cells[0].Paragraphs.First().Append(products[i].FabricName);
                    table.Rows[i+1].Cells[1].Paragraphs.First().Append(products[i].Value.ToString());
                }
                document.InsertTable(table);

                document.Save();

                MemoryStream ms = new MemoryStream();
                document.SaveAs(ms);
                // document.Save(ms, SaveFormat.Docx);
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



        //public ActionResult StoragesLoad()
        //{




        //    ReportViewer reportViewer = new ReportViewer();
        //    reportViewer.ProcessingMode = ProcessingMode.Local;
        //    reportViewer.SizeToReportContent = true;
        //    reportViewer.Width = Unit.Percentage(100);
        //    reportViewer.Height = Unit.Percentage(100);
        //    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report1.rdlc";
        //    reportViewer.LocalReport.DataSources.Add(new ReportDataSource());

        //    return View(reportService.GetStoragesLoad());
        //}

        // GET: Report
        //public ActionResult CustomerIndents()
        //{

        //    ReportViewer reportViewer = new ReportViewer();


        //    var dataSource = reportService.GetCustomerIndents(new ReportBindingModel
        //    {
        //        FileName = @"B:\temp\test2.pdf",            
        //        DateFrom = new DateTime(2018, 1, 1),
        //        DateTo = DateTime.Now
        //    });
        //    reportViewer.ProcessingMode = ProcessingMode.Local;
        //    reportViewer.SizeToReportContent = true;
        //    reportViewer.Width = Unit.Percentage(100);
        //    reportViewer.Height = Unit.Percentage(100);
        //    reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report1.rdlc";
        //    reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetIndents", dataSource));
        //    //ReportDataSource source = new ReportDataSource("DataSetIndents", dataSource);
        //    //reportViewer.LocalReport.DataSources.Add(source);
        //    ViewBag.ReportViewer = reportViewer;

        //    return View(reportViewer);
        //}

        //public ActionResult SaveStoragesLoad()
        //{
        //    reportService.GetCustomerIndents(new ReportBindingModel
        //    {
        //        FileName = @"B:\temp\test2.xls",
        //    });

        //    return RedirectToAction("Index", "Indents");
        //}

        //public ActionResult SaveFabricValue()
        //{
        //    reportService.SaveFabricValue(new ReportBindingModel
        //    {
        //        FileName = @"B:\temp\test.doc"
        //    });
        //    return RedirectToAction("Index", "Indents");
        //}

        //public ActionResult FabricValue()
        //{
        //    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        //    DataTable dt = new DataTable();        
        //    dt.Columns.Add("FabricName", typeof(string));
        //    dt.Columns.Add("Value", typeof(int));
        //    Session["dt"] = dt;

        //    //DocumentModel document = Process();

        //    //string fileName = "FabricValue.doc";

        //    //document.Save(this.Response, fileName);
        //    dt = LoadDataTable();
        //    Export();


        ////var products = context.Fabrics.ToList();
        ////    var table = document.Tables.Add(rangeTable, products.Count, 2, ref missing, ref missing);

        ////    for (int i = 0; i < products.Count; ++i)
        ////    {
        ////        table.Cell(i + 1, 1).Range.Text = products[i].FabricName;
        ////        table.Cell(i + 1, 2).Range.Text = products[i].Value.ToString();
        ////    }
        ////    reportService.SaveFabricValue(new ReportBindingModel
        ////    {
        ////        FileName = @"B:\temp\FabricValue.doc"
        ////    });
        //    return RedirectToAction("Index", "Report");
        //}

        //public DocumentModel Process()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("FabricName", typeof(string));
        //    dt.Columns.Add("Value", typeof(int));
        //    string path = Path.Combine(Request.PhysicalApplicationPath, "Invoice.docx");

        //    // Load template document.
        //    DocumentModel document = new DocumentModel();

        //    var products = context.Fabrics.ToList();

        //    // Subscribe to FieldMerging event (we want to format the output).
        //    document.MailMerge.FieldMerging += (sender, e) =>
        //    {
        //        if (e.IsValueFound)
        //            switch (e.FieldName)
        //            {
        //                case "FabricName":
        //                    break;

        //                case "Value":
        //                    ((Run)e.Inline).Text = ((int)e.Value).ToString("C");
        //                    break;
        //            }
        //    };

        //    // Fill table.
        //    document.MailMerge.Execute(dt, "Item");            
        //    return document;
        //}

        //private DataTable LoadDataTable()
        //{
        //    string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
        //    string query = "SELECT FabricName, Value " +               
        //        "FROM AbstractDatabase.dbo.[Fabrics] ";

        //    using (SqlConnection sqlConn = new SqlConnection(conSTR))
        //    using (SqlCommand cmd = new SqlCommand(query, sqlConn))
        //    {

        //        sqlConn.Open();
        //        DataTable people = (DataTable)Session["dt"];
        //        people.Load(cmd.ExecuteReader());

        //        return people;
        //    }
        //}

        //private void Export()
        //{
        //    SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

        //    DataTable dt = (DataTable)Session["dt"];

        //    // Create Excel file.
        //    DocumentModel document = new DocumentModel();

        //    document.MailMerge.FieldMerging += (sender, e) =>
        //    {
        //        if (e.IsValueFound)
        //            switch (e.FieldName)
        //            {
        //                case "FabricName":
        //                    break;

        //                case "Value":
        //                    ((Run)e.Inline).Text = ((int)e.Value).ToString("C");
        //                    break;
        //            }
        //    };

        //    // Fill table.
        //    document.MailMerge.Execute(dt, "Item");

        //    // Stream or export a file to ASP.NET client's browser.
        //    document.Save(this.Response, "FabricValue.doc");
        //    reportService.SaveCustomerIndents(new ReportBindingModel
        //    {

        //        FileName = "FabricValue.doc"
        //    });
        //}
        
    
}