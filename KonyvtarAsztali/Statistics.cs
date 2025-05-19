using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KonyvtarAsztali
{
    internal class Statistics
    {
        public List<Book> books;
        private string connectionString = "server=localhost;database=books;user=root;password='';";

        public Statistics()
        {
            books = new List<Book>();
        }

        public List<Book> Kapcsolat()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM books;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader.GetString("title");
                            string author = reader.GetString("author");
                            int publish_year = reader.GetInt32("publish_year");
                            int page_count = reader.GetInt32("page_count");

                            books.Add(new Book(title, author, publish_year, page_count));
                        }
                    }
                }

                    int hosszabb500 = books.Count(b => b.Page_count > 500);
                    Console.WriteLine($"500 oldalnál hosszabb könyvek száma: {hosszabb500}");

                    bool vanRegi = books.Any(b => b.Publish_year < 1950);
                    Console.WriteLine($"Van 1950-nél régebbi könyv: {(vanRegi ? "Igen" : "Nem")}");

                    var leghosszabb = books.OrderByDescending(b => b.Page_count).First();
                    Console.WriteLine($"\nLeghosszabb könyv:\nCím: {leghosszabb.Title}, Szerző: {leghosszabb.Author}, Év: {leghosszabb.Publish_year}, Oldalszám: {leghosszabb.Page_count}");

                    var legtobbSzerzo = books
                        .GroupBy(b => b.Author)
                        .OrderByDescending(g => g.Count())
                        .First();
                    Console.WriteLine($"\nLegtöbb könyvvel rendelkező szerző: {legtobbSzerzo.Key} ({legtobbSzerzo.Count()} könyv)");

                    Console.Write("\nAdj meg egy könyvcímet: ");
                    string keresettCim = Console.ReadLine();

                    var talalat = books.FirstOrDefault(b => b.Title.Equals(keresettCim, StringComparison.OrdinalIgnoreCase));
                    if (talalat != null)
                    {
                        Console.WriteLine($"Szerző: {talalat.Author}");
                    }
                    else
                    {
                        Console.WriteLine("Nincs ilyen könyv.");
                    }


                    Console.WriteLine("-------------------------------------------------------");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Adatbázis hiba történt: " + ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message);
                Environment.Exit(1);
            }
            return books;
        }

    }
}
