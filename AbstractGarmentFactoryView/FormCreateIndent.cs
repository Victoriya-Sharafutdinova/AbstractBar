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
    public partial class FormCreateIndent : Form
    {

        public FormCreateIndent()
        {
            InitializeComponent();
        }
        private void FormCreateIndent_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = APICustomer.GetRequest<List<CustomerViewModel>>("api/Customer/GetList");
                if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<FabricViewModel> listF = APICustomer.GetRequest<List<FabricViewModel>>("api/Fabric/GetList");
                if (listF != null)
                {
                    comboBoxFabric.DisplayMember = "FabricName";
                    comboBoxFabric.ValueMember = "Id";
                    comboBoxFabric.DataSource = listF;
                    comboBoxFabric.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcTotal()
        {
            if (comboBoxFabric.SelectedValue != null && !string.IsNullOrEmpty(textBoxAmount.Text))
            {
                try
                {
                    List<FabricViewModel> list = APICustomer.GetRequest<List<FabricViewModel>>("api/Fabric/GetList");
                    int id = Convert.ToInt32(comboBoxFabric.SelectedValue);
                    FabricViewModel product = list.ElementAt(id);
                    int count = Convert.ToInt32(textBoxAmount.Text);
                    textBoxTotal.Text = (count * product.Value).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxAmount_TextChanged(object sender, EventArgs e)
        {
            CalcTotal();
        }

        private void comboBoxFabric_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcTotal();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxCustomer.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFabric.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APICustomer.PostRequest<IndentBindingModel, bool>("api/Indent/UpdElement", new IndentBindingModel
                {
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    FabricId = Convert.ToInt32(comboBoxFabric.SelectedValue),
                    Amount = Convert.ToInt32(textBoxAmount.Text),
                    Total = Convert.ToInt32(textBoxTotal.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
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
