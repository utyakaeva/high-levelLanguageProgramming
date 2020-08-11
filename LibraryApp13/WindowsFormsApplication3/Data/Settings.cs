using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication3.Data
{
    public class Settings
    {
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
                        if (File.Exists("settings.txt"))
                        {
                            string[] lines = File.ReadAllLines("settings.txt");
                            instance.ShowNumberInHeader = bool.Parse(lines[0]);
                            instance.DefaultLibrarianId = lines.Length > 1 ? Helpers.ParseNullableInt(lines[1]) : null;
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

        public static void Save()
        {
            File.WriteAllLines("settings.txt", new string[]
            {
                instance.ShowNumberInHeader.ToString(),
                instance.DefaultLibrarianId.HasValue ? instance.DefaultLibrarianId.Value.ToString() : string.Empty
            });
        }
    }
}
