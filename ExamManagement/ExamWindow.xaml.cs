using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using ExamManagement.Models;
using ExamManagement.Services;
using ExamManagement.Storage;
using ExamManagement.UserControls;
using HandyControl.Controls;

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow: INotifyPropertyChanged
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

        private Answer _selectedAnswer;

        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                SetField(ref _selectedAnswer, value, nameof(SelectedAnswer));
            
            }
        }


        private bool _isImageQuestion;

        public bool IsImageQuestion
        {
            get { return _isImageQuestion; }
            set
            {
                SetField(ref _isImageQuestion, value, nameof(IsImageQuestion));
            }
        }


        private BitmapImage _imageQuestion;

        public BitmapImage ImageQuestion
        {
            get { return _imageQuestion; }
            set
            {
                SetField(ref _imageQuestion, value, nameof(ImageQuestion));
            }
        }

        private bool _isTextQuestion;

        public bool IsTextQuestion
        {
            get { return _isTextQuestion; }
            set
            {
                SetField(ref _isTextQuestion, value, nameof(IsTextQuestion));
            }
        }



        private int _numberOfQuiestions;

        public int NumberOfQuestions
        {
            get { return _numberOfQuiestions; }
            set { _numberOfQuiestions = value; }
        }

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
        public Exam Exam { get; set; }
        private readonly ExamService examService;
        private readonly ExamResult examResult;
        private readonly StudentService studentService;
        private readonly ImageService imageService;
        
        private StudentExam studentExam;
        public ExamWindow(Exam exam, Student student)
        {
            if(student == null)
            {
                HandyControl.Controls.MessageBox.Show("Student is not registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
                return;

            }

            if (exam is null)
            {

                HandyControl.Controls.MessageBox.Show("Exam is not registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
                return;
            }
            
            Student = student;

            examService = new ExamService();
            examResult = new ExamResult();
            studentService = new StudentService();
            studentExam = new StudentExam();
            imageService = new ImageService();

            InitializeComponent();
            Exam = exam;
            
            NumberOfQuestions = exam.Questions.Count();
            SolvedQuestions = 0;
            UnsolvedQuestions = NumberOfQuestions;
            submitExam.Visibility = Visibility.Hidden;

            if (exam.RandomSorting)
            {
                var random = new Random();
                exam.Questions.OrderBy(x => random.Next()).ToList();
            
            }

            if (!string.IsNullOrEmpty(exam.Questions[_i].ImageUrl))
            {
                ImageQuestion = imageService.GetMedia(exam.Questions[_i].ImageUrl);
                radioListBoxEdit.ItemsSource = exam.Questions[_i].Answers;
                IsImageQuestion = true;
                IsTextQuestion = false;
            }
            else
            {
                IsImageQuestion = false;
                IsTextQuestion = true;
                question.Text = exam.Questions[_i].Text;
                radioListBoxEdit.ItemsSource = exam.Questions[_i].Answers;
            }

        }

        //private async void StartExam(Guid examId)
        //{
        //    this.exam = await studentService.StartExam(examId);
        //    NumberOfQuestions = exam.Questions.Count();
        //    SolvedQuestions = 0;
        //    UnsolvedQuestions = NumberOfQuestions;

        //}
        public void SubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            SolvedQuestions++;
            UnsolvedQuestions--;
            studentExam.SelectedAnswers.Add(Exam.Questions[_i].Answers.IndexOf(SelectedAnswer));
            SelectedAnswer = new Answer();
            if(_i < Exam.Questions.Count - 1)
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

            if (!string.IsNullOrEmpty(Exam.Questions[_i].ImageUrl))
            {
                ImageQuestion = imageService.GetMedia(Exam.Questions[_i].ImageUrl);
                radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                IsImageQuestion = true;
                IsTextQuestion = false;
            }
            else
            {
                IsImageQuestion = false;
                IsTextQuestion = true;
                question.Text = Exam.Questions[_i].Text;
                radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
            }
           
        }
        public async void SubmitExam_Click(object sender, RoutedEventArgs e)
        {
            await studentService.SubmitExamAnswers(Exam, Student.Id, studentExam);
            bool success = true;
            if (success)
            {
                HandyControl.Controls.MessageBox.Show("Successfully submited");

            }
            else
            {
                HandyControl.Controls.MessageBox.Show("Failed to submit");
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
