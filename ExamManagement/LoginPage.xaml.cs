using System.Windows;
using System.Windows.Controls;

namespace ExamManagement
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the login API and navigate to the dashboard if successful
            //bool loginSuccessful = await ExamManagementAPI.LoginAsync(EmailTextBox.Text, PasswordBox.Password, RememberMeCheckBox.IsChecked ?? false);

            //if (loginSuccessful)
            //{
            //    // Navigate to the LecturerDashboardPage
            //    var mainWindow = Window.GetWindow(this) as MainWindow;
            //    mainWindow.MainFrame.Navigate(new LecturerDashboardPage());
            //}
            //else
            //{
            //    // Display an error message if the login failed
            //    MessageBox.Show("Invalid email or password.");
            //}
        }
    }
}
