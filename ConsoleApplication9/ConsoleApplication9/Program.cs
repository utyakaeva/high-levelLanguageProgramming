using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Book
    {
        public int PAGE;
        public string author;
        public int year;
        public string name;
        public string ISBN;
        public string publish;
        public Book(int a, string b, int c, string d, string q, string j) // инициализируем поля класса 
        {
            PAGE = a;
            author = b;
            year = c;
            name = d;
            ISBN = q;
            publish = j;
        }
    }

    class Books
    {
       
        public List<Book> allBooks;

        public Books()//конструктор 
        {
            allBooks = new List<Book>();
            allBooks = readBooks();
        }


        public List<Book> readBooks()//функция чтобы считать все книги из файла 
        {
            List<Book> spisok = new List<Book>();
            foreach (string line in File.ReadLines("fvf.txt", Encoding.Default))
            {
                string[] pr = line.Split(new string[] { "," }, StringSplitOptions.None);
                int r = Convert.ToInt32(pr[0]);
                int p = Convert.ToInt32(pr[2]);
                
                Book temp = new Book(r, pr[1], p, pr[3], pr[4], pr[5]);
                spisok.Add(temp);
            }
            return spisok;
        }
        public void Poisk() // создаем функцию для поиска по автору 
        {
            Console.WriteLine("Ищем сведения о книге:\n1)ПО АВТОРУ\n2)ПО НАЗВАНИЮ\n3)ПО ГОДУ ");
            int my = 0;
            while ((my < 1) || (my > 3)) //выполняем цикл до тех пор, пока не будет введено возможное значение 
            {
                my = Convert.ToInt32(Console.ReadLine());
            }
            if (my == 1) // если поиск по автору, то переменной присваиваем значение 
            {
                Console.WriteLine("Введите автора:");
                string pattern = Console.ReadLine();
                findByauthor(pattern);
            }

            if (my == 2)//если поиск по названию, то переменной присваиваем значение 

            {
                Console.WriteLine("Введите название:");
                string pattern = Console.ReadLine();
                findByname(pattern);
            }

            if (my == 3)
            {
                Console.WriteLine("Введите год издания:");
                int pattern = Convert.ToInt32(Console.ReadLine());
                findByyear(pattern);
            }

        }

        public void findByauthor(string str) //поиск в списке по фамилии 
        {
            
            
                for (int i = 0; i < allBooks.Count; i++)
                {
                    bool f = allBooks[i].author.Contains(str);
                    if (f) // сравниваем очередой элемент массива с переменной и если они равны, то выводим сведения о книге 
                    {
                        Console.Write("{0,-10}", allBooks[i].PAGE);
                        Console.Write("{0,-10}", allBooks[i].author);
                        Console.Write("{0,-10}", allBooks[i].year);
                        Console.Write("{0,-10}", allBooks[i].name);
                        Console.Write("{0,-10}", allBooks[i].ISBN);
                        Console.Write("{0,-10}", allBooks[i].publish);
                        Console.WriteLine();
                    }
                }
            
        }
        public void findByname(string q)
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                bool l = allBooks[i].name.Contains(q);

                if (l)
                {
                    Console.Write("{0,-10}", allBooks[i].PAGE);
                    Console.Write("{0,-10}", allBooks[i].author);
                    Console.Write("{0,-10}", allBooks[i].year);
                    Console.Write("{0,-10}", allBooks[i].name);
                    Console.Write("{0,-10}", allBooks[i].ISBN);
                    Console.Write("{0,-10}", allBooks[i].publish);
                    Console.WriteLine();
                }
            }

        }

        public void findByyear(int year) //поиск по ID 
        {

            for (int i = 0; i < allBooks.Count; i++)

                if (allBooks[i].year == year) // сравниваем очередой элемент массива с переменной и если они равны, то выводим сведения о книге 
                {
                    Console.Write("{0,-10}", allBooks[i].PAGE);
                    Console.Write("{0,-10}", allBooks[i].author);
                    Console.Write("{0,-10}", allBooks[i].year);
                    Console.Write("{0,-10}", allBooks[i].name);
                    Console.Write("{0,-10}", allBooks[i].ISBN);
                    Console.Write("{0,-10}", allBooks[i].publish);
                    Console.WriteLine();
                }

        }

        public void savePersons() //функция для записи всех книг в файл 
        {
            StreamWriter afile = new StreamWriter("fvf.txt", false, Encoding.Default);
            foreach (Book p in allBooks)
            {
                afile.WriteLine("{0},{1},{2},{3},{4},{5}", p.PAGE, p.author, p.year, p.name, p.ISBN, p.publish);
            }
            afile.Close();
        }



        public void katalog() //создаем функцию для создания каталога книг 
        {
            for (int i = 0; i < allBooks.Count; i++)
            {
                Console.Write("{0,-10}", allBooks[i].PAGE);
                Console.Write("{0,-10}", allBooks[i].author);
                Console.Write("{0,-10}", allBooks[i].year);
                Console.Write("{0,-10}", allBooks[i].name);
                Console.Write("{0,-10}", allBooks[i].ISBN);
                Console.Write("{0,-10}", allBooks[i].publish);
                Console.WriteLine();
            }
        }


        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 30); // задаем размер консоли 


            Books books = new Books();
            begin:
            int c = 0;
            Console.WriteLine("Выберите действие, которое хотите выполнить:\n1)Каталог книг\n2Поиск книг по названию,по автору, по году издания");
            while ((c < 1) || (c > 2)) //выполняем цикл до тех пор, пока не будет введено возможное значение 
            {
                c = Convert.ToInt32(Console.ReadLine());
            }
            if (c == 1) books.katalog(); // если выбрали первый пункт, то составляем каталог 
            if (c == 2) books.Poisk(); // если выбрали второй пункт, то выпоняется поиск 
            Console.WriteLine();
            Console.WriteLine("Выйти?");
            string exit = Console.ReadLine();
            if (exit.ToLower() != "yes") goto begin;
        }
    }
}

