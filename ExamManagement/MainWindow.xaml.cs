using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace ExamManagement
{
    public partial class MainWindow: INotifyPropertyChanged
    {

        private bool _isStudent;

        public bool IsStudent
        {
            get { return _isStudent; }
            set
            {
                SetField(ref _isStudent, value, nameof(IsStudent));
            }
        }


        private bool _isLector;

        public bool IsLector
        {
            get { return _isLector; }
            set
            {
                SetField(ref _isLector, value, nameof(IsLector));
            }
        }



        public MainWindow()
        {

       

            InitializeComponent();
            if(Storage.Storage.User == "Lector" || Storage.Storage.User == "lector")
            {
                IsLector = true;

            }
            else
            {
               IsStudent = true;
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
        private void editExam(object sender, RoutedEventArgs e)
        {
            UpdateExam window = new UpdateExam();
            window.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

       
        private void searchButton_Click_1(object sender, RoutedEventArgs e)
        {
            StudentStatsWindow window = new StudentStatsWindow();
            window.Show();
        }
    }
}
