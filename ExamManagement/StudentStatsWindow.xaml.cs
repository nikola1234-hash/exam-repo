using EasyTestMaker.Models;
using EasyTestMaker.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for StudentStatsWindow.xaml
    /// </summary>
    public partial class StudentStatsWindow : INotifyPropertyChanged 
    {


        private readonly TestService _examService;
        private int _studentId;

        public int StudentId
        {
            get { return _studentId; }
            set 
            {
                _studentId = value;
            }
        }
        private ObservableCollection<TestResult> _examResults;

        public ObservableCollection<TestResult> ExamResults
        {
            get { return _examResults; }
            set
            {
                SetField(ref _examResults, value, nameof(ExamResults));
            }
        }

        public StudentStatsWindow()
        {
            InitializeComponent();
            _examService = new TestService();
            ExamResults = new ObservableCollection<TestResult>();
        }
        /// <summary>
        /// Shows overall stats for student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var results = Task.Run(() => _examService.GetTestResultsById(StudentId)).Result;
            ExamResults = new ObservableCollection<TestResult>(results);
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
