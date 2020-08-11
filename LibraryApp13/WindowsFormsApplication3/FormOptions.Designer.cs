namespace WindowsFormsApplication3
{
    partial class FormOptions
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
            this.btSave = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.cbShowNumberInHeader = new System.Windows.Forms.CheckBox();
            this.cmbLibrarians = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(229, 125);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 3;
            this.btSave.Text = "ОК";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(136, 125);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // cbShowNumberInHeader
            // 
            this.cbShowNumberInHeader.AutoSize = true;
            this.cbShowNumberInHeader.Location = new System.Drawing.Point(13, 12);
            this.cbShowNumberInHeader.Name = "cbShowNumberInHeader";
            this.cbShowNumberInHeader.Size = new System.Drawing.Size(232, 17);
            this.cbShowNumberInHeader.TabIndex = 5;
            this.cbShowNumberInHeader.Text = "Отображать номер проката в заголовке";
            this.cbShowNumberInHeader.UseVisualStyleBackColor = true;
            // 
            // cmbLibrarians
            // 
            this.cmbLibrarians.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLibrarians.FormattingEnabled = true;
            this.cmbLibrarians.Location = new System.Drawing.Point(12, 63);
            this.cmbLibrarians.Name = "cmbLibrarians";
            this.cmbLibrarians.Size = new System.Drawing.Size(305, 21);
            this.cmbLibrarians.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Библиотекарь по умолчанию";
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 157);
            this.Controls.Add(this.cmbLibrarians);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbShowNumberInHeader);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.CheckBox cbShowNumberInHeader;
        private System.Windows.Forms.ComboBox cmbLibrarians;
        private System.Windows.Forms.Label label5;
    }
}