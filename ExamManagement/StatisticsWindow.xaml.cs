using EasyTestMaker.Models;
using EasyTestMaker.Services;
using HandyControl.Tools.Extension;
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
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : INotifyPropertyChanged
    {

        


        private double _averageGrade;

        public double AverageGrade
        {
            get { return _averageGrade; }
            set 
            {
                SetField(ref _averageGrade, value, nameof(AverageGrade));
            }
        }

        private int _numberOfStudents;

        public int NumberOfStudents
        {
            get { return _numberOfStudents; }
            set
            {
                SetField(ref _numberOfStudents, value, nameof(NumberOfStudents));
            }
        }

        private int _correctAnswers;

        public int CorrectAnswers
        {
            get { return _correctAnswers; }
            set
            {
                SetField(ref _correctAnswers, value, nameof(CorrectAnswers));
            }
        }
        private int _invalidAnswers;

        public int InvalidAnswers
        {
            get { return _invalidAnswers; }
            set
            {
                SetField(ref _invalidAnswers, value, nameof(InvalidAnswers));
            }
        }

  

        private readonly ExamService _examService;
        public StatisticsWindow()
        {
            _examService = new ExamService();
            InitializeComponent();
            GetGrades();

        }

        /// <summary>
        /// Get all grades and calculate the average grade
        /// SHows Valid and Invalid answers
        /// </summary>
        private async void GetGrades()
        {
            List<ExamResult> results = await _examService.GetExamResults();

            if (results == null)
            {
                MessageBox.Show("No results found");
                return;
            }
            foreach(var result in results)
            {
                if(result.Errors.Count == 0)
                {
                    var exams =  await _examService.GetExamsAsync();
                    CorrectAnswers += exams.Where(s=> s.Id == result.ExamId).FirstOrDefault().Questions.Count();
                }
                foreach(var error in result.Errors)
                {
          
                    if(error.SelectedAnswer == error.CorrectAnswer)
                    {
                        CorrectAnswers++;
                    }
                    else
                    {
                        InvalidAnswers++;
                    }
                }
            }

            AverageGrade = results.Average(x => x.Grade);
            NumberOfStudents = results.Select(s=> s.StudentId).Count();
         

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
