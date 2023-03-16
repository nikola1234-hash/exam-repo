using ExamManagement.Event;
using ExamManagement.Models;
using ExamManagement.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : INotifyPropertyChanged
    {

       public event EventHandler<QuestionCustomEvent> RiseQuestionAddedEvent;

        private Question _question;

        public Question Question
        {
            get { return _question; }
            set
                {
                    SetField(ref _question, value, nameof(Question));
                }
        }


        private ObservableCollection<Question> _questions;

        public ObservableCollection<Question> Questions
        {
            get { return _questions; }
            set
            {
                SetField(ref _questions, value, nameof(Questions));
            }
        }



        private ObservableCollection<Answer> _answers;

        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set
            {
                SetField(ref _answers, value, nameof(Answers));
            }
        }


        public Guid ExamGuid { get;set;}


        public string Media { get; set; }
        private bool _isImageQuestion;

        public bool IsImageQuestion
        {
            get { return _isImageQuestion; }
            set 
            {
                SetField(ref _isImageQuestion, value, nameof(IsImageQuestion));
                SetField(ref _isTextQuestion, value, nameof(IsTextQuestion));
            }
        }

        private bool _isTextQuestion;

        public bool IsTextQuestion
        {
            get { return !_isImageQuestion; }
            set
            {
                SetField(ref _isTextQuestion, value, nameof(IsTextQuestion));
            }
        }


        private bool _randomizeAnswers;

        public bool RandomizeAnswers
        {
            get { return _randomizeAnswers; }
            set
            {
                Question.AnswersSortedRandomly = value;
                SetField(ref _randomizeAnswers, value, nameof(RandomizeAnswers));
            }   
        }




        private Answer _answer;

        public Answer Answer
        {
            get { return _answer; }
            set 
            {
                SetField(ref _answer, value, nameof(Answer));
                
            }
        }


        private bool _isCorrect;

        public bool IsCorrect
            
        {
            get { return _isCorrect; }
            set
            {
                SetField(ref _isCorrect, value, nameof(IsCorrect));
            }
        }

        private readonly ImageService imageService;
        

        public AddQuestionWindow(Guid examId)
        {
            InitializeComponent();
            Question = new Question();
            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>();
            Answer = new Answer();
            ExamGuid = examId;
            ShowIsCorrectAnswer = true;
            imageService = new ImageService();
        }
        private bool _showIsCorrectAnswer;

        public bool ShowIsCorrectAnswer
        {
            get { return _showIsCorrectAnswer; }
            set
            {


                SetField(ref _showIsCorrectAnswer, value, nameof(ShowIsCorrectAnswer));
            }
        }

        private void SubmitQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if(Questions.Count > 0)
            {
                RiseQuestionAddedEvent?.Invoke(this, new QuestionCustomEvent(Questions));
            }
            else
            {
                RiseQuestionAddedEvent?.Invoke(this, new QuestionCustomEvent(Questions));
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

        // Add Image
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;

            if (choofdlog.ShowDialog() == true)
            {
                var imagePath = imageService.AddImage(choofdlog.FileName, ExamGuid);
                Question.ImageUrl = imagePath;
                imageArea.Source = imageService.GetMedia(imagePath);
            }
        }
        // Save Single Question
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Questions.Count > 0)
            {
                Questions.Add(Question);
                Question.Text = string.IsNullOrEmpty(Question.ImageUrl) ? StringFromRichTextBox(questionText) : "Image question";
                RiseQuestionAddedEvent?.Invoke(this, new QuestionCustomEvent(Questions));
                MessageBox.Show("Questions added");
                this.Close();
            }
            else
            {
                if(Question.Answers.Count == 0)
                {
                    MessageBox.Show("Your question does not have answers");
                }
                else
                {
                    Question.Text = StringFromRichTextBox(questionText);
                    RiseQuestionAddedEvent?.Invoke(this, new QuestionCustomEvent(Question));
                    MessageBox.Show("Question added");
                    this.Close();
                }
               
            }
        }
        // Add Another Question
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ShowIsCorrectAnswer = true;
            if(Question.Answers.Count > 0)
            {
                Question.Text = string.IsNullOrEmpty(Question.ImageUrl) ? StringFromRichTextBox(questionText) : "Image question";
                Questions.Add(Question);
                Question = new Question();
                Answers = new ObservableCollection<Answer>();
                IsImageQuestion = false;
                imageArea.Source = null;
                questionText.Document.Blocks.Clear();
                UpdateProperties();

            }
            else
            {
                MessageBox.Show("You didnt create any answers");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            if (IsCorrect)
            {
                Answer.Text = StringFromRichTextBox(asnwerTextBox);
                Question.Answers.Add(Answer);
                Question.CorrectAnswerIndex = Question.Answers.IndexOf(Answer);
                Answers.Add(Answer);
                Answer = new Answer();
                asnwerTextBox.Document.Blocks.Clear();
                ShowIsCorrectAnswer = false;
                IsCorrect = false;
                UpdateProperties();
           
                
            }
            else
            {
                Answer.Text = StringFromRichTextBox(asnwerTextBox);
                Question.Answers.Add(Answer);
                Answers.Add(Answer);
                Answer = new Answer();
                asnwerTextBox.Document.Blocks.Clear();
                UpdateProperties();
            }
        }

        string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                rtb.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                rtb.Document.ContentEnd
            );

            // The Text property on a TextRange object returns a string
            // representing the plain text content of the TextRange.
            return textRange.Text;
        }


        private void UpdateProperties()
        {
            SetField(ref _answers, Answers, nameof(Answers));
            SetField(ref _question, Question, nameof(Question));
            SetField(ref _answer, Answer, nameof(Answer));
            SetField(ref _showIsCorrectAnswer, ShowIsCorrectAnswer, nameof(ShowIsCorrectAnswer));
        }
    }
}
