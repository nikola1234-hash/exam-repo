using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using ExamManagement.Models;
using ExamManagement.Services;
using ExamManagement.Storage;

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window, INotifyPropertyChanged
    {
        private int _i = 0;
        private int unsolvedQuestions;
        private int solvedQuestions;


        public Student Student { get; set; }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            {
                SetField(ref _isChecked, value, nameof(IsChecked));
            }
        }
        public GradeEntity Grade { get; set; }
        private Answer _selectedAnswer;

        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                SetField(ref _selectedAnswer, value, nameof(SelectedAnswer));
            
            }
        }


        public int NumberOfQuestions => exam.Questions.Count();
        public int SolvedQuestions { get => solvedQuestions;
            set
            {
                SetField(ref solvedQuestions, value, nameof(SolvedQuestions));
            }
        }
        public int UnsolvedQuestions { get => unsolvedQuestions; 
            set
            {
                SetField(ref unsolvedQuestions, value, nameof(UnsolvedQuestions));
            }
        }
        Exam exam { get; set; }
        private readonly ExamService examService;
        private readonly ExamResult examResult;
        private readonly StudentService studentService;
        private StudentExam studentExam;
        public ExamWindow(Exam exam, Student student)
        {
            if (exam is null)
            {
                this.Close();
            }
            Student = student;

            examService = new ExamService();
            examResult = new ExamResult();
            studentService = new StudentService();
            StartExam(exam);
            studentExam = new StudentExam();
            InitializeComponent();
            submitExam.Visibility = Visibility.Hidden;
            
            UnsolvedQuestions = exam.Questions.Count();

            if (exam.RandomSorting)
            {
                var random = new Random();
                exam.Questions.OrderBy(x => random.Next()).ToList();
            
            }
           
            question.Text = exam.Questions[_i].Text;
            radioListBoxEdit.ItemsSource = exam.Questions[_i].Answers;

        }

        private async void StartExam(Exam exam)
        {
            this.exam = await studentService.StartExam(exam.Id);
        }
        public void SubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            SolvedQuestions++;
            UnsolvedQuestions--;
            studentExam.SelectedAnswers.Add(exam.Questions[_i].Answers.IndexOf(SelectedAnswer));
            SelectedAnswer = new Answer();
            if(_i < exam.Questions.Count - 1)
            {
                _i++;
            }
            else
            {
                question.Visibility = Visibility.Hidden;
                radioListBoxEdit.Visibility = Visibility.Hidden;
                submitExam.Visibility = Visibility.Visible;
                return;
            }
            question.Text = exam.Questions[_i].Text;
            radioListBoxEdit.ItemsSource = exam.Questions[_i].Answers;
        }
        public async void SubmitExam_Click(object sender, RoutedEventArgs e)
        {
            await studentService.SubmitExamAnswers(exam, Student.Id, studentExam);
            bool success = true;
            if (success)
            {
                MessageBox.Show("Successfully submited");
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to submit");
            }
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

    }
}
