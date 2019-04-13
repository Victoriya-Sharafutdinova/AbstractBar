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
    public partial class FormStocking : Form
    {

        public int Id { set { id = value; } }

        private int? id;

        public FormStocking()
        {
            InitializeComponent();
        }

        private void FormStocking_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StockingViewModel stocking = APICustomer.GetRequest<StockingViewModel>("api/Stocking/Get/" + id.Value);
                    if (stocking != null)
                    {
                        textBoxName.Text = stocking.StockingName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APICustomer.PostRequest<StockingBindingModel, bool>("api/Stocking/UpdElement", new StockingBindingModel
                    {
                        Id = id.Value,
                        StockingName = textBoxName.Text
                    });
                }
                else
                {
                    APICustomer.PostRequest<StockingBindingModel, bool>("api/Stocking/UpdElement", new StockingBindingModel
                    {
                        StockingName = textBoxName.Text
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
