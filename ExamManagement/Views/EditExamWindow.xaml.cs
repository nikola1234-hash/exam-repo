using ExamManagement.Models;
using ExamManagement.Services;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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

    public partial class EditExamWindow : INotifyPropertyChanged, IDisposable
    {

        private readonly ExamService _examService;
        

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

        public EditExamWindow(Exam exam)
        {
            InitializeComponent();
            _examService = new ExamService();
            Exam = exam;
            ObservableQuestions = new ObservableCollection<Question>(exam.Questions);
            
            

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

        private void EditQuestionWindow_RiseQuestionAddedEvent(object? sender, Event.QuestionCustomEvent e)
        {
            Exam.Questions.Clear();
            if(e.Arg is Question question)
            {
                Exam.Questions.Remove(SelectedQuestion);
                Exam.Questions.Add(question);
                ObservableQuestions.Add(question);
            }          
        }

        
        public void ResetForm()
        {
            Exam = new Exam();
            ObservableQuestions = new ObservableCollection<Question>();
            SetField(ref _exam, Exam, nameof(Exam));
            SetField(ref _observableQuestions, ObservableQuestions, nameof(ObservableQuestions));

        }


        private void editQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            EditQuestionWindow addQuestionWindow = new EditQuestionWindow(SelectedQuestion);
            addQuestionWindow.RiseQuestionAddedEvent += EditQuestionWindow_RiseQuestionAddedEvent;
            addQuestionWindow.Show();
        }

        private async void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            _examService.AddExam(Exam);
            HandyControl.Controls.MessageBox.Show("Successfull, will redirect you to exam list window");
            this.Close();    
            
             
        }
        

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
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}