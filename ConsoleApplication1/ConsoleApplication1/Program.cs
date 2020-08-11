using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Bus
    {
        public string nomer_bus;
        public string FIO;
        public string bus_route;
        public string location;
        public Bus(string a, string b, string c, string d) // инициализируем поля класса
        {
            nomer_bus = a;
            FIO = b;
            bus_route = c;
            location = d;
        }
        public int Read() // создаем метод для подсчета количесвта строк
        {
            StreamReader kol = new StreamReader("Автобусы.txt", Encoding.Default);
            string s = kol.ReadLine(); //считываем первую строку, т.к. её считать мы не будем
            int flash = 0;
            while (kol.EndOfStream != true) //выполняем цикл до тех пор, пока не законичтся текст в файле
            {
                s = kol.ReadLine(); // считываем очередную строку
                flash++;
            }
            kol.Close();//закрываем StreamReader
            return flash; // возвращаем значение
        }
        public Bus[] infa()
        {
            StreamReader ya = new StreamReader("Автобусы.txt", Encoding.Default); // создаем объект StreamReader
            string one = ya.ReadLine(); // считываем строку
            Bus[] omg = new Bus[Read()]; // создаем объект omg, а для определения размера обращаемся к методу Read()
            int n = Read();
            string[] BusInfo = null;
            for (int i = 0; i < n; i++) // организуем цикл для перебора элементов массива
            {
                one = ya.ReadLine(); //считываем очередную строку
                BusInfo = one.Split(',');
                if (BusInfo.Length == 4)
                {
                    omg[i] = new Bus(BusInfo[0], BusInfo[1], BusInfo[2], BusInfo[3]); // заполняем наш массив
                }

            }
            ya.Close(); // закрываем StreamReader
            return omg;
        }
    }
    class Program
    {
        public static Bus[] Zapros(Bus[] park) // создаем функцию для запроса
        {
            Console.WriteLine("Ищем сведения об автобусах, находящихся:\n1)В парке\n2)На маршруте");
            int zzz = 0;
            string za = null;
            while ((zzz < 1) || (zzz > 2)) //выполняем цикл до тех пор, пока не будет введено возможное значение
            {
                zzz = Convert.ToInt32(Console.ReadLine());
            }
            StreamReader yo = new StreamReader("Автобусы.txt", Encoding.Default); // задаем объект StreamReader
            string s = yo.ReadLine(); // считываем первую строку
            string[] N = s.Split(','); // создаем строковый массив с разделителем ","
            foreach (string ONE in N)// организуем цикл для ввода первой строки
            {
                Console.Write("{0,-35}", ONE); // вводим в строку очередной элеменгт массива
            }
            yo.Close(); //закрываем StreamReader
            Console.WriteLine();
            if (zzz == 1) // если запрос об автобусах в парке, то переменной za присваиваем значение "в парке" иначе "на маршруте"
                za = "в парке";
            else za = "на маршруте";
            for (int i = 0; i < park.Length; i++)
            {
                if (park[i].location == za) // сравниваем очередой элемент массива с переменной za и если они равны, то выводим сведения об автобусе
                {
                    Console.Write("{0,-35}", park[i].nomer_bus);
                    Console.Write("{0,-35}", park[i].FIO);
                    Console.Write("{0,-35}", park[i].bus_route);
                    Console.Write("{0,-35}", park[i].location);
                    Console.WriteLine();
                }
            }
            return park;

        }
        public static Bus[] spisok(Bus[] park) //создаем функцию для создания списка автобусов
        {
            StreamReader yo = new StreamReader("Автобусы.txt", Encoding.Default); // задаем объект StreamReader
            string s = yo.ReadLine(); // считываем первую строку
            string[] N = s.Split(','); // создаем строковый массив с разделителем ","
            foreach (string ONE in N)// организуем цикл для ввода первой строки
            {
                Console.Write("{0,-35}", ONE); // вводим в строку очередной элеменгт массива
            }
            yo.Close();
            Console.WriteLine();
            for (int i = 0; i < park.Length; i++)
            {
                Console.Write("{0,-35}", park[i].nomer_bus);
                Console.Write("{0,-35}", park[i].FIO);
                Console.Write("{0,-35}", park[i].bus_route);
                Console.Write("{0,-35}", park[i].location);
                Console.WriteLine();
            }
            return park;
        }
        public static Bus[] SW(Bus[] park) // создаем функцию для записи текущей информации в файл
        {
            StreamReader readone = new StreamReader("Автобусы.txt", Encoding.Default);
            string s = readone.ReadLine();
            readone.Close();
            StreamWriter stream = new StreamWriter("Автобусы.txt", false, Encoding.Default);// создаем объект StreamWriter
            stream.Write(s); //записываем первую строку в файл
            stream.WriteLine(); // переходим на следующую
            for (int i = 0; i < park.Length; i++)
            {
                stream.Write(park[i].nomer_bus + "," + park[i].FIO + "," + park[i].bus_route + "," + park[i].location); //вносим данные в файл
                stream.WriteLine();
            }
            stream.Close();
            return park;

        }
        public static Bus[] tutu(Bus[] park) // создаем функцию для въезда\выезда автобуса в\из парка
        {
            Console.WriteLine("Выезд или Въезд:\n1)Выезд\n2)Въезд");
            int w = 0, l = 0;
            while ((w < 1) || (w > 2)) //выполняем цикл до тех пор, пока не будет введено возможное значение
            {
                w = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Введите номер автобуса:");
            string nomer = Console.ReadLine(); //вводим номер автобуса
            for (int i = 0; i < park.Length; i++)
            {
                if (park[i].nomer_bus.ToLower() == nomer.ToLower() && w == 1) // если номер автобуса равен введенному номеру и мы выбрали выезд идем по следующей ветве
                {
                    if (park[i].location == "на маршруте") //если мы ввели номер автобуса, который итак на маршруте, то выводим сообщение и выходим из цикла
                    {
                        Console.WriteLine("Автобус итак на маршруте");
                        break;
                    }
                    park[i].location = "на маршруте";
                    Console.WriteLine("Автобус с номером:{0} теперь {1}", nomer, park[i].location);
                    break;
                }
                else if (park[i].nomer_bus.ToLower() == nomer.ToLower() && w == 2)
                {
                    if (park[i].location == "в парке")//если мы ввели номер автобуса, который итак в парке, то выводим сообщение и выходим из цикла
                    {
                        Console.WriteLine("Автобус итак в парке");
                        break;
                    }
                    park[i].location = "в парке";
                    Console.WriteLine("Автобус с номером:{0} теперь {1}", nomer, park[i].location);
                    break;
                }
                else l = l + 1;
            }
            if (l == park.Length) Console.WriteLine("Нет автобуса с таким номером"); // если ни один из номеров не равен введенному, то выводим следующее сообщение
            park = SW(park); // обращаемся к фуекции SW для записи информации в файл
            return park;
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 30); // задаем размер консоли
            string s = "";
            Bus[] park = new Bus(s, s, s, s).infa(); // объявляем объект типа Bus
            begin:
            int c = 0;
            Console.WriteLine("Выберите действие, которое хотите выполнить:\n1)Данные обо всех автобусах\n2)Выезд(Въезд) автобуса из(в) парка\n3)Cведения об автобусах, находящихся в парке, или об автобусах, находящихся на маршруте");
            while ((c < 1) || (c > 3)) //выполняем цикл до тех пор, пока не будет введено возможное значение
            {
                c = Convert.ToInt32(Console.ReadLine());
            }
            if (c == 1) park = spisok(park); // если выбрали первый пункт, то составляем список с данными обо всех автобусах
            if (c == 2) park = tutu(park); // если выбрали второй пункт, то редактируем локацию автобуса
            if (c == 3) park = Zapros(park); // если выбрали третий пункт, выполняется запрос
            Console.WriteLine();
            Console.WriteLine("Выйти?");
            string exit = Console.ReadLine();
            if (exit.ToLower() != "yes") goto begin;
        }
    }
}












