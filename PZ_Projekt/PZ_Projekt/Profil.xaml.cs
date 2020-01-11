using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;


namespace PZ_Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Profil.xaml
    /// </summary>
    public partial class Profil : Window
    {

        private string UserId { get; set; }
 

        public Profil(string userid)
        {
            InitializeComponent();
            this.UserId = userid;
            this.FillupForm();
            
        }

        private void GetUser()
        {
            try
            {

                SqlConnection con = DB.GetConnection();

                SqlCommand cmd = new SqlCommand("Select * from [dbo].[PZUsers] where userid='" + this.UserId + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    textBoxFirstName.AppendText(dataSet.Tables[0].Rows[0]["imie"].ToString());
                    textBoxLastName.AppendText(dataSet.Tables[0].Rows[0]["nazwisko"].ToString());
                    textBoxEmail.AppendText(dataSet.Tables[0].Rows[0]["email"].ToString());

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

        private void FillupForm()
        {
            this.GetUser();
        }

        private void AnulujAction(object sender, RoutedEventArgs e)
        {
            Zalogowany zalogowany = new Zalogowany(this.UserId);
            zalogowany.Show();
            Close();
        }

        private void ZapiszAction(object sender, RoutedEventArgs e)
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
                string query = " ";

                if (passwordBoxConfirm.Password.Length > 0 && passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Hasła nie są identyczne.";
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    if (passwordBoxConfirm.Password.Length > 0)
                    {
                        password = DB.HasloDoBase64(password);
                        query = "UPDATE PZUsers SET email = '" + email + "', password = '" + password + "', imie = '" + firstname + "', nazwisko = '" + lastname + "' WHERE userid = '" + this.UserId + "'";

                    }
                    else
                    {
                        query = "UPDATE PZUsers SET email = '" + email + "', imie = '" + firstname + "', nazwisko = '" + lastname + "' WHERE userid = '"+ this.UserId+"'";
                    }

                        
                    errormessage.Text = "";

                    try
                    {

                        SqlConnection con = DB.GetConnection();


                        SqlCommand cmd = new SqlCommand(query, con)
                        {
                            CommandType = CommandType.Text
                        };
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Konto zostało zaktualizowane");

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
    }
}
