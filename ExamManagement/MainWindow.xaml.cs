using System.Windows;

namespace ExamManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            var createExamWindow = new CreateExamWindow();
            createExamWindow.ShowDialog();
        }

        private void SearchExamsButton_Click(object sender, RoutedEventArgs e)
        {
            var searchExamsWindow = new SearchExamsWindow();
            searchExamsWindow.ShowDialog();
        }
    }
}
