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
                    SelectedExam = new Test();
                    Exams = new ObservableCollection<Test>();
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



        private ObservableCollection<Test> _exams;

        public ObservableCollection<Test> Exams
        {
            get { return _exams; }
            set
            {
                SetField(ref _exams, value, nameof(Exams));
            }
        }
        private Test _selectedExam;

        public Test SelectedExam
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
                if(!string.IsNullOrEmpty(value.Name))
                {
                    IsButtonEnabled = true;
                }
                else
                {
                    IsButtonEnabled = false;
                }
                if(value == null)
                {
                    IsButtonEnabled = false;
                }
                SetField(ref _selectedExam, value, nameof(SelectedExam));
            }
        }


        private readonly TestService _examService;



        private string _studentName;

        public string  StudentName
        {
            get { return _studentName; }
            set
            {
                if(SelectedExam == null)
                {
                    IsButtonEnabled = false;
                    SetField(ref _studentName, value, nameof(StudentName));
                    return;
                }
                if (string.IsNullOrEmpty(value))
                {
                    IsButtonEnabled = false;
                }
                else if(!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(SelectedExam.Name))
                {
                    IsButtonEnabled = true;
                }

                SetField(ref _studentName, value, nameof(StudentName));
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
                    IsButtonEnabled = true;
                }
                else
                {
                    IsButtonEnabled = false;
                }
                if(!string.IsNullOrEmpty(value.Name))
                {
                    IsButtonEnabled = true;
                }
                else
                {
                    IsButtonEnabled = false;
                }
                SetField(ref _exam, value, nameof(Exam));
            }
        }

        public SearchExamWindow()
        {
            InitializeComponent();
            Exams = new ObservableCollection<Test>();
            _examService = new TestService();
            
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
                Test exam = await _examService.GetTestByName(SearchBar);
                if(exam == null || string.IsNullOrEmpty(exam.Name))
                {
                    IsProgressVisible = false;
                    IsButtonEnabled= false;
                    return;
                    
                }
                else
                {
                    if(Exams.Count > 0)
                    {
                        Exams = new ObservableCollection<Test>();
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

            try
            {
                StudentService studentService = new StudentService();
                Student st = new Student();
                st.Name = StudentName;
                if (string.IsNullOrEmpty(StudentName))
                {
                    throw new ArgumentException("You need to write name", nameof(StudentName));
                }
                var student =  await studentService.UpdateStudentInformation(st);

                if(student == null)
                {
                    throw new ArgumentNullException("Student is missing");
                }

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
