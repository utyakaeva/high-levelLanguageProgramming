using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            StreamReader wow = new StreamReader("fvf.txt", Encoding.Default); // задаем объект StreamReader 
            s = wow.ReadLine(); // считываем первую строку
            const string file= "fvf.txt"; //константа на файл
            int LG = File.ReadAllLines(file).Length;
            string[] lines = new string[LG];//массив строк из файла
            int i;
            Console.WriteLine("Введите число:");
            string pattern = Console.ReadLine();
            int a = Convert.ToInt32(pattern); //преобразуем в INT 

            int count = 0;
            for (i = 0; i < LG; i++)//цикл для перебора в массиве строк
            {
                lines[i] = File.ReadAllLines(file, Encoding.Default)[i];
                string[] elems = lines[i].Split(','); //разбиваем строку на элементы 
                int b = Convert.ToInt32(elems[0]);// кол-во страниц в книге 
                if (b > a)
                {
                    Write(lines[i]);
                    count++;
                }
            }
            //создание исключения
            try
            {
                if (count == 0)
                { throw new Exception("Книга не найдена"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);//вывод исключения
            }
            Write("");
            Console.ReadKey();
        }

        private static string p()//функция для записи строки в новый файл
        {
            string gf = "polucheno.txt";
            return gf;
        }
        
        private static void Write(string str)
        {
            string hoh = "";
            hoh = p();
            FileStream a = new FileStream(hoh, FileMode.Append);//записывает в конец файла 
            StreamWriter sw = new StreamWriter(a, Encoding.Default);//создает новый объект
            sw.WriteLine(str);
            sw.Close();

        }
    }
}






