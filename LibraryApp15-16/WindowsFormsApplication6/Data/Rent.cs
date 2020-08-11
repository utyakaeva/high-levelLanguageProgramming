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
    public class Rent
    {
        static string FilePath { get { return Helpers.GetDataFolder("rents.xml"); } }

        public static List<Rent> Data { get; protected set; }

        static Rent()
        {
            Data = new List<Rent>();

            //если файл существует, то прочитаем его, в противном случае вернем пустой список
            if (File.Exists(FilePath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Rent>));
                using (var stream = new FileStream(FilePath, FileMode.Open))
                {
                    Data = xs.Deserialize(stream) as List<Rent>;
                }
            }
        }

        public static List<Rent> VisibleRents
        {
            get
            {
                return Data.FindAll(rent => !rent.IsDeleted);
            }
        }

        public Rent()
        {
            Id = GenerateId();
            CreateDate = DateTime.Now;
            BookIds = new List<int>();
        }

        //создаем новый идентификатор проката
        private int GenerateId()
        {
            int maxId = 1;
            //выберем максимальный идентификатор из уже существующих
            foreach (var entity in Data)
                maxId = Math.Max(maxId, entity.Id);
            //прибавим 1 и вернем полученный номер 
            return ++maxId;
        }

        public int Id { get; set; }
        public int? ReaderId { get; set; }
        public int? LibrarianId { get; set; }
        public bool Guaranteed { get; set; } //с залогом
        public string GuaranteeSum { get; set; } //залог
        public bool IsFinished { get; set; } //прокат сдан
        public string Comment { get; set; }//комментарий
        public DateTime CreateDate { get; set; } //дата возврата
        public List<int> BookIds { get; set; } //книги
        public bool IsDeleted { get; set; } //удален ли прокат

        public string ReaderName { get { return Reader.Data.Find(r => r.Id == this.ReaderId)?.FullName; } }
        public string LibrarianName { get { return Librarian.Data.Find(r => r.Id == this.LibrarianId)?.FullName; } }

        //Сохраняем список прокатов в файл
        public static void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Rent>));
            using (TextWriter tw = new StreamWriter(FilePath))
                xs.Serialize(tw, Data);
        }
    }
}
