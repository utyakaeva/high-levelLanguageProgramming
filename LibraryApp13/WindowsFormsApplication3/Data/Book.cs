using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3.Data
{
    public class Book
    {
        static List<Book> data = null;
        public static List<Book> Data
        {
            get
            {
                //При первом обращении к списку читателей загружаем его из файла
                if (data == null)
                {
                    data = new List<Book>();

                    //считываем из файла с книгами по одной строчке
                    foreach (string s in File.ReadAllLines("books.txt"))
                    {
                        var ts = s.Split(',');
                        //создаем новый экземпляр книги
                        data.Add(new Book()
                        {
                            IndividualNumber = int.Parse(ts[0]),
                            nomer = int.Parse(ts[1]),
                            Pages = int.Parse(ts[2]),
                            Author = ts[3].Trim(),
                            Year = int.Parse(ts[4]),
                            Name = ts[5].Trim(),
                            ISBN = ts[6].Trim(),
                            Issuer = ts[7].Trim()
                        });
                    }
                }
                return data;
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
            using (var writer = new StreamWriter("books.txt"))
            {
                foreach (var book in Data)
                {
                    var line = $"{book.IndividualNumber}, {book.nomer}, {book.Pages}, {book.Author}, {book.Year}, {book.Name}, {book.ISBN}, {book.Issuer}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}
