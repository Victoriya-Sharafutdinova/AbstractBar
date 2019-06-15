using System;
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
using System.Data.Entity.SqlServer;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.util;

namespace ReportsWeb.Report
{
    public partial class Report1 : System.Web.UI.Page
    {
       
        private AbstractDBScope context = new AbstractDBScope();
        private IReportService reportService;
        protected void Page_Load(object sender, EventArgs e)
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");           
            FontSettings.FontsBaseDirectory = Server.MapPath("Fonts/");

            if (!Page.IsPostBack)
            {               
                DataTable people = new DataTable();

                Session["people"] = people;
                LoadDataTable("Indents");
                this.SetDataBinding();
            }
        }


        public List<CustomerIndentsModel> GetCustomerIndents(ReportBindingModel model)
        {
            return context.Indents
                .Include(rec => rec.Customer)
                .Include(rec => rec.Fabric)
                .Where(rec => rec.DateCreate >= model.DateFrom &&
                rec.DateCreate <= model.DateTo).Select(rec => new CustomerIndentsModel
                {
                    CustomerName = rec.Customer.CustomerFIO,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                    SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                    SqlFunctions.DateName("yyyy", rec.DateCreate),
                    FabricName = rec.Fabric.FabricName,
                    Amount = rec.Amount,
                    Total = rec.Total,
                    Condition = rec.Condition.ToString()
                })
                    .ToList();
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
            ef.Save(this.Response, "Report.pdf");
            reportService.SaveCustomerIndents(new ReportBindingModel
            {

                DateFrom = Calendar1.SelectedDate,
                DateTo = Calendar2.SelectedDate,
                FileName = "Report.pdf"
            });
        }


        protected void SelectPeriod_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = null;

            LoadDataTableSelectPeriod();
            DataTable people = LoadDataTableSelectPeriod();
            DataView peopleDataView = people.DefaultView;

            this.GridView1.DataSource = peopleDataView;
            peopleDataView.AllowDelete = true;
            this.GridView1.DataBind();
        }

        private DataTable LoadDataTableSelectPeriod()
        {
            DateTime DateFrom = Calendar1.SelectedDate;
            DateTime DateTo = Calendar2.SelectedDate;
            string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT c.CustomerFIO, f.FabricName, " +
                "i.Amount, i.Total, i.Condition, i.DateCreate, i.DateImplement " +
                "FROM AbstractDatabase.dbo.[Indents] i " +
                "JOIN AbstractDatabase.dbo.[Customers] c " +
                "ON i.CustomerId = c.Id " +
                "JOIN AbstractDatabase.dbo.[Fabrics] f " +
                "ON f.Id = i.FabricId " +
                "WHERE i.DateCreate BETWEEN N'" + DateFrom.ToString("yyyy-MM-dd HH:mm:ss") + "' AND N'" + DateTo.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            using (SqlConnection sqlConn = new SqlConnection(conSTR))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {

                sqlConn.Open();
                DataTable people = new DataTable();
                people.Load(cmd.ExecuteReader());
                return people;
            }
        }

        private DataTable LoadDataTable(string table)
        {
            string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT c.CustomerFIO, f.FabricName, " +
                "i.Amount, i.Total, i.Condition, i.DateCreate, i.DateImplement " +
                "FROM AbstractDatabase.dbo.[" + table + "] i " +
                "JOIN AbstractDatabase.dbo.[Customers] c " +
                "ON i.CustomerId = c.Id " +
                "JOIN AbstractDatabase.dbo.[Fabrics] f " +
                "ON f.Id = i.FabricId ";

            using (SqlConnection sqlConn = new SqlConnection(conSTR))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {

                sqlConn.Open();
                DataTable people = (DataTable)Session["people"];
                people.Load(cmd.ExecuteReader());
                return people;
            }
        }

        private void SetDataBinding()
        {
            DataTable people = (DataTable)Session["people"];
            DataView peopleDataView = people.DefaultView;

            this.GridView1.DataSource = peopleDataView;
            peopleDataView.AllowDelete = true;
            this.GridView1.DataBind();
        }

    }
}