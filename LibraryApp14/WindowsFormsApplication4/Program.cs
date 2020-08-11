using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication4.Data;

namespace WindowsFormsApplication4
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public static class Helpers
    {
        public static int? ParseNullableInt(string s)
        {
            int n;
            if (int.TryParse(s, out n))
                return n;
            else
                return null;
        }

        public static string GetDataFolder(string fileName)
        {
            if (Settings.Instance.DataFolder == null)
                Settings.Instance.DataFolder = "";
            if (!string.IsNullOrEmpty(Settings.Instance.DataFolder) && !Directory.Exists(Settings.Instance.DataFolder))
            {
                MessageBox.Show($"Каталог для хранения данных ({Settings.Instance.DataFolder}) не найден. Данные будут сохраняться в папку программы.");
                Settings.Instance.DataFolder = "";
                Settings.Save();
            }
            return Path.Combine(Settings.Instance.DataFolder, fileName);
        }
    }
}
