using ExamManagement.Models;
using ExamManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for CreateExamWindow.xaml
    /// </summary>
    public partial class CreateExamWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Question> _questions;

        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
                
                {

                SetField(ref _questions, value, nameof(Questions));
                }
        }


        private readonly APIService<Exam> _apiService;


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

        public CreateExamWindow()
        {
            InitializeComponent();
            _apiService = new APIService<Exam>("https://localhost:7129");
            Questions = new ObservableCollection<Question>();

        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // do something
            Questions.Add(new Question());
            SetField(ref _questions, Questions, nameof(Questions));
        }
        private void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            Exam exam = new Exam();
            exam.Name = "Test";
            exam.StartingHour = TimeSpan.FromMilliseconds(1000000);
            exam.Questions = new List<Question>();
            exam.RandomizeQuestions = true;
            exam.TotalTime = 100;
            exam.LecturerName = "Test";
            _apiService.CreateExamAsync(exam);
            

            // do something
        }
        private void EditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        } 
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        } 
        private void SaveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }    
        private void CancelEditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
