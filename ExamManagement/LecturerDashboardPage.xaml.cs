using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace ExamManagement.Views
{
    public sealed partial class LecturerDashboardPage : Page
    {
        public ObservableCollection<Exam> Exams { get; set; }

        public LecturerDashboardPage()
        {
            this.InitializeComponent();
            Exams = new ObservableCollection<Exam>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Load the lecturer's exams from the API
            var exams = await ExamManagementAPI.GetLecturerExamsAsync();
            foreach (var exam in exams)
            {
                Exams.Add(exam);
            }

            // Display the lecturer's name in the welcome message
            var lecturerName = await ExamManagementAPI.GetLecturerNameAsync();
            WelcomeTextBlock.Text = $"Welcome, {lecturerName}!";
        }

        private void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the CreateExamPage
            this.Frame.Navigate(typeof(CreateExamPage));
        }

        private void EditExamButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the EditExamPage for the selected exam
            var exam = (sender as Button).DataContext as Exam;
            this.Frame.Navigate(typeof(EditExamPage), exam.Id);
        }

        private async void DeleteExamButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected exam from the API and remove it from the list view
            var exam = (sender as Button).DataContext as Exam;
            await ExamManagementAPI.DeleteExamAsync(exam.Id);
            Exams.Remove(exam);
        }
    }
}
