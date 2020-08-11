using System;
using System.Windows.Forms;
using WindowsFormsApplication3.Data;

namespace WindowsFormsApplication3
{
    public partial class FormBook : Form
    {
        Book source;

        public FormBook(Book source)
        {
            this.source = source;
            InitializeComponent();
        }

        //Возвращает ошибку заполнения формы (если есть)
        string GetValidationError()
        {
            int n;

            if (!int.TryParse(tbPages.Text, out n) || n <= 0)
            {
                return "В поле \"Страницы\" должно быть указано положительное число.";
            }
            if (!int.TryParse(tbYear.Text, out n) || n < 1000 || n > DateTime.Now.Year)
            {
                return $"В поле \"Год\" должно быть указано число от 1000 до {DateTime.Now.Year}.";
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

            source.nomer = rbtIsBook.Checked ? 1 : 0;
            source.Pages = int.Parse(tbPages.Text);
            source.Author = tbAuthor.Text;
            source.Year = int.Parse(tbYear.Text);
            source.Name = tbName.Text;
            source.ISBN = tbISBN.Text;
            source.Issuer = tbIssuer.Text;

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
            lblIndividualNumber.Text = $"Номер: {source.IndividualNumber}";
            rbtIsBook.Checked = source.nomer == 1; rbtIsJournal.Checked = source.nomer != 1;
            tbPages.Text = source.Pages.ToString();
            tbAuthor.Text = source.Author;
            tbYear.Text = source.Year.ToString();
            tbName.Text = source.Name;
            tbISBN.Text = source.ISBN;
            tbIssuer.Text = source.Issuer;
        }
    }
}
