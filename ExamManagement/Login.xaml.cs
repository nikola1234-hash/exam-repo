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
        private AuthService authService;

        public Login()
        {
            InitializeComponent();
        }
        // This method is an event handler for a button click. It performs a login process by creating a User object with the username and password entered in the UI,
        // checking if both fields are populated, hiding the login panel and showing an info panel with a message indicating that the login process has started.
        // It then creates an instance of the AuthService class, which is responsible for handling the actual login request to a server.
        //
        // The method calls the Login method of the AuthService instance using the await keyword, which means that the method will not block while the login request
        // is being processed. If the login is successful, the MainWindow is shown and the current window is closed. If the login is unsuccessful, an error message
        // is displayed, and the UI is reset to its original state.
        //
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
            authService = new AuthService();

            bool success = await authService.Login(user);
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
