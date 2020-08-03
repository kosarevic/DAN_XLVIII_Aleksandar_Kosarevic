using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zadatak_1.Model;
using Zadatak_1.Validation;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            try
            {
                //User is extracted from the database matching inserted paramaters Username and Password.
                SqlCommand query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username", sqlCon);
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                User user = new User();

                foreach (DataRow row in dataTable.Rows)
                {
                    user = new User
                    {
                        Id = int.Parse(row[0].ToString()),
                        Username = row[1].ToString(),
                        Password = row[2].ToString()
                    };
                }
                //If username is as value below, Employe window is engaged.
                if (user.Username == "Zaposleni" && user.Password == "Zaposleni" && txtPassword.Password == "Zaposleni")
                {
                    EmployeWindow dashboard = new EmployeWindow();
                    dashboard.Show();
                    this.Close();
                    return;
                }
                //If username is as value below, User window is engaged.
                else if (txtPassword.Password == "Gost" && user.Password == "Gost" && user.Username != null)
                {
                    //Validation if user has pending order to be approved.
                    if (!OrderValidation.UserHasOrder(user))
                    {
                        UserWindow dashboard = new UserWindow(user);
                        dashboard.Show();
                        this.Close();
                        return;
                    }
                    else
                    {
                        //If user has order with pending approval, application exits to the login screen.
                        return;
                    }
                }
                else if (txtPassword.Password != "Gost" && user.Username != null)
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect password, please try again.", "Notification");
                    return;
                }
                else
                {
                    user = new User(txtUsername.Text, txtPassword.Password);

                    if (AddUserValidation.Validate(user))
                    {
                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                        {
                            var cmd = new SqlCommand(@"insert into tblUser values (@Username, @Password); SELECT SCOPE_IDENTITY();", conn);
                            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                            conn.Open();
                            user.Id = Convert.ToInt32(cmd.ExecuteScalar());
                            conn.Close();
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("User Successfully created.", "Notification");
                            UserWindow dashboard = new UserWindow(user);
                            dashboard.Show();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
