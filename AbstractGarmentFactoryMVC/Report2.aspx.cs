﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.UI;
using System.Web.UI.WebControls;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using AbstractGarmentFactoryServiceImplementDataBase;
using GemBox.Spreadsheet;

namespace AbstractGarmentFactoryMVC
{
    public partial class Report2 : System.Web.UI.Page
    {
        
        private AbstractDBScope context = new AbstractDBScope();
        private IReportService reportService;
        protected void Page_Load(object sender, EventArgs e)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");

            // By specifying a location that is under ASP.NET application's control, 
            // GemBox.Spreadsheet can use file system operations to retrieve font data even in Medium Trust environment.
            FontSettings.FontsBaseDirectory = Server.MapPath("Fonts/");

            if (!Page.IsPostBack)
            {

                DataTable people = new DataTable();

                people.Columns.Add("Компоненты", typeof(string));
                people.Columns.Add("Название склада", typeof(string));
                people.Columns.Add("Количество", typeof(int));
                

                Session["people"] = people;
                LoadDataTable();
                // this.LoadDataTable(Request.PhysicalApplicationPath + "InData.xlsx");

                this.SetDataBinding();
            }
        }

        protected void Export_Click(object sender, EventArgs e)
        {
            DataTable people = (DataTable)Session["people"];

            // Create Excel file.
            ExcelFile ef = new ExcelFile();
            ef.DefaultFontName = "Calibri";
            ExcelWorksheet ws = ef.Worksheets.Add("DataSheet");
            ws.InsertDataTable(people, new InsertDataTableOptions(0, 0) { ColumnHeaders = true });

            // Stream or export a file to ASP.NET client's browser.
            ef.Save(this.Response, "StoragesLoad.xls");
            reportService.SaveCustomerIndents(new ReportBindingModel
            {

                FileName = "StoragesLoad.xls"
            });
        }
        public List<StorageLoadViewModel> GetStoragesLoad()
        {
            return context.Storages
                .ToList()
                .GroupJoin(
                context.StorageStockings
                    .Include(r => r.Stockings)
                    .ToList(),
                storage => storage,
                storageComponent => storageComponent.Storages,
                (storage, storageCompList) => new StorageLoadViewModel
                {
                    StorageName = storage.StorageName,
                    TotalAmount = storageCompList.Sum(r => r.Amount),
                    Stockings = storageCompList.Select(r =>
                    new Tuple<string, int>(r.Stockings.StockingName, r.Amount))
                })
                    .ToList();
        }
        private DataTable LoadDataTable()
        {
            DataTable people = (DataTable)Session["people"];
            var dict = GetStoragesLoad();
            if (dict != null)
            {
                people.Rows.Clear();
                foreach (var elem in dict)
                {
                    people.Rows.Add(new object[]
                    {
                        elem.StorageName,"", 0
                    });
                    foreach (var listElem in elem.Stockings)
                    {
                        people.Rows.Add(new object[]
                        {
                            "", listElem.Item1, listElem.Item2
                        });
                    }
                    people.Rows.Add(new object[] { "Итого", "", elem.TotalAmount });
                    people.Rows.Add(new object[] { });
                }
            }
            return people;
        }
            

        

        private void LoadDataFromFile(string fileName)
        {
            DataTable people = (DataTable)Session["people"];

            ExcelFile ef = ExcelFile.Load(fileName);

            ExcelWorksheet ws = ef.Worksheets[0];

            ws.ExtractToDataTable(people, new ExtractToDataTableOptions("A1", ws.Rows.Count));
        }

        private void SetDataBinding()
        {
            DataTable people = (DataTable)Session["people"];
            DataView peopleDataView = people.DefaultView;

            this.GridView1.DataSource = peopleDataView;
            peopleDataView.AllowDelete = true;
            this.GridView1.DataBind();
        }

        //protected void gridview1_rowdeleting(object sender, gridviewdeleteeventargs e)
        //{
        //    datatable people = (datatable)session["people"];

        //    people.rows[e.rowindex].delete();
        //    this.setdatabinding();
        //}

        //protected void gridview1_rowediting(object sender, gridviewediteventargs e)
        //{
        //    this.gridview1.editindex = e.neweditindex;
        //    this.setdatabinding();
        //}

        //protected void gridview1_rowupdating(object sender, gridviewupdateeventargs e)
        //{
        //    int i;
        //    int rowindex = e.rowindex;
        //    datatable people = (datatable)session["people"];

        //    for (i = 1; i <= people.columns.count; i++)
        //    {
        //        textbox edittextbox = this.gridview1.rows[rowindex].cells[i].controls[0] as textbox;

        //        if (edittextbox != null)
        //            people.rows[rowindex][i - 1] = edittextbox.text;
        //    }

        //    this.gridview1.editindex = -1;
        //    this.setdatabinding();
        //}

        //protected void gridview1_rowcancelingedit(object sender, gridviewcancelediteventargs e)
        //{
        //    this.gridview1.editindex = -1;
        //    this.setdatabinding();
        //}
    }

}