using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ExamManagement.Models;
using ExamManagement.Services;
using ExamManagement.Storage;
namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login 
    {
        private APIService<User> _apiService;

        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            User user = new User(username.Text, password.Password);
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("You have to populate username and password");
                return;
            }
            loginPanel.Visibility = Visibility.Hidden;
            infoPanel.Visibility = Visibility.Visible;
            groupBoxInfo.Header = "Started login process";
            groupBoxInfo.Content = "Please wait contacting server...";
            _apiService = new APIService<User>("https://localhost:7129");

            bool success = await _apiService.Login(user);
            if (success)
            {
               
                groupBoxInfo.Header = "Login success";
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                loginPanel.Visibility = Visibility.Visible;
                infoPanel.Visibility = Visibility.Hidden;
                MessageBox.Show("Wrong username or password");
            }
          
        }
    }
}
