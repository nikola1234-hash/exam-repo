using EasyTestMaker.Models;
using EasyTestMaker.Services;
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

namespace EasyTestMaker
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public APIService<GradeEntity> _api { get; set; }
        public ObservableCollection<GradeEntity> Grades { get; set; }
        public StatisticsWindow()
        {
            //_api = new APIService<GradeEntity>("https://localhost:7129");
            InitializeComponent();
            GetGrades();

        }
        private async void GetGrades()
        {
             Grades = await _api.GetExamsStatisticsAsync();
         
        }
        private int GetGradesStatistic()
        {
            var grades = Grades.Select(s => Convert.ToInt32(s.Grade.Remove('%'))).ToList();
            int gradeCount = 0;
            int counter = 0;
            for(int i = 0; i < grades.Count(); i++)
            {
                counter++;
                gradeCount += grades[i];
            }


            return gradeCount / counter;
        }
        private void GetStatistics(object sender, RoutedEventArgs e)
        {
            invalid.Text = Grades.Where(s => s.Errors.Count > 0).Count().ToString();
            taken.Text = Grades.Count().ToString();
            grades.Text = GetGradesStatistic().ToString();
        }

    }
}
