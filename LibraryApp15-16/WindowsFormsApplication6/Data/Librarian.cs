using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApplication6.Data
{
    public class Librarian
    {
        static string FilePath { get { return Helpers.GetDataFolder("librarians.xml"); } }

        public static List<Librarian> Data { get; protected set; }

        static Librarian()
        {
            Data = new List<Librarian>();

            //если файл существует, то прочитаем его, в противном случае вернем пустой список
            if (File.Exists(FilePath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Librarian>));
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    Data = xs.Deserialize(stream) as List<Librarian>;
                }
            }
        }

        public int Id { get; set; }
        public string FullName { get; set; }

        public Librarian()
        {
            Id = GenerateId();
        }

        //перегрузка стандартной функции, для корректного отображения объекта в выпадающем списке
        public override string ToString()
        {
            return FullName;
        }

        //создаем новый идентификатор библиотекаря
        private int GenerateId()
        {
            int maxId = 1;
            //выберем максимальный идентификатор из уже существующих
            foreach (var entity in Data)
                maxId = Math.Max(maxId, entity.Id);
            //прибавим 1 и вернем полученный номер 
            return ++maxId;
        }

        //Сохраняем список библиотекарей в файл
        public static void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Librarian>));
            using (TextWriter tw = new StreamWriter(FilePath))
                xs.Serialize(tw, Data);
        }
    }
}
