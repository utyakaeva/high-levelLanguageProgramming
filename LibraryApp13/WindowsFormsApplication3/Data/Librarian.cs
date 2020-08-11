using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3.Data
{
    public class Librarian
    {
        static List<Librarian> data = null;
        public static List<Librarian> Data
        {
            get
            {
                //При первом обращении к списку библиотекарей загружаем его из файла
                if (data == null)
                {
                    data = new List<Librarian>();
                    //считываем из файла по одной строчке
                    foreach (string s in File.ReadAllLines("librarians.txt"))
                    {
                        var ts = s.Split(',');
                        //создаем новый экземпляр библиотекаря
                        data.Add(new Librarian() { Id = int.Parse(ts[0].Trim()), FullName = ts[1].Trim() });
                    }
                }
                return data;
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
            using (var writer = new StreamWriter("librarians.txt"))
            {
                foreach (var librarian in Data)
                {
                    var line = $"{librarian.Id}, {librarian.FullName}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}
