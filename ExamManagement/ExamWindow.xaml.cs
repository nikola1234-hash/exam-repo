using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using DevExpress.Xpf.Core;
using ExamManagement.Models;

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : ThemedWindow, INotifyPropertyChanged
    {
        private int _i = 0;
        private int unsolvedQuestions;
        private int solvedQuestions;

        public Timer Timer { get; set; }

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


        public List<Result> Results { get; set; }
        public int NumberOfQuestions => exam.GetTotalNumberOfQuestions();
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

        public ExamWindow(Exam exam)
        {
            if (exam is null)
            {
                this.Close();
            }
            this.exam = exam;
            InitializeComponent();
            submitExam.Visibility = Visibility.Hidden;


            Results = new List<Result>();

            UnsolvedQuestions = exam.GetTotalNumberOfQuestions();
            exam.ShuffleQuestions();
            question.Text = exam.Questions[_i].Text;
            radioListBoxEdit.ItemsSource = exam.Questions[_i].Answers;
            Timer = new Timer(exam.TotalTime);
            Timer.Start();
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            MessageBox.Show("Time elapse");

            this.Close();
        }

        public void SubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            SolvedQuestions++;
            UnsolvedQuestions--;
            SelectedAnswer.IsChecked = true;
            Results.Add(new Result(exam.Questions[_i], SelectedAnswer));
            
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
        public void SubmitExam_Click(object sender, RoutedEventArgs e)
        {

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
