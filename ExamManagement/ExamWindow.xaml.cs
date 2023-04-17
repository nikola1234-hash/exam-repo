﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using EasyTestMaker.Constants;
using EasyTestMaker.Models;
using EasyTestMaker.Services;
using EasyTestMaker.Storage;
using EasyTestMaker.UserControls;
using HandyControl.Controls;

namespace EasyTestMaker
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

        private int _currentQuestion;

        public int CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                SetField(ref _currentQuestion, value,nameof(CurrentQuestion));
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
                if(value == NumberOfQuestions)
                {
                   submitExam.Visibility = Visibility.Visible;
                }
                SetField(ref solvedQuestions, value, nameof(SolvedQuestions));
            }
        }
        public int UnsolvedQuestions { get => unsolvedQuestions; 
            set
            {
                SetField(ref unsolvedQuestions, value, nameof(UnsolvedQuestions));
            }
        }
        public Test Exam { get; set; }
        private readonly TestService examService;
        private readonly TestResult examResult;
        private readonly StudentService studentService;
        private readonly ImageService imageService;

        public Dictionary<int, Answer> SelectedAnswers { get; set;}
        
        private StudentTest studentExam;
        public ExamWindow(Test exam, Student student)
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

            examService = new TestService();
            examResult = new TestResult();
            studentService = new StudentService();
            studentExam = new StudentTest();
            imageService = new ImageService();
            SelectedAnswers = new Dictionary<int, Answer>();
            InitializeComponent();
            Exam = exam;
            InitializeTimer();
            studentExam.StudentName = student.Name;
            previousButton.IsEnabled = false;
            if(Exam.Questions.Count == 1)
            {
                nextButton.IsEnabled = false;
            }
            else
            {
                nextButton.IsEnabled = true;
            }

            CurrentQuestion = _i + 1;
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

        private double _counter;

        public double Counter
        {
            get { return _counter; }
            set
            {
                SetField(ref _counter, value, nameof(Counter));
            }
        }
        /// <summary>
        /// Timer of the exam
        /// </summary>

        public void InitializeTimer()
        {
            TimeSpan totalTime = Exam.TotalTime;
            DispatcherTimer timer = new DispatcherTimer();
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = totalTime.ToString("c");
                if (totalTime == TimeSpan.Zero) timer.Stop();
                totalTime = totalTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            timer.Start();
        }

    
        /// <summary>
        /// Previous QUestion method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PreviousQUestion_Click(object sender, RoutedEventArgs e)
        {
            
            SelectedAnswer = new Answer();
            if (_i <= Exam.Questions.Count - 1 && _i > 0)
            {
                _i--;

                if(_i == 0)
                {
                    previousButton.IsEnabled = false;
                }
                if(_i < Exam.Questions.Count - 1)
                {
                    nextButton.IsEnabled = true;
                }
                CurrentQuestion = _i +1;
            }
            else
            {
                HandyControl.Controls.MessageBox.Show("No more questions");
                return;
            }

            if (!string.IsNullOrEmpty(Exam.Questions[_i].ImageUrl))
            {
                ImageQuestion = imageService.GetMedia(Exam.Questions[_i].ImageUrl);
                if (Exam.Questions[_i].AnswersSortedRandomly)
                {
                    var random = new Random();
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers.OrderBy(x => random.Next()).ToList();
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                else
                {
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                IsImageQuestion = true;
                IsTextQuestion = false;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
            }
            else
            {
                IsImageQuestion = false;
                IsTextQuestion = true;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
                question.Text = Exam.Questions[_i].Text;

                radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                if (SelectedAnswers.ContainsKey(_i))
                {
                    radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                }

            }
        }
        /// <summary>
        /// Next Question Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            SelectedAnswer = new Answer();
            if (_i <= Exam.Questions.Count - 1)
            {
                _i++;
                if(_i == Exam.Questions.Count - 1)
                {
                    nextButton.IsEnabled = false;
                }
                if(_i > 0)
                {
                    previousButton.IsEnabled = true;
                }
                CurrentQuestion = _i + 1;
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

                if (Exam.Questions[_i].AnswersSortedRandomly)
                {

                    var random = new Random();
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers.OrderBy(x => random.Next()).ToList();
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                else
                {
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                IsImageQuestion = true;
                IsTextQuestion = false;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
            }
            else
            {
                IsImageQuestion = false;
                IsTextQuestion = true;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
                question.Text = Exam.Questions[_i].Text;


                if (Exam.Questions[_i].AnswersSortedRandomly)
                {
                    var random = new Random();
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers.OrderBy(x => random.Next()).ToList();
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                else
                {
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
            
                


            }
        }


        /// <summary>
        /// This method submits question and goes to the next one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        public void SubmitQuestion_Click(object sender, RoutedEventArgs e)
        {
            SolvedQuestions++;
            UnsolvedQuestions--;
            studentExam.SelectedAnswers.Add(Exam.Questions[_i].Answers.IndexOf(SelectedAnswer));
         
            if (SelectedAnswers.ContainsKey(_i))
            {
                SelectedAnswers[_i] = SelectedAnswer;
            }
            else
            {
                SelectedAnswers.Add(_i, SelectedAnswer);
            }
            SelectedAnswer = new Answer();
            if (_i < Exam.Questions.Count - 1)
            {
                
                _i++;
                CurrentQuestion = _i + 1;
                previousButton.IsEnabled = true;
            }
            else
            {
                return;
            }

            if (!string.IsNullOrEmpty(Exam.Questions[_i].ImageUrl))
            {
                ImageQuestion = imageService.GetMedia(Exam.Questions[_i].ImageUrl);
                if (Exam.Questions[_i].AnswersSortedRandomly)
                {
                    var random = new Random();
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers.OrderBy(x => random.Next()).ToList();
                }
                else
                {
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
                IsImageQuestion = true;
                IsTextQuestion = false;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
            }
            else
            {
                IsImageQuestion = false;
                IsTextQuestion = true;
                SetField(ref _isImageQuestion, IsImageQuestion, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, IsTextQuestion, nameof(IsTextQuestion));
                question.Text = Exam.Questions[_i].Text;
                if (Exam.Questions[_i].AnswersSortedRandomly)
                {
                    var random = new Random();
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers.OrderBy(x => random.Next()).ToList();
                }
                else
                {
                    radioListBoxEdit.ItemsSource = Exam.Questions[_i].Answers;
                    if (SelectedAnswers.ContainsKey(_i))
                    {
                        radioListBoxEdit.SelectedItem = SelectedAnswers[_i];
                    }
                }
               
            }
           
        }


        /// <summary>
        /// This method submits exam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SubmitExam_Click(object sender, RoutedEventArgs e)
        {
            await studentService.SubmitTestAnswers(Exam, Const.UserId, studentExam);
            bool success = true;
            if (success)
            {
                HandyControl.Controls.MessageBox.Show($"Successfully submited, you can check your results on Results area with an your student ID:{Student.Id}");
                this.Close();

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
