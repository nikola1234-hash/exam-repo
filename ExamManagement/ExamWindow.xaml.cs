using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using ExamManagement.Models;

namespace ExamManagement
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : ThemedWindow
    {
        public ExamWindow(Exam exam)
        {
            InitializeComponent();
            question.Text = exam.Questions[0].Text;
            radioListBoxEdit.ItemsSource = exam.Questions[0].Answers;
        }

    }
}
