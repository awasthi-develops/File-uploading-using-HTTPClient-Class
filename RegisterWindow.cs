using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfAppLogin
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=as_login;Uid=root;Pwd=;");
                connection.Open();
                MySqlCommand com, com1;
                string str1 = "select count(*) from users where email = @UserName";
                com1 = new MySqlCommand(str1, connection);
                com1.CommandType = CommandType.Text;
                com1.Parameters.AddWithValue("@UserName", rgUsername.Text);
                object obj1 = com1.ExecuteScalar();
                if (Convert.ToInt32(obj1) > 0)
                {
                    MessageBox.Show("UserName already exists");
                }
                else
                {
                    try
                    {
                        string str = "insert into users(name,email,password) values (@Name,@UserName,@Password)";
                        com = new MySqlCommand(str, connection);
                        com.CommandType = CommandType.Text;
                        com.Parameters.AddWithValue("@Name", rgName.Text);
                        com.Parameters.AddWithValue("@UserName", rgUsername.Text);
                        com.Parameters.AddWithValue("@Password", rgPassword.Password);
                        object obj = com.ExecuteScalar();
                        MessageBox.Show("User created successfully");
                        MainWindow win = new MainWindow();
                        win.Show();
                        this.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error:please try again");
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("error:please try later");
            }
        }
    }
}
