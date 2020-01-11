using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    public partial class Rejestracja : Window
    {


        public Rejestracja()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void ZresetujForm(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void AnulujForm(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void WyslijForm(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Wypełnij pole adres e-mail.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Podaj prawidłowy adres e-mail.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Wypełnij pole hasło.";
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Wypełnij pole powtórz hasło.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Hasła nie są identyczne.";
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    password = DB.HasloDoBase64(password);
                    errormessage.Text = "";

                    try
                    {

                        SqlConnection con = DB.GetConnection();


                        SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[PZUsers] ([userid],[email],[password],[imie],[nazwisko]) VALUES (newid(), '" + email + "', '" + password + "', '" + firstname + "', '" + lastname + "')", con)
                        {
                            CommandType = CommandType.Text
                        };
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Proces rejestracji przebiegł pomyślnie. Prosimy o zalogowanie się");
                        Reset();
                        Login login = new Login();
                        login.Show();
                        Close();

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
        }

        private void ZalogujForm(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

    }
}
