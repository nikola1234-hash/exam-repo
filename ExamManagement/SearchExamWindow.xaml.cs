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
                    SelectedExam = new Exam();
                    Exams = new ObservableCollection<Exam>();
                    SetField(ref _searchBar, value, nameof(SearchBar));
                }
            }
        }

        private bool _isProgressVisible;

        public bool IsProgressVisible
        {
            get { return _isProgressVisible; }
            set
            {
                SetField(ref _isProgressVisible,  value, nameof(IsProgressVisible));
            }
        }


        private int _progressBarValue;

        public int ProgressBarValue
            
        {
            get { return _progressBarValue; }
            set
            {
                SetField(ref _progressBarValue, value, nameof(ProgressBarValue));
            }
        }
        private bool _isButtonEnabled;

        public bool IsButtonEnabled
            
        {
            get { return _isButtonEnabled; }
            set
            {
                SetField(ref _isButtonEnabled, value, nameof(IsButtonEnabled));
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
                if(value != null)
                {
                    IsButtonEnabled = true;
                }
                if (value != _selectedExam)
                {
                    IsButtonEnabled = true;
                
                }
                if(value == null)
                {
                    IsButtonEnabled = false;
                }
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

        /// <summary>
        /// When clicking on search exam button
        /// this method is called
        /// it searches exam by name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchBox_Click(object sender, RoutedEventArgs e)
        {
            IsProgressVisible = true;
            if (!string.IsNullOrEmpty(SearchBar))
            {
                Exam exam = await _examService.GetExamByName(SearchBar);
                if(exam == null)
                {
                    IsProgressVisible = false;
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
                    IsProgressVisible = false;
                }

             

            }
      
        }
        /// <summary>
        /// When clicking on exam to open this method is called
        /// it creates new student and stores him in db
        /// student service start exam checks if exam is in valid Time 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenExam_Click(object sender, RoutedEventArgs e)
        {
            StudentService studentService = new StudentService();
            Student st = new Student();
            st.Name = StudentName;
            var student =  await studentService.UpdateStudentInformation(st);


            try
            {
                var examFromServer = await studentService.StartExam(Exam);
                var examWindow = new ExamWindow(examFromServer, student);
                examWindow.Show();
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
