using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main(string[] args)
        {

            RegistryKey myKey = Registry.CurrentUser;

            // Для удаления тоже нужно иметь права редактирования.
            RegistryKey wKey = myKey.OpenSubKey("Software", true);

            // Вывод на экран всего содержимого ключа поименно.
            string[] keyNameArray = wKey.GetSubKeyNames();

            foreach (string name in keyNameArray)
            {
                Console.ForegroundColor = ConsoleColor.Gray;

                if (name.Contains("nookery"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.WriteLine(new string(' ', 5) + name);
            }

            // Теперь пытаемся удалить ключ.
            try
            {
                Console.WriteLine("Всего записей в {0}: {1}.", wKey.Name, wKey.SubKeyCount);
                wKey.DeleteSubKey("nookery");

                Console.WriteLine("Запись \'nookery\' успешно удалена из реестра!");
                Console.WriteLine("Теперь записей стало: {0}.", wKey.SubKeyCount);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                wKey.Close();
            }

            // Задержка на экране.
            Console.ReadKey();
        }
    }
}
