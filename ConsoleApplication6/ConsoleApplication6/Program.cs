using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер квадратной матрицы: ");
            int r = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Размер матрицы{0,0}*{0,0}", r);
            int[,] MATRICA = new int[r, r];
            Random random = new Random();
            Console.WriteLine("\nИсходная матрица\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0,8}", "№");
            for (int j = 0; j < r; j++)
            {
                Console.Write("{0,8}", j);
            }// for j
            Console.WriteLine("\n");
            for (int i = 0; i < r; i++)
            {
                //выводим матрицу
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("{0,8}", i);
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = 0; j < r; j++)
                {
                    MATRICA[i, j] = random.Next(-10, 100);
                    Console.Write("{0,8}", MATRICA[i, j]+ "\t");
                } // for j
                Console.WriteLine();
            } // for i
            int nd = 2 * r - 2; // количество диагоналей вокруг главной
            int sumD; // сумма диагонали 
            int iBeg, iEnd, jBeg, jEnd, minD, kminD;
            minD = MATRICA[r - 1, 0]; kminD = 0;
            // нижние диагонали
            Console.WriteLine("Нижние диагонали");
            iBeg =r-1; iEnd = r - 1;
            jBeg = 0; jEnd = 0;
            for (int kD = 1; kD < nd / 2; kD++)
            {
                // диагональ с номером kD 
                sumD = 0;
                Console.Write("(Диагональ №{0,0})", kD+1);
                // Console.WriteLine("Номера элементов");
                // суммы по диагоналям
                for (int i = iBeg, j = jBeg; i <= iEnd; i++, j++)
                {
                    sumD += MATRICA[i, j];
                    // Console.WriteLine("{0,3}*{1,3}",i,j);
                }
                Console.WriteLine("Сумма диагонали={0,0}", sumD);
                iBeg--; jEnd++;
            
            } 
            // верхние диагонали
            Console.WriteLine("Верхние диагонали");
            iBeg = 0; iEnd = r - 2;
            jBeg = 1; jEnd = r - 1;
            for (int kD = nd / 2; kD < nd; kD++)
            {
                // диагональ с номером kD 
                sumD = 0;
                Console.Write("(Диагональ №{0,0})", kD);
                // Console.WriteLine("Номера элементов");
                // суммы по диагоналям
                for (int i = iBeg, j = jBeg; i <= iEnd; i++, j++)
                {
                    sumD += MATRICA[i, j];
                    // Console.WriteLine("{0,3}*{1,3}", i, j);
                }
                Console.WriteLine("Сумма диагонали={0,0}", sumD);
                iEnd--; jBeg++;
                if (sumD < minD)
                {
                    minD = sumD; kminD = kD;
                }
            } 
            Console.WriteLine("Минимальная сумма ={0,0} у диагонали №{1,4}", minD, kminD);
            //Сумма элементов в тех строках, которые не содержат отрицательных элементов

            for (int j = 0; j < r; j++)
            {
                int s = 0;
                bool d = true;
                for (int k = 0; k < r; k++)
                {
                    s += MATRICA[k, j];
                    if (MATRICA[k, j] < 0) d = false;
                }
                if (d)
                    Console.WriteLine("Сумма элементов строки,не имеющей отрицательных элементов [{0}] = " + s, j + 1);
                { 
                Console.ReadKey();
                
                    
                    
                
                }
            }
        }
}
        }