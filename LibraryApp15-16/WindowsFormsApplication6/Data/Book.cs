using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsFormsApplication6.Data
{
    public class Book
    {
        static string FilePath { get { return Helpers.GetDataFolder("books.xml"); } }

        public static List<Book> Data { get; protected set; }

        static Book()
        {
            Data = new List<Book>();

            //если файл существует, то прочитаем его, в противном случае вернем пустой список
            if (File.Exists(FilePath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    Data = xs.Deserialize(stream) as List<Book>;
                }
            }
        }

        public int IndividualNumber { get; set; }
        public int nomer { get; set; }
        public int Pages { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Issuer { get; set; }

        public string BookType
        {
            get
            {
                return nomer == 0 ? "Журнал" : "Книга";
            }
        }

        public Book()
        {
            IndividualNumber = GenerateId();
            nomer = 1;
        }

        //перегрузка стандартной функции, для корректного отображения объекта в выпадающем списке
        public override string ToString()
        {
            return $"{Author}. {Name} - {Year} г. {Pages}. ISBN: {ISBN}.";
        }

        //создаем новый идентификатор книги
        private int GenerateId()
        {
            int maxId = 1;
            //выберем максимальный идентификатор из уже существующих
            foreach (var entity in Data)
                maxId = Math.Max(maxId, entity.IndividualNumber);
            //прибавим 1 и вернем полученный номер 
            return ++maxId;
        }

        //Сохраняем список книг в файл
        public static void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            using (TextWriter tw = new StreamWriter(FilePath))
                xs.Serialize(tw, Data);
        }
    }
}
