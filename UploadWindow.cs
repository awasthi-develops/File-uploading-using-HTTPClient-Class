using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        
        private OpenFileDialog fileChooser;

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            fileChooser = new OpenFileDialog();
            fileChooser.Title = "choose your file";
            fileChooser.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fileChooser.ShowDialog().Value)
            {
                filename.Text = fileChooser.FileName;
            }
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {

                int length = 1024;
                var paramFileBytes = new byte[length];
                paramFileBytes = File.ReadAllBytes(filename.Text);
                Stream paramFileStream = new MemoryStream(paramFileBytes);
                var phpadress = "http://localhost/upload_in_wpf.php";



                HttpContent stringContentname = new StringContent(eTname.Text);
                HttpContent stringContentemail = new StringContent(eTemail.Text);
                HttpContent stringContentfilename = new StringContent(filename.Text);
                HttpContent fileStreamContent = new StreamContent(paramFileStream);
                HttpContent bytesContent = new ByteArrayContent(paramFileBytes);
                

            using (HttpClient httpClient = new HttpClient())
            {
                using (var formdata = new MultipartFormDataContent())
                {
                    formdata.Add(stringContentname, "name");
                    formdata.Add(stringContentemail, "email");
                    formdata.Add(stringContentfilename, "file_name");

                    formdata.Add(fileStreamContent, "file", eTfilename.Text);
                    formdata.Add(bytesContent,eTfilename.Text);
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await httpClient.PostAsync(phpadress,formdata);
                    string responseText = await response.Content.ReadAsStringAsync();
                    ResponseText.Text = responseText.ToString();
                }
            }
        }
    }
}
