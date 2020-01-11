using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

        }

        Rejestracja Rejestracja = new Rejestracja();

        private void RejestracjaAction(object sender, RoutedEventArgs e)
        {
            Rejestracja.Show();
            Close();
        }

        private void ZalogujAction(object sender, RoutedEventArgs e)
        {

            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Podaj adres e-mail.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Podaj prawidłowy adres e-mail.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "Podaj hasło.";
                textBoxEmail.Focus();
            }
            else
            {

                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                password = DB.HasloDoBase64(password);

                try
                {

                    SqlConnection con = DB.GetConnection();

                    SqlCommand cmd = new SqlCommand("Select * from [dbo].[PZUsers] where email='" + email + "'  and password='" + password + "'", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    //
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        string userId = dataSet.Tables[0].Rows[0]["userid"].ToString();

                        Zalogowany zalogowany = new Zalogowany(userId);
                        zalogowany.Show();
                        Close();
                    }
                    else
                    {
                        errormessage.Text = "Podano nieprawidłowe dane do logowania";
                    }

                    con.Close();
                }
                catch (SqlException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        MessageBox.Show("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }

                }


            }
        }

        private void AnulujAction(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}
