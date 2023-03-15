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
    /// Interaction logic for SearchExamWindow.xaml
    /// </summary>
    public partial class SearchExamWindow : INotifyPropertyChanged
    {



        private string _searchBar;

        public string SearchBar
        {
            get { return _searchBar; }
            set
            {
                if (value != _searchBar)
                {
                    SetField(ref _searchBar, value, nameof(SearchBar));
                }
            }
        }


        private ObservableCollection<Exam> _exams;

        public ObservableCollection<Exam> Exams
        {
            get { return _exams; }
            set
            {
                SetField(ref _exams, value, nameof(Exams));
            }
        }
        private Exam _selectedExam;

        public Exam SelectedExam
        {
            get { return _selectedExam; }
            set
            {
                SetField(ref _selectedExam, value, nameof(SelectedExam));
            }
        }


        private readonly ExamService _examService;



        private string _studentName;

        public string  StudentName
        {
            get { return _studentName; }
            set
            {
                SetField(ref _studentName, value, nameof(StudentName));
            }
        }

        public Exam Exam { get; set; }
        public SearchExamWindow()
        {
            InitializeComponent();
            Exams = new ObservableCollection<Exam>();
            _examService = new ExamService();
            
        }
        private async void SearchBox_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchBar))
            {
                Exam exam = await _examService.GetExamByName(SearchBar);
                if(exam == null)
                {
                    return;

                }
                else
                {
                    if(Exams.Count > 0)
                    {
                        Exams = new ObservableCollection<Exam>();
                    }

                    Exam = exam;
                    Exams.Add(Exam);
                    SetField(ref _exams, Exams, nameof(Exams));
                }

             

            }
      
        }
        private async void OpenExam_Click(object sender, RoutedEventArgs e)
        {
            StudentService studentService = new StudentService();
            Student st = new Student();
            st.Name = StudentName;
            var student =  await studentService.UpdateStudentInformation(st);


            try
            {
                var examWindow = new ExamWindow(SelectedExam.Id, student);
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
