using MySql.Data.MySqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KonyvtarAsztali
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Statistics s;
        List<Book> books;
        private string connectionString = "server=localhost;database=books;user=root;password='';";
        public MainWindow()
        {
            InitializeComponent();
            s = new Statistics();
            books = s.Kapcsolat();
            konyvLista.ItemsSource = books;
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = konyvLista.SelectedItem as Book;

            if (selectedBook == null)
            {
                MessageBox.Show("Törléshez előbb válasszon ki könyvet!", "Figyelem", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Megerősítés
            var result = MessageBox.Show("Biztos szeretné törölni a kiválasztott könyvet?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            // Törlés az adatbázisból
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM books WHERE title = @title AND author = @author;";
                    using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", selectedBook.Title);
                        cmd.Parameters.AddWithValue("@author", selectedBook.Author);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sikeres törlés!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                            books.Remove(selectedBook);
                            konyvLista.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Nem sikerült törölni a könyvet.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a törlés során: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}