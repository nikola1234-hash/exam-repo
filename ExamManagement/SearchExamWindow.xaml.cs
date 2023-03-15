using ExamManagement.Models;
using ExamManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class SearchExamWindow : Window
    {

        public ObservableCollection<Exam> Exams { get; set; }
        private readonly APIService<Exam> _apiService;

        private string _studentName;

        public string  StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public Exam Exam { get; set; }
        public SearchExamWindow()
        {
            InitializeComponent();
            Exams = new ObservableCollection<Exam>();
            _apiService = new APIService<Exam>("https://localhost:7129");
        }
        private async void SearchBox_Click(object sender, RoutedEventArgs e)
        {
            //string text = SearchBox.Text;
            //Exam exam = await _apiService.GetExamAsync(text);
            //if(exam.Date.Date > DateTime.Now.Date)
            //{
            //    MessageBox.Show($"Exam starts on {exam.Date.Date.ToShortDateString()}");
            //    this.Close();
            //}
            //else if(exam.Date.Date > DateTime.Now.Date)
            //{
            //    MessageBox.Show("Exam has passed");
            //    this.Close();
            //}
            //else
            //{
            //    Exam = exam;
            //    Exams.Add(exam);
            //    resultList.ItemsSource = Exams;
            //}
        
        }
        private async void OpenExam_Click(object sender, RoutedEventArgs e)
        {
            StudentService studentService = new StudentService();
            Student st = new Student();
            st.Name = StudentName;
            var student =  await studentService.UpdateStudentInformation(st);

            var examWindow = new ExamWindow(Exam, student);
            examWindow.Show();
            this.Close();
        }
     
    }
}
