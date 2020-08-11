using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)

        {
            string a;
            Console.WriteLine("Размер массива N = ");
            int n = int.Parse(Console.ReadLine());
            int[] m = new int[n];
            int i = 0;//заводим счетчик массива
            int s = 0, p = 1;
            Console.WriteLine("Введите все элементы массива. После каждого введенного элемента нажмите Enter");
            // просим пользователя ввести все  элементы массива
            Console.WriteLine();
            while (i < n)
            {
                a = Console.ReadLine();
                m[i] = Convert.ToInt32(a);//преобразуем строку для записи в массив
                i++;
            }

            for (i = 0; i < n; i++) //счетчик
                Console.Write("m[{0}]={1} ", i, m[i]);//выводим на экран получившийся массив
            Console.WriteLine();
            for (i=0; i<n; i++)
            { 
                if (m[i] < 0)//находим отрицательные элементы массива
                {
                    p *= m[i];//находим произведение отрицательных элементов массива
                }
            }
            int index = 0;
            int max = m[0];
            for (i = 0; i < n; i++)
            {
                if (max < m[i])//находим максимальный элемент массива
                {
                    index = i;
                    max = m[i];
                }
            }
            for (i = 0; i < index; i++)
                if (m[i] > 0)
                {
                    s += m[i];//считаем сумму положительных до max
                }
            for (i = 0; i < n; i++)
            Console.WriteLine();
            Console.WriteLine("Вы ввели:Сумма положительных до max = " + s);
            Console.WriteLine("Произведение отрицательных элементов массива = " + p);
            Console.ReadKey();
            Console.WriteLine("Выйти? Да - yes");
            string c = Console.ReadLine();
            if (c != "yes") ;
        }
    }


}
    

