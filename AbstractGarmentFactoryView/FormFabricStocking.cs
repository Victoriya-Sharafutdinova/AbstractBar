using AbstractGarmentFactoryServiceDAL.Interfaces;
using AbstractGarmentFactoryServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractGarmentFactoryView
{
    public partial class FormFabricStocking : Form
    {
        [Dependency] public new IUnityContainer Container { get; set; }

        public FabricStockingViewModel Model
        {
            set { model = value; }
            get { return model; }
        }

        private readonly IStockingService service;

        private FabricStockingViewModel model;

        public FormFabricStocking(IStockingService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormProductComponent_Load(object sender, EventArgs e)
        {
            try
            {
                List<StockingViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxStocking.DisplayMember = "StockingName";
                    comboBoxStocking.ValueMember = "Id";
                    comboBoxStocking.DataSource = list;
                    comboBoxStocking.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxStocking.Enabled = false;
                comboBoxStocking.SelectedValue = model.StockingId;
                textBoxAmount.Text = model.Amount.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAmount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStocking.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new FabricStockingViewModel
                    {
                        StockingId = Convert.ToInt32(comboBoxStocking.SelectedValue),
                        StockingName = comboBoxStocking.Text,
                        Amount = Convert.ToInt32(textBoxAmount.Text)
                    };
                }
                else
                {
                    model.Amount = Convert.ToInt32(textBoxAmount.Text);
                }
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
