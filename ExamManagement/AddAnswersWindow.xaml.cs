using ExamManagement.Models;
using Prism.Events;
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
    /// Interaction logic for AddAnswersWindow.xaml
    /// </summary>
    public partial class AddAnswersWindow : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Answer> _answers;
        public int Index { get; set; }
        public ObservableCollection<Answer> Answers
        {
            get { return _answers; }
            set => SetField(ref _answers, value, nameof(Answers));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private IEventAggregator eventAggregator = new EventAggregator();
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public AddAnswersWindow()
        {
            InitializeComponent();
            Answers = new ObservableCollection<Answer>();
        }


        public event EventHandler<CustomEventArgs> RaiseCustomEvent;
        private void AddAnswers_Click (object sender, RoutedEventArgs e)
        {
            RaiseCustomEvent(this, new CustomEventArgs(Answers, Index));
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var answer = new Answer();
            Answers.Add(answer);
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            if ((bool)box.IsChecked)
            {
                Index = Answers.IndexOf(Answers.Last());
            }
            else
            {
                Index = 0;
            }
        }
    }
}
