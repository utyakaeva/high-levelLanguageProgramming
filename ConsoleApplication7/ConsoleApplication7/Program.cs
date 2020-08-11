using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите строку:");//просим пользователя ввести строку
            string s = Console.ReadLine();
            s = tolower(s);
            Console.WriteLine("Количество элементов в строке {0}.", s.Length);//считает количество элементов в строке
            string[] words = s.Split(new char[] { ' ', '.' });//строковый массив,убирает лишние пробелы в начале,в конце,между словами
            Console.WriteLine("Слова,отличные от последнего:");
            string word = "";
            for (int i = 0; i < words.Length - 1; i++)//цикл на перебор слов в строке
            {
                //Если встретили слово, отличное от последнего
                if (words[i] == words[words.Length - 1]) continue;
                string stroka = words[i].Substring(words[i].Length - 1);
                //переносит последнюю букву в начало слова(обрезает определенную часть строки)
                string stroka1 = (string.Concat(stroka, words[i].Substring(0, words[i].Length - 1)));
                word = word +" " + stroka1;
                string[] values = new string[] {word};
                String s10 = String.Join(" ", values);
                Console.WriteLine(s10);
                {
                    char[] ch = s10.ToCharArray();
                    int count = ch.Where((n) => n >= '0' && n <= '9').Count();
                    Console.WriteLine("Количество цифр в строке: " + count);
                }
            }
            Console.ReadKey();
        }

        private static string tolower(string d)
        {
            return d = d.ToLower();
        }

    }
}





