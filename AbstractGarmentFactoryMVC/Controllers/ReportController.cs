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

namespace AbstractGarmentFactoryMVC.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;
        private AbstractDBScope context;
        public ReportController(IReportService service)
        {
            reportService = service;
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
        public ActionResult CustomerIndents()
        {

            ReportViewer reportViewer = new ReportViewer();


            var dataSource = reportService.GetCustomerIndents(new ReportBindingModel
            {
                FileName = @"B:\temp\test2.pdf",            
                DateFrom = new DateTime(2018, 1, 1),
                DateTo = DateTime.Now
            });
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report1.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetIndents", dataSource));
            //ReportDataSource source = new ReportDataSource("DataSetIndents", dataSource);
            //reportViewer.LocalReport.DataSources.Add(source);
            ViewBag.ReportViewer = reportViewer;
            
            return View(reportViewer);
        }

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
    }
}