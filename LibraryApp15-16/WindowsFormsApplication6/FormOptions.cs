using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication6.Data;

namespace WindowsFormsApplication6
{
    public partial class FormOptions : Form
    {
        public FormOptions()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Settings.Instance.ShowNumberInHeader = cbShowNumberInHeader.Checked;
            Settings.Instance.DefaultLibrarianId = (cmbLibrarians.SelectedItem as Librarian)?.Id;
            Settings.Instance.DataFolder = tbDataFolder.Text;

            Settings.Save();
            Book.Save();
            Librarian.Save();
            Reader.Save();
            Rent.Save();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            cbShowNumberInHeader.Checked = Settings.Instance.ShowNumberInHeader;

            cmbLibrarians.Items.AddRange(Librarian.Data.ToArray());
            var selectedLibrarian = Librarian.Data.Find(r => r.Id == Settings.Instance.DefaultLibrarianId);
            cmbLibrarians.SelectedItem = selectedLibrarian;

            tbDataFolder.Text = Settings.Instance.DataFolder;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbDataFolder.Text = dlg.SelectedPath;
                }
            }
        }
    }
}
