﻿namespace AbstractGarmentFactoryView
{
    partial class FormCreateIndent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            this.comboBoxFabric = new System.Windows.Forms.ComboBox();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.textBoxTotal = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Изделие";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Сумма";
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(145, 7);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(291, 24);
            this.comboBoxCustomer.TabIndex = 4;
            // 
            // comboBoxFabric
            // 
            this.comboBoxFabric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFabric.FormattingEnabled = true;
            this.comboBoxFabric.Location = new System.Drawing.Point(145, 44);
            this.comboBoxFabric.Name = "comboBoxFabric";
            this.comboBoxFabric.Size = new System.Drawing.Size(288, 24);
            this.comboBoxFabric.TabIndex = 5;
            this.comboBoxFabric.SelectedIndexChanged += new System.EventHandler(this.comboBoxFabric_SelectedIndexChanged);
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(145, 80);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(290, 22);
            this.textBoxAmount.TabIndex = 6;
            this.textBoxAmount.TextChanged += new System.EventHandler(this.textBoxAmount_TextChanged);
            // 
            // textBoxTotal
            // 
            this.textBoxTotal.Location = new System.Drawing.Point(145, 116);
            this.textBoxTotal.Name = "textBoxTotal";
            this.textBoxTotal.Size = new System.Drawing.Size(289, 22);
            this.textBoxTotal.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(172, 164);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(123, 29);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(316, 163);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(119, 29);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormCreateIndent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 262);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxTotal);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.comboBoxFabric);
            this.Controls.Add(this.comboBoxCustomer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormCreateIndent";
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateIndent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
        private System.Windows.Forms.ComboBox comboBoxFabric;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.TextBox textBoxTotal;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}