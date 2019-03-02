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
using Unity;

namespace AbstractGarmentFactoryView
{
    public partial class FormCreateIndent : Form
    {
        [Dependency] public new IUnityContainer Container { get; set; }

        private readonly ICustomerService serviceC;

        private readonly IFabricService serviceP;

        private readonly IMainService serviceM;

        public FormCreateIndent(ICustomerService serviceC, IFabricService serviceP, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;
        }
        private void FormCreateIndent_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = serviceC.GetList(); if (listC != null)
                {
                    comboBoxCustomer.DisplayMember = "CustomerFIO";
                    comboBoxCustomer.ValueMember = "Id";
                    comboBoxCustomer.DataSource = listC;
                    comboBoxCustomer.SelectedItem = null;
                }
                List<FabricViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBoxFabric.DisplayMember = "FabricName";
                    comboBoxFabric.ValueMember = "Id";
                    comboBoxFabric.DataSource = listP;
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
                    int id = Convert.ToInt32(comboBoxFabric.SelectedValue);
                    FabricViewModel product = serviceP.GetElement(id);
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
                serviceM.CreateIndent(new IndentBindingModel
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
