using EasyTestMaker.Event;
using EasyTestMaker.Models;
using EasyTestMaker.Services;
using HandyControl.Tools.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EasyTestMaker.ViewModels
{
    public class CreateTestViewModel : ViewModelBase
    {
        private readonly ITestService _service = App.GetService<ITestService>();


        private Test _test;

        public Test Test
        {
            get { return _test; }
            set
            {
                SetProperty(ref _test, value);
            }
        }


        private ObservableCollection<Question> _observableQuestions;

        public ObservableCollection<Question> ObservableQuestions
        {
            get { return _observableQuestions; }
            set
            {
                SetProperty(ref _observableQuestions, value);
            }
        }

        public ICommand CreateCommand { get;}
        public ICommand UpdateServerCommand { get;}
        public ICommand CreateQuestions { get;}

        public CreateTestViewModel()
        {

            Initialize();
            CreateCommand = new Prism.Commands.DelegateCommand(CreateTest);
            UpdateServerCommand = new Prism.Commands.DelegateCommand(Update);
            CreateQuestions = new Prism.Commands.DelegateCommand(OpenQuestionsWindow);
        }

        private void OpenQuestionsWindow()
        {
            AddQuestionWindow addQuestionWindow = new AddQuestionWindow(Test.Id);
            addQuestionWindow.RiseQuestionAddedEvent += AddQuestionWindow_RiseQuestionAddedEvent;
            addQuestionWindow.Show();

        }

        private void AddQuestionWindow_RiseQuestionAddedEvent(object? sender, QuestionCustomEvent e)
        {
            if (e.Arg is Question question)
            {
                Test.Questions.Add(question);
                ObservableQuestions.Add(question);
            }

            if (e.Arg is ObservableCollection<Question> questions)
            {
                Test.Questions = questions.ToList();
                ObservableQuestions = questions;
            
            }
        }

        private void Update()
        {
            Task.Run(() => SendToServerAsync());
        }

        private void Initialize()
        {
            Test = new Test();
            ObservableQuestions = new ObservableCollection<Question>();
        }


        public void Reset()
        {
            Test = new Test();
            ObservableQuestions = new ObservableCollection<Question>();
        }


        private async Task SendToServerAsync()
        {
            try
            {
                await _service.UpdateExam(Test, true);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message, "Error");
            }
        }
        private void CreateTest()
        {
            _service.AddExam(Test);
            var result = HandyControl.Controls.MessageBox.Show("Would you like to create another test?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Test = new Test();
                ObservableQuestions = new ObservableCollection<Question>();

            }
            else
            {
                var question = HandyControl.Controls.MessageBox.Show("Would you like to push this test to server?", "", MessageBoxButton.YesNo);
                if (question == MessageBoxResult.Yes)
                {
                    try
                    {
                        Task.Run(() => PushToServer());
                        Reset();
                    }
                    catch (Exception ex)
                    {
                        HandyControl.Controls.MessageBox.Show(ex.Message, "Error");
                    }
                }
                else
                {
                    Reset();
                }

            }

        }

        private async Task PushToServer()
        {
            await _service.UpdateExam(Test, true);
        }
    }
}
