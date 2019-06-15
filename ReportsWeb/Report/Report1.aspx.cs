using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceImplementDataBase;
using GemBox.Spreadsheet;

namespace ReportsWeb.Report
{
    public partial class Report1 : System.Web.UI.Page
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

               
                //people.Columns.Add("ФИО", typeof(string));
                //people.Columns.Add("Дата создания", typeof(DateTime));
                //people.Columns.Add("Изделие", typeof(string));
                //people.Columns.Add("Количество", typeof(int));
                //people.Columns.Add("Сумма", typeof(int));
                //people.Columns.Add("Статус", typeof(string));

                Session["people"] = people;
                LoadDataTable("Indents");
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
            LoadDataTableSelectPeriod();
        }

        private DataTable LoadDataTableSelectPeriod()
        {
            DateTime DateFrom = Calendar1.SelectedDate.ToLocalTime();
            DateTime DateTo = Calendar2.SelectedDate.ToLocalTime();
            string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT c.CustomerFIO, f.FabricName, " +
                "i.Amount, i.Total, i.Condition, i.DateCreate, i.DateImplement " +
                "FROM AbstractDatabase.dbo.[Indents] i " +
                "JOIN AbstractDatabase.dbo.[Customers] c " +
                "ON i.CustomerId = c.Id " +
                "JOIN AbstractDatabase.dbo.[Fabrics] f " +
                "ON f.Id = i.FabricId " +
                "WHERE i.DateCreate BETWEEN ('" + DateFrom +
                "') AND ('" + DateTo + "')";

            using (SqlConnection sqlConn = new SqlConnection(conSTR))
            using (SqlCommand cmd = new SqlCommand(query, sqlConn))
            {

                sqlConn.Open();
                DataTable people = (DataTable)Session["people"];
                people.Load(cmd.ExecuteReader());
                return people;
            }
        }

        private DataTable LoadDataTable (string table)
        {
            string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT c.CustomerFIO, f.FabricName, " +
                "i.Amount, i.Total, i.Condition, i.DateCreate, i.DateImplement " +
                "FROM AbstractDatabase.dbo.[" + table + "] i " +
                "JOIN AbstractDatabase.dbo.[Customers] c " +
                "ON i.CustomerId = c.Id " +
                "JOIN AbstractDatabase.dbo.[Fabrics] f " +
                "ON f.Id = i.FabricId ";

            using(SqlConnection sqlConn = new SqlConnection(conSTR))
            using(SqlCommand cmd = new SqlCommand(query, sqlConn))
            {

                sqlConn.Open();
                DataTable people = (DataTable)Session["people"];
                people.Load(cmd.ExecuteReader());
                return people;
            }
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

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable people = (DataTable)Session["people"];

            people.Rows[e.RowIndex].Delete();
            this.SetDataBinding();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridView1.EditIndex = e.NewEditIndex;
            this.SetDataBinding();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int i;
            int rowIndex = e.RowIndex;
            DataTable people = (DataTable)Session["people"];

            for (i = 1; i <= people.Columns.Count; i++)
            {
                TextBox editTextBox = this.GridView1.Rows[rowIndex].Cells[i].Controls[0] as TextBox;

                if (editTextBox != null)
                    people.Rows[rowIndex][i - 1] = editTextBox.Text;
            }

            this.GridView1.EditIndex = -1;
            this.SetDataBinding();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridView1.EditIndex = -1;
            this.SetDataBinding();
        }
    }
}