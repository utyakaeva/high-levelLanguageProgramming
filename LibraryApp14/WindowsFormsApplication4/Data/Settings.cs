using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication4.Data
{
    public class Settings
    {
        static string FilePath { get { return "settings.txt"; } }

        static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                //При первом обращении к настройкам загружаем их из файла
                if (instance == null)
                {
                    instance = new Settings();

                    try
                    {
                        //если файл существует, то прочитаем его, в противном случае вернем пустой список
                        if (File.Exists(FilePath))
                        {
                            string[] lines = File.ReadAllLines(FilePath);
                            instance.ShowNumberInHeader = bool.Parse(lines[0]);
                            instance.DefaultLibrarianId = lines.Length > 1 ? Helpers.ParseNullableInt(lines[1]) : null;
                            instance.DataFolder = lines.Length > 2 ? lines[2] : null;
                        }
                    } catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        MessageBox.Show($"Ошибка загрузки настроек. {ex}");
                    }
                }
                return instance;
            }
        }

        public Settings()
        {
            ShowNumberInHeader = true;
        }

        public bool ShowNumberInHeader { get; set; }
        public int? DefaultLibrarianId { get; set; }
        public string DataFolder { get; set; }

        public static void Save()
        {
            File.WriteAllLines(FilePath, new string[]
            {
                instance.ShowNumberInHeader.ToString(),
                instance.DefaultLibrarianId.HasValue ? instance.DefaultLibrarianId.Value.ToString() : string.Empty,
                instance.DataFolder
            });
        }
    }
}
