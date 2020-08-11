using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WindowsFormsApplication6.Data
{
    public class Settings
    {
        static string FilePath { get { return "settings.xml"; } }

        static Settings instance = null;
        public static Settings Instance
        {
            get
            {
                //При первом обращении к настройкам загружаем их из файла
                if (instance == null)
                {
                    instance = new Settings();

                    //если файл существует, то прочитаем его, в противном случае вернем пустой список
                    if (File.Exists(FilePath))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(Settings));
                        using (var stream = new FileStream(FilePath, FileMode.Open))
                        {
                            instance = xs.Deserialize(stream) as Settings;
                        }
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
            XmlSerializer xs = new XmlSerializer(typeof(Settings));
            TextWriter tw = new StreamWriter(FilePath);
            xs.Serialize(tw, Instance);
        }
    }
}
