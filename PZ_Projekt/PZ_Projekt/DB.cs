using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace PZ_Projekt
{
    class DB
    {

        public static SqlConnection GetConnection()
        {

            SqlConnection con = null;

            string strCon = "Data Source=DESKTOP-NL1D0PO\\SQLEXPRESS;Initial Catalog=PZ_Projekt;User ID=PZ_Projekt;Password=Start123";
            try
            {
                con = new SqlConnection(strCon);
                con.Open();
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
            return con;
        }

        public static string HasloDoBase64(string haslo)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(haslo);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
    }
}
