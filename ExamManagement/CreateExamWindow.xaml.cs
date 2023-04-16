using EasyTestMaker.Models;
using EasyTestMaker.Services;
using HandyControl.Tools.Converter;
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

namespace EasyTestMaker
{
    /// <summary>
    /// Interaction logic for CreateExamWindow.xaml
    /// </summary>
    public partial class CreateExamWindow : INotifyPropertyChanged, IDisposable
    {

        private readonly TestService _service;
        

        private Test _test;

        public Test Test
        {
            get { return _test; }
            set
            {
                SetField(ref _test, value, nameof(Test));
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

        public CreateExamWindow()
        {
            InitializeComponent();
            _service = new TestService();
            Test = new Test();
            ObservableQuestions = new ObservableCollection<Question>();
            
            

        }
    

        private void CreateNewExam_Click(object sender, RoutedEventArgs e)
        {
            Test = new Test();
        }
        
        // Opens new AddQuestion Window
        
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // do something
            AddQuestionWindow addQuestionWindow = new AddQuestionWindow(Test.Id);
            addQuestionWindow.RiseQuestionAddedEvent += AddQuestionWindow_RiseQuestionAddedEvent; 
            addQuestionWindow.Show();
            
        
        }
        /// <summary>
        /// Risen by event from add question Window
        /// Adds added questions to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddQuestionWindow_RiseQuestionAddedEvent(object? sender, Event.QuestionCustomEvent e)
        {

            if(e.Arg is Question question)
            {
                Test.Questions.Add(question);
                ObservableQuestions.Add(question);
            }

            if(e.Arg is ObservableCollection<Question> questions)
            {
                Test.Questions = questions.ToList();
                ObservableQuestions = questions;
                SetField(ref _observableQuestions, ObservableQuestions, nameof(ObservableQuestions));
            }

          
            
        }

        /// <summary>
        /// Form Reset
        /// </summary>
        public void ResetForm()
        {
            Test = new Test();
            ObservableQuestions = new ObservableCollection<Question>();
            SetField(ref _test, Test, nameof(Test));
            SetField(ref _observableQuestions, ObservableQuestions, nameof(ObservableQuestions));

        }
        //Creates exam updates JSON and pushes to server in the end resets form
        private async void CreateExamButton_Click(object sender, RoutedEventArgs e)
        {
            _service.AddExam(Test);
            var result = HandyControl.Controls.MessageBox.Show("Would you like to create another exam?", "", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                Test = new Test();
                ObservableQuestions = new ObservableCollection<Question>();

            }
            else
            {
                var question = HandyControl.Controls.MessageBox.Show("Would you like to push this exam to server?", "", MessageBoxButton.YesNo);
                if (question == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _service.UpdateExam(Test, true);
                        ResetForm();
                    }
                    catch (Exception ex)
                    {
                        HandyControl.Controls.MessageBox.Show(ex.Message, "Error");
                    }
                }
                else
                {
                    ResetForm();
                }

            }
             
        }
        
        /// <summary>
        /// Submits exam to server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SubmitExamToServer(object sender, RoutedEventArgs e)
        {
            try
            {
                await _service.UpdateExam(Test, true);
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

        /// <summary>
        /// Notify Property Changed
        /// </summary>
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