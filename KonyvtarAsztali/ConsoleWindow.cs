using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarAsztali
{
    public static class ConsoleWindow
    {
        public static void Run()
        {
            ConsoleHelper.ShowConsole();

            var stat = new Statistics();
            var books = stat.Kapcsolat();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} - {book.Author} ({book.Publish_year})");
            }

            Console.WriteLine("Nyomj meg egy billentyűt a kilépéshez...");
            Console.ReadKey();
        }
    }


}
