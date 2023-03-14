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
using System.Windows.Shapes;
using ExamManagement.Models;
using ExamManagement.Services;
using ExamManagement.Storage;
namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private APIService<User> _apiService;

        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _apiService = new APIService<User>("https://localhost:7129");
            User user = new User(username.Text, password.Text);
            bool success = await _apiService.Login(user);
            if (success)
            {
                Storage.Storage.User = username.Text;
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
          
        }
    }
}
