using System;
using System.Windows.Forms;
using WindowsFormsApplication3.Data;

namespace WindowsFormsApplication3
{
    public partial class FormLibrarian : Form
    {
        Librarian source;

        public FormLibrarian(Librarian source)
        {
            this.source = source;
            InitializeComponent();
        }

        //Возвращает ошибку заполнения формы (если есть)
        string GetValidationError()
        {
            if (string.IsNullOrEmpty(tbFullName.Text))
            {
                return "В поле \"ФИО\" не может быть пустая строка.";
            }

            return null;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            var error = GetValidationError();
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            source.FullName = tbFullName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            lblIndividualNumber.Text = $"Номер: {source.Id}";
            tbFullName.Text = source.FullName;
        }
    }
}
