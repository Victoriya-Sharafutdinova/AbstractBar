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
    public partial class FormFabric : Form
    {
        public int Id
        {
            set
            {
                id = value;
            }
        }

        private int? id;

        private List<FabricStockingViewModel> fabricStockings;

        public FormFabric()
        {
            InitializeComponent();
        }

        private void FormFabric_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    FabricViewModel view = APICustomer.GetRequest<FabricViewModel>("api/Fabric/Get/" + id.Value); 
                    if (view != null)
                    {
                        textBoxName.Text = view.FabricName;
                        textBoxPrice.Text = view.Value.ToString();
                        fabricStockings = view.FabricStocking;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else  
            {
                fabricStockings = new List<FabricStockingViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (fabricStockings != null)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = fabricStockings;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormFabricStocking();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.FabricId = id.Value;
                    }
                    fabricStockings.Add(form.Model);
                }
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var form = new FormFabricStocking();
                form.Model = fabricStockings[dataGridView1.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    fabricStockings[dataGridView1.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        fabricStockings.RemoveAt(dataGridView1.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonCh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fabricStockings == null || fabricStockings.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<FabricStockingBindingModel> fabricStockingsBM = new List<FabricStockingBindingModel>();
                for (int i = 0; i < fabricStockings.Count; ++i)
                {
                    fabricStockingsBM.Add(new FabricStockingBindingModel
                    {
                        Id = fabricStockings[i].Id,
                        FabricId = fabricStockings[i].FabricId,
                        StockingId = fabricStockings[i].StockingId,
                        Amount = fabricStockings[i].Amount
                    });
                }
                if (id.HasValue)
                {
                    APICustomer.PostRequest<FabricBindingModel, bool>("api/Fabric/UpdElement", new FabricBindingModel
                    {
                        Id = id.Value,
                        FabricName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        FabricStocking = fabricStockingsBM
                    });
                }
                else
                {
                    APICustomer.PostRequest<FabricBindingModel, bool>("api/Fabric/AddElement", new FabricBindingModel
                    {
                        FabricName = textBoxName.Text,
                        Value = Convert.ToInt32(textBoxPrice.Text),
                        FabricStocking = fabricStockingsBM
                    });
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
