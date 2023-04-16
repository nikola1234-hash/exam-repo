using EasyTestMaker.Models;
using EasyTestMaker.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace EasyTestMaker
{
    /// <summary>
    /// Interaction logic for UpdateExam.xaml
    /// </summary>
    public partial class UpdateExam : INotifyPropertyChanged
    {
        private ObservableCollection<Test> _exams;

        public ObservableCollection<Test> Exams
        {
            get { return _exams; }
            set
            {
                SetField(ref _exams, value, nameof(Exams));
            }
        }
        private bool _isEditEnabled;

        public bool IsEditEnabled
        {
            get { return _isEditEnabled; }
            set
            {
                SetField(ref _isEditEnabled, value, nameof(IsEditEnabled));
            }
        }


        private Test _exam;

        public Test Exam
        {
            get { return _exam; }
            set
            {
                if(value != null)
                {
                    IsEditEnabled = true;
                }
                SetField(ref _exam, value, nameof(Exam));
            }
        }
            

        private readonly TestService _examService;
        public UpdateExam()
        {
            _examService = new TestService();
            InitializeComponent();
            InitializeExamList();
        }
        /// <summary>
        /// Gets Exams from API
        /// </summary>
        public void InitializeExamList()
        {
            var exams = Task.Run(() => _examService.GetTestsAsync()).Result;
            Exams = new ObservableCollection<Test>(exams);
        }
        /// <summary>
        /// Opens EditExamWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditExam(object sender, RoutedEventArgs e)
        {
            EditExamWindow editExamWindow = new EditExamWindow(Exam);
            editExamWindow.RiserListChanged += EditExamWindow_RiserListChanged;
            editExamWindow.Show();
        }

        private void EditExamWindow_RiserListChanged(object? sender, CustomEventArgs e)
        {
            var exams = Task.Run(() => _examService.GetTestsAsync()).Result;
            Exams = new ObservableCollection<Test>(exams);
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
