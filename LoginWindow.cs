using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfAppLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=as_login;Uid=root;Pwd=;");
                connection.Open();
                MySqlCommand com;
                string str = "select count(*) from users where email = @UserName and password = @Password";
                com = new MySqlCommand(str, connection);
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@UserName",textbox.Text);
                com.Parameters.AddWithValue("@Password",passwordbox.Password);
                object obj = com.ExecuteScalar();

                if (Convert.ToInt32(obj) > 0)
                    {
                        MessageBox.Show("Login successful");
                    Window1 win1 = new Window1();
                    win1.Show();
                    this.Close();
                    
                    }
                    else
                    {
                        MessageBox.Show("Login failed,wrong credentials or not registered");
                    }
                
               

                }catch (Exception)
            {
                MessageBox.Show("error,wait");

            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hyperlink_Click1(object sender, RoutedEventArgs e)
        {
            Window2 rg = new Window2();
            rg.Show();
            this.Close();
        }

    }
}

