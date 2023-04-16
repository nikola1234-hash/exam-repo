using System.Windows;

namespace EasyTestMaker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(Storage.Storage.User != "Lector")
            {
                //createButton.Visibility = Visibility.Hidden;
                //searchLectorhButton.Visibility = Visibility.Hidden;
                //statisticsShow.Visibility = Visibility.Hidden;

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
        private void ShowStatisctics(object sender, RoutedEventArgs e)
        {
            var statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
        }
        private void searchExamsLectorButton(object sender, RoutedEventArgs e)
        {

        }
    }
}
