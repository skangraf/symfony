using System.Windows;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Zalogowany.xaml
    /// </summary>
    public partial class Zalogowany : Window
    {
        private string User { set; get; }
        public Zalogowany(string user)
        {
            InitializeComponent();
            this.User = user;
        }

        private void WylogujAction(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void EdytujAction(object sender, RoutedEventArgs e)
        {
            Profil profil = new Profil(this.User);
            profil.Show();
            this.Close();
        }
    }
}
