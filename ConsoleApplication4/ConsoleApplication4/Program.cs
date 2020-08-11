using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 0;
            Console.WriteLine(" Сколько покупателей в очереди?");
                N = int.Parse(Console.ReadLine());//присваиваем переменной число, обозначающее сколько человек в очереди
            int[] T = new int[N + 1];//массив на время обслуживания
            int[] V = new int[N + 1];//массив на время пребывания
            Random r = new Random();//случайно заполняем массив
            for (int i = 1; i < N + 1; i++)//счетчик
            {
                T[i] = r.Next(2, 20);//выводим случайные числа, обозначающие время обслуживания клиента в очереди
                
            Console.Write("\nВремя обслуживания {0}-го клиента в очереди {1}-минут ", i, T[i]);
            }
            int max = T.Max(); // находим максимальное значение в массиве Т, чтобы потом узнать его индекс
            int maxind = 0;
            for (int i = 1; i < N + 1; i++)//счетчик
            {
                if (T[i] == max)
                {
                    maxind = i;
                }
                if (i == 1)
                {
                    V[i] = T[i];
                    Console.WriteLine("\nВремя пребывания первого клиента в очереди составило: {0}", V[i]);
                }
                else
                {
                    for (int j = 1; j < i; j++)//счетчик для нахождения времени пребывания в очереди остальных клиентов, кроме первого 
                    {
                        V[i] += T[j];

                    }
                    Console.WriteLine("Время пребывания {0}-го клиента в очереди составило: {1}", i, V[i]);
                }

            }
            Console.WriteLine("Номер клиента, для обслуживания которого кассиру потребовалось больше всего времени: {0}", maxind);
            Console.ReadKey();
            Console.WriteLine("Выйти? Да - yes");
            string c = Console.ReadLine();
            if (c != "yes") ;

        }
    }

    }