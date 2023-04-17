using EasyTestMaker.Event;
using EasyTestMaker.Models;
using EasyTestMaker.Services;
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

namespace EasyTestMaker
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class EditQuestionWindow : INotifyPropertyChanged
    {

       public event EventHandler<QuestionCustomEvent> RiseQuestionAddedEvent;
        #region Fields
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
                if(value == true)
                {
                    Question.CorrectAnswerIndex = Question.Answers.IndexOf(SelectedAnswer);
                    TextVisible = true;
                    value = false;
                    ShowIsCorrectAnswer = value;
                    SetField(ref _showIsCorrectAnswer, value, nameof(ShowIsCorrectAnswer));
                }
                SetField(ref _isCorrect, value, nameof(IsCorrect));
            }
        }

        private bool _textVisible;

        public bool TextVisible
        {
            get { return _textVisible; }
            set 
            { 
               SetField(ref _textVisible, value, nameof(TextVisible));
            }
        }

        private bool _isNewAnswer;

        public bool IsNewAnswer
        {
            get { return _isNewAnswer; }
            set
            {
                NewAnswerIsEnabled = !value;
                SetField(ref _isNewAnswer, value, nameof(IsNewAnswer));
            }
            
        }

        private bool _newAnswerIsEnabled;

        public bool NewAnswerIsEnabled
        {
            get { return _newAnswerIsEnabled; }
            set
            {
                SetField(ref _newAnswerIsEnabled, value, nameof(NewAnswerIsEnabled));
            }
        }


        private readonly ImageService imageService;
        private Answer _selectedAnswer;

        public Answer SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                if(_selectedAnswer != value)
                {
                    
                    ShowIsCorrectAnswer = !(Question.CorrectAnswerIndex == Question.Answers.IndexOf(value));
                    TextVisible = !ShowIsCorrectAnswer;
                    asnwerTextBox.Document.Blocks.Clear();
                    asnwerTextBox.Document.Blocks.Add(new Paragraph(new Run(value.Text)));
                    IsNewAnswer = false;
                }
                else
                {
                    asnwerTextBox.Document.Blocks.Clear();
                    asnwerTextBox.Document.Blocks.Add(new Paragraph(new Run(value.Text)));
                }
                SetField(ref _selectedAnswer, value, nameof(SelectedAnswer));
            }
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

        #endregion
        public EditQuestionWindow(Question question)
        {
            InitializeComponent();
            Question = question;
            Questions = new ObservableCollection<Question>();
            Answers = new ObservableCollection<Answer>(question.Answers);
            Answer = new Answer();
            SelectedAnswer = question.Answers[question.CorrectAnswerIndex];
            ShowIsCorrectAnswer = false;
            IsImageQuestion = !string.IsNullOrEmpty(question.ImageUrl);
            imageService = new ImageService();

            IsTextQuestion = !IsImageQuestion;
            if (IsImageQuestion)
            {
                imageArea.Source = imageService.GetMedia(question.ImageUrl);
            }
            RandomizeAnswers = question.AnswersSortedRandomly;
            questionText.Document.Blocks.Add(new Paragraph(new Run(question.Text)));
            NewAnswerIsEnabled = true;
        }
     
        //Removes answer from List
        private void RemoveAnswer(object sender, RoutedEventArgs e)
        {
            Question.Answers.Remove(SelectedAnswer);
            Answers.Remove(SelectedAnswer);
        }

        private void SubmitQuestionButton_Click(object sender, RoutedEventArgs e)
        {
           

            RiseQuestionAddedEvent?.Invoke(this, new QuestionCustomEvent(Question));

            
        }

        private void SaveAnswer(object sender, RoutedEventArgs e)
        {
            if (IsNewAnswer)
            {
                Answer.Text = StringFromRichTextBox(asnwerTextBox);
                Question.Answers.Add(Answer);
                Answers.Add(Answer);
                IsNewAnswer = false;
                UpdateProperties();
               
            }
            else
            {
                var index = Question.Answers.IndexOf(SelectedAnswer);
                Question.Answers[index].Text = StringFromRichTextBox(asnwerTextBox);
                Answers[index].Text = StringFromRichTextBox(asnwerTextBox);
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
        // Adds new Answer
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
                
                IsNewAnswer = true;
                Answer = new Answer();
                IsCorrect = false;
                asnwerTextBox.Document.Blocks.Clear();
                UpdateProperties();

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
            SetField(ref _questions, Questions, nameof(Questions));
            SetField(ref _answer, Answer, nameof(Answer));
            SetField(ref _showIsCorrectAnswer, ShowIsCorrectAnswer, nameof(ShowIsCorrectAnswer));
        }
    }
}
