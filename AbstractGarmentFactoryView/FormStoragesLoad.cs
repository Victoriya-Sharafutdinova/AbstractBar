using AbstractGarmentFactoryServiceDAL.BindingModel;
using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AbstractGarmentFactoryView
{
    public partial class FormStoragesLoad : Form
    {

        public FormStoragesLoad()
        {
            InitializeComponent();
        }

        private void FormStoragesLoad_Load(object sender, EventArgs e)
        {
            try
            {
                List<StorageLoadViewModel> list = APICustomer.GetRequest<List<StorageLoadViewModel>>("api/Storage/GetList");
                if (list != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var elem in list)
                    {
                        dataGridView.Rows.Add(new object[] 
                        {
                            elem.StorageName, "", ""
                        });
                        foreach (var listElem in elem.Stockings)
                        {
                            dataGridView.Rows.Add(new object[]
                            {
                                "", listElem.Item1, listElem.Item2
                            });
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", elem.TotalAmount });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "xls|*.xls|xlsx|*.xlsx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APICustomer.PostRequest<ReportBindingModel, bool>("api/Report/SaveClientOrders", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
