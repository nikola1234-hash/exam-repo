﻿using ExamManagement.Models;
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
    /// Interaction logic for UpdateExam.xaml
    /// </summary>
    public partial class UpdateExam : INotifyPropertyChanged
    {
        private ObservableCollection<Exam> _exams;

        public ObservableCollection<Exam> Exams
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


        private Exam _exam;

        public Exam Exam
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
            

        private readonly ExamService _examService;
        public UpdateExam()
        {
            _examService = new ExamService();
            InitializeComponent();
            InitializeExamList();
        }
        public void InitializeExamList()
        {
            var exams = Task.Run(() => _examService.GetExamsAsync()).Result;
            Exams = new ObservableCollection<Exam>(exams);
        }

        private void EditExam(object sender, RoutedEventArgs e)
        {
            EditExamWindow editExamWindow = new EditExamWindow(Exam);
            editExamWindow.Show();
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
