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

     /*   public void SaveCustomerIndents(ReportBindingModel model)
        {
            //из ресрусов получаем шрифт для кирилицы      
            if (!File.Exists("TIMCYR.TTF"))
            {
                File.WriteAllBytes("TIMCYR.TTF", Properties.Resources.TIMCYR);
            }
            //открываем файл для работы  
            FileStream fs = new FileStream(model.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            //создаем документ, задаем границы, связываем документ и поток 
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            doc.SetMargins(0.5f, 0.5f, 0.5f, 0.5f);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();
            BaseFont baseFont = BaseFont.CreateFont("TIMCYR.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            //вставляем заголовок   
            var phraseTitle = new Phrase("Заказы клиентов",
            new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD));
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(phraseTitle)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            var phrasePeriod = new Phrase("c " + model.DateFrom.Value.ToShortDateString() + " по "
                + model.DateTo.Value.ToShortDateString(),
                new iTextSharp.text.Font(baseFont, 14, iTextSharp.text.Font.BOLD));
            paragraph = new iTextSharp.text.Paragraph(phrasePeriod)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 12
            };
            doc.Add(paragraph);

            //вставляем таблицу, задаем количество столбцов, и ширину колонок   
            PdfPTable table = new PdfPTable(6)
            {
                TotalWidth = 800F
            };
            table.SetTotalWidth(new float[]
            {
                160, 140, 160, 100, 100, 140
            });
            //вставляем шапку     
            PdfPCell cell = new PdfPCell();
            var fontForCellBold = new iTextSharp.text.Font(baseFont, 10, iTextSharp.text.Font.BOLD);
            table.AddCell(new PdfPCell(new Phrase("ФИО клиента", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Дата создания", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Изделие", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Количество", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Сумма", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            table.AddCell(new PdfPCell(new Phrase("Статус", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_CENTER
            });
            //заполняем таблицу          
            var list = GetCustomerIndents(model);
            var fontForCells = new iTextSharp.text.Font(baseFont, 10);
            for (int i = 0; i < list.Count; i++)
            {
                cell = new PdfPCell(new Phrase(list[i].CustomerName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].DateCreate, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].FabricName, fontForCells));
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Amount.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Total.ToString(), fontForCells));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT; table.AddCell(cell);
                cell = new PdfPCell(new Phrase(list[i].Condition, fontForCells));
                table.AddCell(cell);
            }
            //вставляем итого      
            cell = new PdfPCell(new Phrase("Итого:", fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Colspan = 4,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(list.Sum(rec => rec.Total)
                .ToString(), fontForCellBold))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0
            };
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase("", fontForCellBold))
            {
                Border = 0
            };
            table.AddCell(cell);
            //вставляем таблицу          
            doc.Add(table);

            doc.Close();
        }*/

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
                DateFrom = new DateTime(2018, 1, 1),
                DateTo = DateTime.Now,
                //DateFrom = Calendar1.SelectedDate,
                //DateTo = Calendar2.SelectedDate,
                FileName = "Report.pdf"
            });
        }


        protected void SelectPeriod_Click(object sender, EventArgs e)
        {
            LoadDataTableSelectPeriod();
        }

        private DataTable LoadDataTableSelectPeriod()
        {
            //DateTime DateFrom = Calendar1.SelectedDate.ToLocalTime();
            //DateTime DateTo = Calendar2.SelectedDate.ToLocalTime();
            string conSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ВИКА\\AbstractDatabase.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT c.CustomerFIO, f.FabricName, " +
                "i.Amount, i.Total, i.Condition, i.DateCreate, i.DateImplement " +
                "FROM AbstractDatabase.dbo.[Indents] i " +
                "JOIN AbstractDatabase.dbo.[Customers] c " +
                "ON i.CustomerId = c.Id " +
                "JOIN AbstractDatabase.dbo.[Fabrics] f " +
                "ON f.Id = i.FabricId "/* +
                "WHERE i.DateCreate BETWEEN 24.05.2019 00:00:00 AND 12.06.2019 00:00:00"*/;

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

        //protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    DataTable people = (DataTable)Session["people"];

        //    people.Rows[e.RowIndex].Delete();
        //    this.SetDataBinding();
        //}

        //protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    this.GridView1.EditIndex = e.NewEditIndex;
        //    this.SetDataBinding();
        //}

        //protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int i;
        //    int rowIndex = e.RowIndex;
        //    DataTable people = (DataTable)Session["people"];

        //    for (i = 1; i <= people.Columns.Count; i++)
        //    {
        //        TextBox editTextBox = this.GridView1.Rows[rowIndex].Cells[i].Controls[0] as TextBox;

        //        if (editTextBox != null)
        //            people.Rows[rowIndex][i - 1] = editTextBox.Text;
        //    }

        //    this.GridView1.EditIndex = -1;
        //    this.SetDataBinding();
        //}

        //protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    this.GridView1.EditIndex = -1;
        //    this.SetDataBinding();
        //}
    }
}