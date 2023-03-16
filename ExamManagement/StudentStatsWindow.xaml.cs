using ExamManagement.Models;
using ExamManagement.Services;
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

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for StudentStatsWindow.xaml
    /// </summary>
    public partial class StudentStatsWindow : INotifyPropertyChanged 
    {


        private readonly ExamService _examService;
        private int _studentId;

        public int StudentId
        {
            get { return _studentId; }
            set 
            {
                _studentId = value;
            }
        }
        private ObservableCollection<ExamResult> _examResults;

        public ObservableCollection<ExamResult> ExamResults
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
            _examService = new ExamService();
            ExamResults = new ObservableCollection<ExamResult>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var results = Task.Run(() => _examService.GetExamResultsByName(StudentId)).Result;
            ExamResults = new ObservableCollection<ExamResult>(results);
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
