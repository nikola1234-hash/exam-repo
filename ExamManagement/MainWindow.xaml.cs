using System.Windows;

namespace ExamManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(Storage.Storage.User != "Lector")
            {
                createButton.Visibility = Visibility.Hidden;
            }
        }

        private void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            var createExamWindow = new CreateExamWindow();
            createExamWindow.ShowDialog();
        }

        private void SearchExamsButton_Click(object sender, RoutedEventArgs e)
        {
            var searchExamsWindow = new SearchExamWindow();
            searchExamsWindow.ShowDialog();
        }
    }
}
