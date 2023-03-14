using ExamManagement.Models;
using ExamManagement.Services;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        private ObservableCollection<Answer> _answers;

        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set => SetField(ref _answers, value, nameof(Answers));
        }
        private Question selectedQuestion { get; set; }
        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
                
                {

                SetField(ref _questions, value, nameof(Questions));
                }
        }

 

        private readonly APIService<Exam> _apiService;

        private ExamService examService;
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
            Answers = new ObservableCollection<Answer>();
            examService = new ExamService();
            

        }

        private void AddAnswers(object obj)
        {
            selectedQuestion.Answers = ((ObservableCollection<Answer>) obj).ToList();
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // do something
            Questions.Add(new Question());
            SetField(ref _questions, Questions, nameof(Questions));
        }
        private async void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var q in Questions)
            {
                if (string.IsNullOrEmpty(q.ImageUrl))
                {
                    q.ImageUrl = string.Empty;
              
                }
            }
            //Exam exam = new Exam();
            //exam.Name = NameTextBox.Text;
            //exam.StartingHour =(StartingHourTextBox.Text);
            //exam.Questions = Questions.ToList();
            //exam.RandomSorting = (bool)rendomizeQuestions.IsChecked;
            //exam.TotalTime = Convert.ToInt32(totalTimeTextBox.Text);
            //exam.LecturerName = ;
            //exam.Date = (DateTime)DatePicker.SelectedDate;

            //examService.AddExam(exam);
          

            // do something
        }
        private void EditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        } 
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var toRemove = Questions.FirstOrDefault(s => s.Text == (string)(((Button)sender).CommandParameter));
            Questions.Remove(toRemove);
            SetField(ref _questions, Questions, nameof(Questions));
        } 
        private void SaveQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }    
        private void CancelEditQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void AddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var file = File.ReadAllText(openFileDialog.FileName);

            }
                
        }

   
        private void AddAnswers_Click (object sender, RoutedEventArgs e)
        {
            var question = Questions.FirstOrDefault(s => s.Text == (string)(((Button)sender).CommandParameter));
            selectedQuestion = question;
            var window = new AddAnswersWindow();
            window.RaiseCustomEvent += Window_RaiseCustomEvent;
            window.Show();


            if(Answers == null)
            {
                Answers = new ObservableCollection<Answer>();

            }
            var answer = new Answer();

            Answers.Add(new Answer());
            SetField(ref _questions, Questions, nameof(Questions));
            SetField(ref _answers, Answers, nameof(Answers));

        }

        private void Window_RaiseCustomEvent(object? sender, CustomEventArgs e)
        {
            selectedQuestion.Answers = ((ObservableCollection<Answer>)e.Args).ToList();
            selectedQuestion.CorrectAnswerIndex = (int)e.Index;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
