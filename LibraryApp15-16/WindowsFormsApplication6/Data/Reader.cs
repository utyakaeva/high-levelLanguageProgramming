using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApplication6.Data
{
    public class Reader
    {
        static string FilePath { get { return Helpers.GetDataFolder("lll.xml"); } }

        public static List<Reader> Data { get; protected set; }

        static Reader()
        {
            Data = new List<Reader>();

            //если файл существует, то прочитаем его, в противном случае вернем пустой список
            if (File.Exists(FilePath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Reader>));
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    Data = xs.Deserialize(stream) as List<Reader>;
                }
            }
        }

        public int Id { get; set; }
        public string FullName { get; set; }

        public Reader()
        {
            Id = GenerateId();
        }

        //перегрузка стандартной функции, для корректного отображения объекта в выпадающем списке
        public override string ToString()
        {
            return FullName;
        }

        //создаем новый идентификатор читателя
        private int GenerateId()
        {
            int maxId = 1;
            //выберем максимальный идентификатор из уже существующих
            foreach (var entity in Data)
                maxId = Math.Max(maxId, entity.Id);
            //прибавим 1 и вернем полученный номер 
            return ++maxId;
        }

        //Сохраняем список читателей в файл
        public static void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Reader>));
            using (TextWriter tw = new StreamWriter(FilePath))
                xs.Serialize(tw, Data);
        }
    }
}
