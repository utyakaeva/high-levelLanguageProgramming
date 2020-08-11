using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите три любых целых положительных числа через enter"); // приглашение на ввод чисел
            string a = Console.ReadLine(); // ввод данных
            string b = Console.ReadLine();
            string c = Console.ReadLine();
            double aa = Convert.ToDouble(a); // вводим первое число
            double bb = Convert.ToDouble(b);
            double cc = Convert.ToDouble(c);
            double summ = aa + bb + cc; // число, которое обозначет сумму трех введенных чисел
            double arifm = summ / 3; // нахождение среднего арифметического
            double geometrik= 0;
            geometrik = Math.Pow (aa* bb* cc,1.0/3.0);
            Console.WriteLine("Вы ввели: Среднее арифметическое- " + arifm + " Среднее геометрическое- " + geometrik); // вывод среднего арифметического и геометрического
            Console.WriteLine("Выйти? Да - yes");
            Console.ReadLine();
            if (c != "yes");
                }
    }        
  }

