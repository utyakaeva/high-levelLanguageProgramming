namespace WindowsFormsApplication3
{
    partial class FormBook
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
            this.rbtIsBook = new System.Windows.Forms.RadioButton();
            this.rbtIsJournal = new System.Windows.Forms.RadioButton();
            this.lblIndividualNumber = new System.Windows.Forms.Label();
            this.tbPages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbISBN = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIssuer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(265, 212);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 3;
            this.btSave.Text = "ОК";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(172, 212);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 4;
            this.btCancel.Text = "Отмена";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // rbtIsBook
            // 
            this.rbtIsBook.AutoSize = true;
            this.rbtIsBook.Location = new System.Drawing.Point(12, 46);
            this.rbtIsBook.Name = "rbtIsBook";
            this.rbtIsBook.Size = new System.Drawing.Size(55, 17);
            this.rbtIsBook.TabIndex = 5;
            this.rbtIsBook.TabStop = true;
            this.rbtIsBook.Text = "Книга";
            this.rbtIsBook.UseVisualStyleBackColor = true;
            // 
            // rbtIsJournal
            // 
            this.rbtIsJournal.AutoSize = true;
            this.rbtIsJournal.Location = new System.Drawing.Point(73, 46);
            this.rbtIsJournal.Name = "rbtIsJournal";
            this.rbtIsJournal.Size = new System.Drawing.Size(65, 17);
            this.rbtIsJournal.TabIndex = 6;
            this.rbtIsJournal.TabStop = true;
            this.rbtIsJournal.Text = "Журнал";
            this.rbtIsJournal.UseVisualStyleBackColor = true;
            // 
            // lblIndividualNumber
            // 
            this.lblIndividualNumber.AutoSize = true;
            this.lblIndividualNumber.Location = new System.Drawing.Point(9, 11);
            this.lblIndividualNumber.Name = "lblIndividualNumber";
            this.lblIndividualNumber.Size = new System.Drawing.Size(35, 13);
            this.lblIndividualNumber.TabIndex = 7;
            this.lblIndividualNumber.Text = "label1";
            // 
            // tbPages
            // 
            this.tbPages.Location = new System.Drawing.Point(12, 99);
            this.tbPages.Name = "tbPages";
            this.tbPages.Size = new System.Drawing.Size(156, 20);
            this.tbPages.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Страниц";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Автор";
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(12, 156);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(156, 20);
            this.tbAuthor.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Год";
            // 
            // tbYear
            // 
            this.tbYear.Location = new System.Drawing.Point(190, 24);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(156, 20);
            this.tbYear.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Название";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(190, 64);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(156, 20);
            this.tbName.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "ISBN";
            // 
            // tbISBN
            // 
            this.tbISBN.Location = new System.Drawing.Point(190, 110);
            this.tbISBN.Name = "tbISBN";
            this.tbISBN.Size = new System.Drawing.Size(156, 20);
            this.tbISBN.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Издатель";
            // 
            // tbIssuer
            // 
            this.tbIssuer.Location = new System.Drawing.Point(190, 156);
            this.tbIssuer.Name = "tbIssuer";
            this.tbIssuer.Size = new System.Drawing.Size(156, 20);
            this.tbIssuer.TabIndex = 18;
            // 
            // FormBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 242);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbIssuer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbISBN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAuthor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPages);
            this.Controls.Add(this.lblIndividualNumber);
            this.Controls.Add(this.rbtIsJournal);
            this.Controls.Add(this.rbtIsBook);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBook";
            this.Text = "Книга";
            this.Load += new System.EventHandler(this.FormBook_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.RadioButton rbtIsBook;
        private System.Windows.Forms.RadioButton rbtIsJournal;
        private System.Windows.Forms.Label lblIndividualNumber;
        private System.Windows.Forms.TextBox tbPages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbISBN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbIssuer;
    }
}