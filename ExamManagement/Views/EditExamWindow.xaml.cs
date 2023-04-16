using EasyTestMaker.Models;
using EasyTestMaker.Services;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace EasyTestMaker
{

    public partial class EditExamWindow : INotifyPropertyChanged, IDisposable
    {

        private readonly ExamService _examService;

        #region Fields
        public event EventHandler<CustomEventArgs> RiserListChanged;



        private Exam _exam;

        public Exam Exam
        {
            get { return _exam; }
            set
            {
                SetField(ref _exam, value, nameof(Exam));
            }
        }


        private ObservableCollection<Question> _observableQuestions;

        public ObservableCollection<Question> ObservableQuestions
        {
            get { return _observableQuestions; }
            set
            {
                SetField(ref _observableQuestions, value, nameof(ObservableQuestions));
            }
        }
        private Question _selectedQuestion;

        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                if(value != null)
                {
                    IsEditButtonEnabled = true;
                }
                SetField(ref _isEditButtonEnabled, IsEditButtonEnabled, nameof(IsEditButtonEnabled));
                SetField(ref _selectedQuestion, value, nameof(SelectedQuestion));
            }
        }
        private bool _isEditButtonEnabled;

        public bool IsEditButtonEnabled
        {
            get { return _isEditButtonEnabled; }
            set 
            { 
                SetField(ref _isEditButtonEnabled, value, nameof(IsEditButtonEnabled));
            }
        }
        #endregion
        public EditExamWindow(Exam exam)
        {
            InitializeComponent();
            _examService = new ExamService();
            Exam = exam;
            ObservableQuestions = new ObservableCollection<Question>(exam.Questions);
            
            

        }

        private Question _clickedQuestion;

        public Question ClickedQuestion

        {
            get { return _clickedQuestion; }
            set
            {
                SetField(ref _clickedQuestion, value, nameof(ClickedQuestion));
            }
        }

        private void CreateNewExam_Click(object sender, RoutedEventArgs e)
        {
            Exam = new Exam();
        }
        
        
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionWindow window = new AddQuestionWindow(Exam);
            window.RiseQuestionAddedEvent += Window_RiseQuestionAddedEvent;
            window.Show();


        }
        /*

        This is a private method named Window_RiseQuestionAddedEvent that handles the QuestionCustomEvent event raised when a new question is added to the exam.
        It takes two arguments, one of type Question and the other of type ObservableCollection<Question>.
        If the argument is a Question object, the method removes the currently selected question from the exam's list of questions, adds the new question to the list, and updates the ObservableQuestions collection.
        If the argument is an ObservableCollection of Question objects, the method adds all the questions in the collection to the exam's list of questions and updates the ObservableQuestions collection.
        */
        private void Window_RiseQuestionAddedEvent(object? sender, Event.QuestionCustomEvent e)
        {
            if (e.Arg is Question question)
            {
                Exam.Questions.Remove(SelectedQuestion);
                Exam.Questions.Add(question);
                ObservableQuestions.Add(question);
            }
            if(e.Arg is ObservableCollection<Question> questions)
            {
                Exam.Questions.AddRange(questions);
                ObservableQuestions.AddRange(questions);
            }
        }
        /*
          This is a private method that handles the QuestionCustomEvent event raised when a question is edited in the EditQuestionWindow. 
          The method clears the exam's list of questions and then checks the type of argument passed in the event.

          If the argument is a Question, it removes the currently selected question from the exam's list of questions, adds the edited question to the list, and updates the ObservableQuestions collection.
         */
        private void EditQuestionWindow_RiseQuestionAddedEvent(object? sender, Event.QuestionCustomEvent e)
        {
            if(e.Arg is Question question)
            {
                Exam.Questions.Remove(ClickedQuestion);
                Exam.Questions.Add(question);
                ObservableQuestions.Remove(ClickedQuestion);
                ObservableQuestions.Add(question);
            }          
        }
        // Removes question from exam
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var result = HandyControl.Controls.MessageBox.Show("Are you sure you want to remove this Question from exam?", "Error", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Exam.Questions.Remove(SelectedQuestion);
                ObservableQuestions.Remove(SelectedQuestion);
                SelectedQuestion = null;
                SetField(ref _selectedQuestion, SelectedQuestion, nameof(SelectedQuestion));
            }
         
        }


        /// <summary>
        /// This method Resets form
        /// </summary>
        public void ResetForm()
        {
            Exam = new Exam();
            ObservableQuestions = new ObservableCollection<Question>();
            SetField(ref _exam, Exam, nameof(Exam));
            SetField(ref _observableQuestions, ObservableQuestions, nameof(ObservableQuestions));

        }

        //This method opens a new window to edit the selected question
        private void editQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            EditQuestionWindow addQuestionWindow = new EditQuestionWindow(SelectedQuestion);
            addQuestionWindow.RiseQuestionAddedEvent += EditQuestionWindow_RiseQuestionAddedEvent;
            addQuestionWindow.Show();
        }

        //This methos adds exams to JSON file
        private async void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            _examService.AddExam(Exam);
            HandyControl.Controls.MessageBox.Show("Successfull, will redirect you to exam list window");
            this.Close();    
            
             
        }
        /// <summary>
        /// This method submits JSON to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private async void SubmitExamToServer(object sender, RoutedEventArgs e)
        {
            try
            {
                await _examService.UpdateExam(Exam, true);
            }
            catch(Exception ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message, "Error");
            }
          
        }

        public void Dispose()
        {
            this.Dispose();
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

        private void updateExam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task.Run(() => _examService.UpdateExamJson(Exam));
                Task.Run(() => _examService.PutToServer(Exam));

                HandyControl.Controls.MessageBox.Show("Successfull, will redirect you to exam list window", "Success");
                RiserListChanged?.Invoke(this, new CustomEventArgs(null, 0));
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

     
   
        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ClickedQuestion = SelectedQuestion;
        }
    }
}